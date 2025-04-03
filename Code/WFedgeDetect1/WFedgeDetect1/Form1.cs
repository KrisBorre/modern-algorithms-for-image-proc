using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

// Chapter 6: Edge Detection
// page 87
namespace WFedgeDetect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Edge Detection";
        }

        private Bitmap origBmp;

        public Bitmap BmpPictBox1;
        public Bitmap BmpPictBox2;
        public Bitmap BmpPictBox3;

        public CImage OrigIm;  // copy of original image

        private CImage sigmaIm;  // local mean
        private CImage extremeFilteredImage;  // shading corrected image and the result
        private CImage combIm;  // shading corrected image and the result
        private CImage edgeIm;  // shading corrected image and the result

        private int nBit;
        private bool OPEN = false;
        private int nLoop, denomProg;

        public double Scale1;
        public int marginX, marginY;

        private int threshold;

        public Graphics g1, g2, g3;
        public bool BmpGraph;


        private void buttonOpenImage_Click(object sender, EventArgs e) // Open image
        {
            label4.Visible = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    origBmp = new Bitmap(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Error: " + ex.Message);
                    return;
                }
            }
            else return;

            byte[] grid;

            progressBar1.Step = 1;
            denomProg = progressBar1.Maximum / progressBar1.Step;
            nLoop = 2;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            label2.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label6.Text = "Opened image: " + openFileDialog1.FileName;
            label6.Visible = true;

            if (origBmp.PixelFormat == PixelFormat.Format24bppRgb)
            {
                grid = new byte[3 * origBmp.Width * origBmp.Height]; // color image
                nBit = 24;
                BitmapToGrid(origBmp, grid);
                OPEN = true;
                BmpGraph = true;
            }
            else if (origBmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                grid = new byte[origBmp.Width * origBmp.Height]; // indexed image
                nBit = 8;
                BitmapToGridGet(origBmp, grid);
                BmpGraph = false;
            }
            else
            {
                MessageBox.Show("Form1: Inappropriate pixel format. Returning.");
                return;
            }

            label4.Text = "Original image";
            label4.Visible = true;
            BmpGraph = false;
            progressBar1.Visible = false;
            BmpPictBox1 = new Bitmap(origBmp.Width, origBmp.Height, PixelFormat.Format24bppRgb);
            BmpPictBox2 = new Bitmap(origBmp.Width, origBmp.Height, PixelFormat.Format24bppRgb);
            BmpPictBox3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            pictureBoxOriginalImage.Image = origBmp;
            pictureBoxDetectedEdges.Image = BmpPictBox2;
            pictureBox3.Image = BmpPictBox3;

            if (BmpGraph)
            {
                g1 = Graphics.FromImage(BmpPictBox1);
                g2 = Graphics.FromImage(BmpPictBox2);
                g3 = Graphics.FromImage(BmpPictBox3);
            }
            else
            {
                g1 = pictureBoxOriginalImage.CreateGraphics();
                g2 = pictureBoxDetectedEdges.CreateGraphics();
                g3 = pictureBox3.CreateGraphics();
            }

            OrigIm = new CImage(origBmp.Width, origBmp.Height, nBit, grid);
            sigmaIm = new CImage(origBmp.Width, origBmp.Height, nBit, grid);
            extremeFilteredImage = new CImage(origBmp.Width, origBmp.Height, nBit, grid);
            combIm = new CImage(1 + 2 * origBmp.Width, 1 + 2 * origBmp.Height, 8);
            edgeIm = new CImage(origBmp.Width, origBmp.Height, 8, grid);

            double ScaleX = (double)pictureBoxOriginalImage.Width / (double)OrigIm.width;
            double ScaleY = (double)pictureBoxOriginalImage.Height / (double)OrigIm.height;

            if (ScaleX < ScaleY)
            {
                Scale1 = ScaleX;
            }
            else
            {
                Scale1 = ScaleY;
            }

            marginX = (pictureBoxOriginalImage.Width - (int)(Scale1 * OrigIm.width)) / 2;
            marginY = (pictureBoxOriginalImage.Height - (int)(Scale1 * OrigIm.height)) / 2;
            OPEN = true;
        } //************************************** end Open image *********************************************



        private void BitmapToGridGet(Bitmap bmp, byte[] Grid)
        // Assigned both for color and grayscasle images
        {
            progressBar1.Visible = true;
            Color color;
            int nByte = nBit / 8;
            for (int y = 0; y < bmp.Height; y++)
            {
                int y1 = 1 + bmp.Height / 100;
                if (y % y1 == 1) progressBar1.PerformStep();

                for (int x = 0; x < bmp.Width; x++)
                {
                    int i = x + bmp.Width * y;
                    color = origBmp.GetPixel(x, y);

                    for (int c = 0; c < nByte; c++)
                    {
                        if (c == 0) Grid[nByte * i] = color.B;
                        if (c == 1) Grid[nByte * i + 1] = color.G;
                        if (c == 2) Grid[nByte * i + 2] = color.R;
                    }
                }
            }
            progressBar1.Visible = false;
        } //****************************** end BitmapToGridOld ****************************************



        private void BitmapToGrid(Bitmap bmp, byte[] Grid)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            int nbyte;

            switch (bmp.PixelFormat)
            {
                case PixelFormat.Format24bppRgb: nbyte = 3; break;
                case PixelFormat.Format8bppIndexed: nbyte = 1; break;
                default: MessageBox.Show("BitmapToGrid: Inappropriate pixel format=" + bmp.PixelFormat); return;
            }

            IntPtr ptr = bmpData.Scan0;
            int length = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[length];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, length);

            progressBar1.Visible = true;

            for (int y = 0; y < bmp.Height; y++)
            {
                int y1 = 1 + bmp.Height / 100;
                if (y % y1 == 1) progressBar1.PerformStep();

                for (int x = 0; x < bmp.Width; x++)
                {
                    if (nbyte == 1)  // nbyte is global according to the PixelFormat of "bmp"
                    {
                        Color color = bmp.Palette.Entries[rgbValues[x + Math.Abs(bmpData.Stride) * y]]; // es war ohne 3*

                        Grid[3 * (x + bmp.Width * y) + 0] = color.B;
                        Grid[3 * (x + bmp.Width * y) + 1] = color.G;
                        Grid[3 * (x + bmp.Width * y) + 2] = color.R;
                    }
                    else
                    {
                        for (int c = 0; c < nbyte; c++)
                        {
                            Grid[c + nbyte * (x + bmp.Width * y)] = rgbValues[c + nbyte * x + Math.Abs(bmpData.Stride) * y];
                        }
                    }
                }
            }

            bmp.UnlockBits(bmpData);
            progressBar1.Visible = false;
        } //****************************** end BitmapToGrid ****************************************


        public void GridToBitmap(Bitmap bmp, byte[] grid)
        // Converts color Grid to Bitmap with any format.
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            int nbyte;

            switch (bmp.PixelFormat)
            {
                case PixelFormat.Format24bppRgb: nbyte = 3; break;
                case PixelFormat.Format8bppIndexed: nbyte = 1; break;
                default: MessageBox.Show("GridToBitmap: Inappropriate pixel format=" + bmp.PixelFormat); return;
            }

            IntPtr ptr = bmpData.Scan0;
            int size = bmp.Width * bmp.Height;
            int length = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[length];

            progressBar1.Visible = true;
            int y1 = 1 + nLoop * bmp.Height / 63;

            for (int y = 0; y < bmp.Height; y++)
            {
                if (((y + 1) % y1) == 0) progressBar1.PerformStep();

                for (int x = 0; x < bmp.Width; x++)
                {
                    if (nbyte == 1)  // nbyte is global according to the PixelFormat of "bmp"
                    {
                        Color color = bmp.Palette.Entries[grid[3 * (x + bmp.Width * y)]];

                        rgbValues[3 * (x + Math.Abs(bmpData.Stride) * y) + 0] = color.B;
                        rgbValues[3 * (x + Math.Abs(bmpData.Stride) * y) + 1] = color.G;
                        rgbValues[3 * (x + Math.Abs(bmpData.Stride) * y) + 2] = color.R;
                    }
                    else
                    {
                        for (int c = 0; c < nbyte; c++)
                        {
                            rgbValues[c + nbyte * x + Math.Abs(bmpData.Stride) * y] = grid[c + nbyte * (x + bmp.Width * y)];
                        }
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, length);
            bmp.UnlockBits(bmpData);
        } //****************************** end GridToBitmap ****************************************

        /// <summary>
        /// GridToBitmap is not called
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="Grid"></param>
        /// <param name="nbyteG"></param>
        private void GridToBitmap(Bitmap bmp, byte[] Grid, int nbyteG)
        // Converts Grid with "nbytesG" bytes per pixel to Bitmap with any format.
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            int nbyteB;
            switch (bmp.PixelFormat)
            {
                case PixelFormat.Format24bppRgb: nbyteB = 3; break;
                case PixelFormat.Format8bppIndexed: nbyteB = 1; break;
                default: MessageBox.Show("GridToBitmap: Inappropriate pixel format=" + bmp.PixelFormat); return;
            }
            IntPtr ptr = bmpData.Scan0;
            int size = bmp.Width * bmp.Height;
            int length = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[length];

            Color color;
            progressBar1.Visible = true;
            for (int y = 0; y < bmp.Height; y++)
            {
                int y1 = 1 + bmp.Height / 100;
                if (y % y1 == 1) progressBar1.PerformStep();

                for (int x = 0; x < bmp.Width; x++)
                {
                    if (nbyteB == 1)  // nbyteB is defined by the PixelFormat of "bmp"
                    {
                        if (nbyteG == 3)
                            color = bmp.Palette.Entries[Grid[3 * (x + bmp.Width * y)]]; // Grid is colore
                        else
                        {
                            color = bmp.Palette.Entries[Grid[x + bmp.Width * y]];  // Grid is grayscale
                            rgbValues[x + Math.Abs(bmpData.Stride) * y] = color.R;
                        }
                    }
                    else // nbyteB == 3

                        for (int c = 0; c < nbyteB; c++)
                        {
                            if (nbyteG == 3)
                                rgbValues[c + nbyteB * x + Math.Abs(bmpData.Stride) * y] =
                                                      Grid[c + nbyteB * (x + bmp.Width * y)];
                            else
                                rgbValues[c + nbyteB * x + Math.Abs(bmpData.Stride) * y] =
                                        Grid[x + bmp.Width * y];
                        }
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, length);
            bmp.UnlockBits(bmpData);
        } //****************************** end GridToBitmap ****************************************


        // not called
        private void GridToBitmapSet(Bitmap bmp, byte[] grid, int nbyte)
        // The argument "nByte" specifies the type of "Grid"
        {
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
            {
                MessageBox.Show("GridToBitmapSet: the pixel format of 'bmp' must be 24");
                return;
            }

            progressBar1.Visible = true;
            int y1 = 1 + nLoop * bmp.Height / denomProg;
            for (int y = 0; y < bmp.Height; y++)
            {
                if (((y + 1) % y1) == 0) progressBar1.PerformStep();
                if (nbyte == 3)
                {
                    for (int x = 0; x < bmp.Width; x++)
                        bmp.SetPixel(x, y, Color.FromArgb(grid[nbyte * (x + bmp.Width * y) + 2],
                             grid[nbyte * (x + bmp.Width * y) + 1], grid[nbyte * (x + bmp.Width * y) + 0]));
                }
                else // nbyte == 1
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(grid[x + bmp.Width * y],
                                               grid[x + bmp.Width * y], grid[x + bmp.Width * y]));
                    }
                }
            }
        }//****************************** end GridToBitmapSet ****************************************


        public void GridToBitmapOld(Bitmap bmp, byte[] grid)
        {
            progressBar1.Visible = true;
            int jump, Len = bmp.Height, nStep = 15;
            if (Len > 2 * nStep) jump = Len / nStep;
            else jump = 2;

            for (int y = 0; y < bmp.Height; y++)
            {
                if (y % jump == jump - 1) progressBar1.PerformStep();

                for (int x = 0; x < bmp.Width; x++)
                {
                    bmp.SetPixel(x, y, Color.FromArgb(0, grid[x + bmp.Width * y],
                         grid[x + bmp.Width * y], grid[x + bmp.Width * y]));
                }
            }
            progressBar1.Visible = false;
        } //****************************** end GridToBitmapOld ****************************************


        private void buttonDetectEdges_Click(object sender, EventArgs e) // Detect edges
        {
            if (OPEN == false)
            {
                MessageBox.Show("Please open an image");
                return;
            }

            progressBar1.Visible = true;
            progressBar1.Value = 0;

            sigmaIm.SigmaFilterSimpleUniversal(input: OrigIm, hWind: 1, toleranz: 30, fm1: this);

            if (OrigIm.N_Bits == 24)
            {
                extremeFilteredImage.ExtremeFilterLightColor(input: sigmaIm, hWind: 2, th: 1, fm1: this);
            }
            else
            {
                extremeFilteredImage.ExtremeFilterGrayscale(input: sigmaIm, hWind: 2, fm1: this);
            }

            this.threshold = (int)numericUpDown1.Value;
            //int NX = OrigIm.width;

            int rv;
            rv = combIm.LabelCellsSign(this.threshold, extremeFilteredImage, this);
            rv = combIm.CleanCombNew(16, this);
            edgeIm.CracksToPixel(combIm, this);

            GridToBitmapOld(BmpPictBox2, edgeIm.Grid);

            radioButton1Comb.Visible = true;
            radioButton2Image.Visible = true;
            radioButton1Comb.Checked = false;
            radioButton2Image.Checked = false;
            label5.Visible = true;

            pictureBoxDetectedEdges.Refresh();
        } // ******************* end Detect edges *****************************************


        private void pictureBoxDetectedEdges_MouseClick(object sender, MouseEventArgs e) // DrawComb
        {
            int standX, standY;
            if (!radioButton1Comb.Checked && !radioButton2Image.Checked)
            {
                MessageBox.Show("Please click one of the right radio buttons");
            }

            if (radioButton2Image.Checked)
            {
                pictureBoxOriginalImage.Image = origBmp;
                radioButton1Comb.Checked = false;
                standX = (int)((e.X - marginX) / Scale1);
                standY = (int)((e.Y - marginY) / Scale1);
                label2.Visible = true;
                label3.Visible = true;
                pictureBox3.Visible = true;
                extremeFilteredImage.DrawImageLine(Y: standY, xStart: standX, threshold: this.threshold, sigmaImage: sigmaIm, grid2: combIm.Grid, fm1: this);
            }

            if (radioButton1Comb.Checked)
            {
                standX = (int)((e.X - marginX) / Scale1);
                standY = (int)((e.Y - marginY) / Scale1);
                label2.Visible = false;
                label3.Visible = false;
                pictureBox3.Visible = false;
                combIm.DrawComb(standX: standX, standY: standY, fm1: this);
            }

            if (BmpGraph)
            {
                pictureBoxOriginalImage.Refresh();
                pictureBoxDetectedEdges.Refresh();
            }
        } //***************************** end MouseClick ******************************
    } //****************************** end Form1 *****************************************************
} //******************************** end namespace **************************************************

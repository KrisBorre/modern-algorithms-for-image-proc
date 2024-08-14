using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

// chapter 3: Contrast Enhancement
// page 54
namespace WFpiecewiseLinear
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Piecewise Linear Contrast Enhancement";
        }

        private Bitmap origBmp;
        private Bitmap ContrastBmp;
        private Bitmap BmpPictBox3;
        private CImage originalImage;
        private CImage origGrayLevelImage; // gray level copy for histogram
        private CImage contrastEnhancedImage;
        private int MaxHist, MinGV, MaxGV, nbyte, width, height;
        private int[] LUT = new int[256];
        private int[] histo = new int[256];

        // Waarom is dit publiek?
        public string OpenImageFile;

        private Graphics g2, g3;
        private int cntClick, X1, Y1, X2, Y2;

        // Waarom is dit publiek?
        public Pen myPen, bluePen;

        private bool BMP_Graph;

        private void button1_Click(object sender, EventArgs e) // Open image
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OpenImageFile = openFileDialog1.FileName;
                    origBmp = new Bitmap(OpenImageFile);
                    pictureBoxOriginalImage.Image = origBmp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    return;
                }
            }
            else return;

            width = origBmp.Width;
            height = origBmp.Height;

            originalImage = new CImage(width, height, 24);
            origGrayLevelImage = new CImage(width, height, 8);  // grayscale version of origIm
            contrastEnhancedImage = new CImage(width, height, 24);

            label3.Text = "Opened image:" + openFileDialog1.FileName;
            label3.Visible = true;

            progressBar1.Visible = true;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            if (origBmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                BMP_Graph = false;
                progressBar1.Visible = true;
                Color color;
                int y2 = height / 100 + 1;
                nbyte = 3;
                int jump, Len = height, nStep = 50;
                if (Len > 2 * nStep) jump = Len / nStep;
                else jump = 2;
                for (int y = 0; y < height; y++) //==============================================
                {
                    if ((y % jump) == jump - 1)
                    {
                        progressBar1.PerformStep();
                    }

                    for (int x = 0; x < width; x++)
                    {
                        int i = x + width * y;
                        color = origBmp.GetPixel(i % width, i / width);
                        for (int c = 0; c < nbyte; c++)
                        {
                            if (c == 0) originalImage.Grid[nbyte * i] = color.B;
                            if (c == 1) originalImage.Grid[nbyte * i + 1] = color.G;
                            if (c == 2) originalImage.Grid[nbyte * i + 2] = color.R;
                        }
                    }
                } //=============================== end for ( int y... =========================
            }
            else if (origBmp.PixelFormat == PixelFormat.Format24bppRgb)
            {
                BMP_Graph = true;
                BitmapToGrid(origBmp, originalImage.Grid);
            }
            else
            {
                MessageBox.Show("Form1: Inappropriate pixel format. Returning.");
                return;
            }

            ContrastBmp = new Bitmap(origBmp.Width, origBmp.Height, PixelFormat.Format24bppRgb);
            pictureBoxContrastEnhancedImage.Image = ContrastBmp;
            BmpPictBox3 = new Bitmap(256, 256);
            //pictureBox2.Image = BmpPictBox3;
            if (BMP_Graph)
            {
                g2 = Graphics.FromImage(ContrastBmp);
                g3 = Graphics.FromImage(BmpPictBox3);
            }
            else
            {
                g2 = pictureBoxContrastEnhancedImage.CreateGraphics();
                g3 = pictureBoxHistogram.CreateGraphics();
            }

            // Calculating the histogram:
            origGrayLevelImage.ColorToGrayMC(originalImage, this);

            for (int gv = 0; gv < 256; gv++)
            {
                histo[gv] = 0;
            }
            for (int i = 0; i < width * height; i++)
            {
                histo[origGrayLevelImage.Grid[i]]++;
            }

            MaxHist = 0;

            for (int gv = 0; gv < 256; gv++)
            {
                if (histo[gv] > MaxHist) MaxHist = histo[gv];
            }

            MinGV = 255;
            MaxGV = 0;

            for (MinGV = 0; MinGV < 256; MinGV++)
            {
                if (histo[MinGV] > 0) break;
            }
            for (MaxGV = 255; MaxGV >= 0; MaxGV--)
            {
                if (histo[MaxGV] > 0) break;
            }

            // Drawing the histogram:
            SolidBrush myBrush = new SolidBrush(Color.LightGray);
            Rectangle rect = new Rectangle(0, 0, 256, 256);
            g3.FillRectangle(myBrush, rect);
            myPen = new Pen(Color.Red);
            Pen greenPen = new Pen(Color.Green);
            for (int gv = 0; gv < 256; gv++)
            {
                int hh = histo[gv] * 255 / MaxHist;
                if (histo[gv] > 0 && hh < 1) hh = 1;
                g3.DrawLine(myPen, gv, 255, gv, 255 - hh);
                if (gv == MinGV || gv == MaxGV)
                {
                    g3.DrawLine(greenPen, gv, 255, gv, 255 - hh);
                }
            }
            if (BMP_Graph) pictureBoxHistogram.Image = BmpPictBox3;

            // Calculating the standard LUT:
            int[] LUT = new int[256];
            int X = (MinGV + MaxGV) / 2;
            int Y = 128;
            for (int gv = 0; gv < 256; gv++)
            {
                if (gv <= MinGV) LUT[gv] = 0;
                if (gv > MinGV && gv <= X) LUT[gv] = (gv - MinGV) * Y / (X - MinGV);
                if (gv > X && gv <= MaxGV) LUT[gv] = Y + (gv - X) * (255 - Y) / (MaxGV - X);
                if (gv >= MaxGV) LUT[gv] = 255;
            }

            int yy = 255;
            Pen bluePen = new Pen(Color.Blue);
            g3.DrawLine(bluePen, 0, yy, MinGV, yy);
            g3.DrawLine(bluePen, MinGV, yy, X, yy - Y);
            g3.DrawLine(bluePen, X, yy - Y, MaxGV, 0);
            g3.DrawLine(bluePen, MaxGV, 0, yy, 0);
            if (BMP_Graph) pictureBoxHistogram.Image = BmpPictBox3;
            // nbyte = 3;  origIm and contrastIm are both 24-bit images
            for (int i = 0; i < nbyte * width * height; i++)
            {
                contrastEnhancedImage.Grid[i] = (byte)LUT[(int)originalImage.Grid[i]];
            }

            progressBar1.Visible = true;
            GridToBitmap(ContrastBmp, contrastEnhancedImage.Grid);
            cntClick = 0;
            if (BMP_Graph) pictureBoxHistogram.Image = BmpPictBox3;
            pictureBoxContrastEnhancedImage.Image = ContrastBmp;
            label1.Visible = true;
            progressBar1.Visible = false;
        } //******************************* end Open image ******************************************

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        // making new LUT and the resulting image
        {
            int nbyte = 3;
            SolidBrush myBrush = new SolidBrush(Color.LightGray);
            Rectangle rect = new Rectangle(0, 0, 256, 256);

            cntClick++;
            if (cntClick == 3) cntClick = 1;
            int oldX = -1, oldY = -1, yy;
            Pen redPen = new Pen(Color.Red);
            Pen bluePen = new Pen(Color.Blue);

            if (cntClick == 1)
            {
                X1 = e.X;
                if (X1 < MinGV) X1 = MinGV;
                if (X1 > MaxGV) X1 = MaxGV;
                Y1 = 255 - e.Y; // (X, Y) is the clicked point in the graph of the LUT
                if (X1 != oldX || Y1 != oldY) //-------------------------------------------------------
                {
                    // Calculating the LUT for X1 and Y1:
                    for (int gv = 0; gv <= X1; gv++)
                    {
                        if (gv <= MinGV) LUT[gv] = 0;
                        if (gv > MinGV && gv <= X1) LUT[gv] = (gv - MinGV) * Y1 / (X1 - MinGV);
                        if (LUT[gv] > 255) LUT[gv] = 255;
                    }
                }

                g3.FillRectangle(myBrush, rect);

                for (int gv = 0; gv < 256; gv++)
                {
                    int hh = histo[gv] * 255 / MaxHist;
                    if (histo[gv] > 0 && hh < 1) hh = 1;
                    g3.DrawLine(redPen, gv, 255, gv, 255 - hh);
                }
                yy = 255;
                g3.DrawLine(bluePen, 0, yy - LUT[0], MinGV, yy - LUT[0]);
                g3.DrawLine(bluePen, MinGV, yy - LUT[0], X1, yy - LUT[X1]);
                oldX = X1;
                oldY = Y1;
            } //------------------------ end if (cntClick == 1) --------------------------------------

            if (cntClick == 2)
            {
                X2 = e.X;
                if (X2 < MinGV) X2 = MinGV;
                if (X2 > MaxGV) X2 = MaxGV;
                if (X2 < X1) X2 = X1 + 1;
                Y2 = 255 - e.Y; // (X2, Y2) is the second clicked point in the graph of the LUT
                if (Y2 < Y1) Y2 = Y1 + 1;

                if (X2 != oldX || Y2 != oldY) //-------------------------------------------------------
                {
                    // Calculating the LUT for X2 and Y2:
                    for (int gv = X1 + 1; gv < 256; gv++)
                    {
                        if (gv > X1 && gv <= X2) LUT[gv] = Y1 + (gv - X1) * (Y2 - Y1) / (X2 - X1);
                        if (gv > X2 && gv <= MaxGV) LUT[gv] = Y2 + (gv - X2) * (255 - Y2) / (MaxGV - X2);
                        if (LUT[gv] > 255) LUT[gv] = 255;
                        if (gv >= MaxGV) LUT[gv] = 255;
                    }
                }

                yy = 255;
                g3.DrawLine(bluePen, X1, yy - LUT[X1], X2, yy - LUT[X2]);
                g3.DrawLine(bluePen, X2, yy - LUT[X2], MaxGV, 0);
                g3.DrawLine(bluePen, MaxGV, 0, 255, 0);
                oldX = X2;
                oldY = Y2;
            } //------------------------------- end if (cntClick == 2) ------------------------------

            if (BMP_Graph) pictureBoxHistogram.Refresh();
            if (cntClick == 2)
            {
                makeBigLUT(LUT);
            }
            // Calculating contrastEnhancedImage:
            int[] GV = new int[3];
            int arg, colOld, colNew;
            for (int i = 0; i < nbyte * width * height; i++)
            {
                contrastEnhancedImage.Grid[i] = 0;
            }

            if (cntClick == 2)
            {
                for (int y = 0, yn = 0; y < width * height; y += width, yn += nbyte * width) //=============
                {
                    for (int x = 0, xn = 0; x < width; x++, xn += nbyte)
                    {
                        int lum = origGrayLevelImage.Grid[x + y];
                        for (int c = 0; c < nbyte; c++)
                        {
                            colOld = originalImage.Grid[c + xn + yn]; // xn + yn = nbyte*(x + width * y);
                            arg = (lum << 8) | colOld;
                            colNew = bigLUT[arg];
                            if (colNew > 255) colNew = 255;
                            contrastEnhancedImage.Grid[c + xn + yn] = (byte)colNew;
                        }
                    }
                } //=============================== end for (int y = 0 ... ======================
                  // Calculating "ContrastBmp":
                GridToBitmap(ContrastBmp, contrastEnhancedImage.Grid);
            }
            progressBar1.Visible = false;
            pictureBoxContrastEnhancedImage.Image = ContrastBmp;
            if (cntClick == 2) label2.Visible = true;
        } //******************************** end pictureBox3_MouseDown **************************


        public void BitmapToGrid(Bitmap bmp, byte[] Grid)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            switch (bmp.PixelFormat)
            {
                case PixelFormat.Format24bppRgb: nbyte = 3; break;
                case PixelFormat.Format8bppIndexed: nbyte = 1; break;
                default: MessageBox.Show("BitmapToGrid: Inappropriate pixel format=" + bmp.PixelFormat); return;
            }
            IntPtr ptr = bmpData.Scan0;
            int Str = bmpData.Stride;
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            int jump, Len = bmp.Height, nStep = 50;
            if (Len > 2 * nStep) jump = Len / nStep;
            else jump = 2;
            for (int y = 0; y < bmp.Height; y++)
            {
                if ((y % jump) == jump - 1)
                {
                    progressBar1.PerformStep();
                }

                for (int x = 0; x < bmp.Width; x++)
                {
                    if (nbyte == 1)  // nbyte is global according to the PixelFormat of "bmp"
                    {
                        Color color = bmp.Palette.Entries[rgbValues[x + Math.Abs(bmpData.Stride) * y]];
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
        } //****************************** end BitmapToGrid ****************************************

        /// <summary>
        /// Calculates the bitmap;
        /// Precondition: bmp is not null.
        /// </summary>
        /// <param name="bmp">is the output</param>
        /// <param name="Grid">is used as input</param>
        public void GridToBitmap(Bitmap bmp, byte[] Grid)
        {
            int nbyte;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            switch (bmp.PixelFormat)
            {
                case PixelFormat.Format24bppRgb: nbyte = 3; break;
                case PixelFormat.Format8bppIndexed: nbyte = 1; break;
                default: MessageBox.Show("GridToBitmap: Inappropriate pixel format=" + bmp.PixelFormat); return;
            }
            IntPtr ptr = bmpData.Scan0;
            int size = bmp.Width * bmp.Height;
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            int jump, Len = bmp.Height, nStep = 40;
            if (Len > 2 * nStep) jump = Len / nStep;
            else jump = 2;
            progressBar1.Visible = true;
            for (int y = 0; y < bmp.Height; y++)
            {
                if ((y % jump) == jump - 1)
                {
                    progressBar1.PerformStep();
                }

                for (int x = 0; x < bmp.Width; x++)
                {
                    if (nbyte == 1)  // nbyte is global according to the PixelFormat of "bmp"
                    {
                        Color color = bmp.Palette.Entries[Grid[3 * (x + bmp.Width * y)]];
                        rgbValues[3 * (x + Math.Abs(bmpData.Stride) * y) + 0] = color.B;
                        rgbValues[3 * (x + Math.Abs(bmpData.Stride) * y) + 1] = color.G;
                        rgbValues[3 * (x + Math.Abs(bmpData.Stride) * y) + 2] = color.R;
                    }
                    else
                    {
                        for (int c = 0; c < nbyte; c++)
                        {
                            rgbValues[c + nbyte * x + Math.Abs(bmpData.Stride) * y] = Grid[c + nbyte * (x + bmp.Width * y)];
                        }
                    }
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
        } //****************************** end GridToBitmap ****************************************


        private void button2_Click(object sender, EventArgs e)  // Save result
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string tmpFileName;
                if (dialog.FileName == OpenImageFile)
                {
                    tmpFileName = OpenImageFile.Insert(OpenImageFile.IndexOf("."), "$$$");
                    if (dialog.FileName.Contains(".jpg"))
                    {
                        ContrastBmp.Save(tmpFileName, ImageFormat.Jpeg); // saving tmpFile
                    }
                    else
                    {
                        if (dialog.FileName.Contains(".bmp"))
                        {
                            ContrastBmp.Save(tmpFileName, ImageFormat.Bmp);
                        }
                        else
                        {
                            MessageBox.Show("The file " + dialog.FileName + " has an inappropriate extension. Returning.");
                            return;
                        }
                    }
                    origBmp.Dispose();
                    File.Replace(tmpFileName, OpenImageFile, OpenImageFile.Insert(OpenImageFile.IndexOf("."), "BackUp"));
                    // Replaces the contents of 'OpenImageFile' with the contents of the file 'tmpFileName', 
                    // deleting 'tmpFileName', and creating a backup of the 'OpenImageFile'.
                    origBmp = new Bitmap(OpenImageFile);
                    pictureBoxOriginalImage.Image = origBmp;
                }
                else
                {
                    if (dialog.FileName.Contains(".jpg")) ContrastBmp.Save(dialog.FileName, ImageFormat.Jpeg);
                    else
                      if (dialog.FileName.Contains(".bmp")) ContrastBmp.Save(dialog.FileName, ImageFormat.Bmp);
                    else
                    {
                        MessageBox.Show("The file " + dialog.FileName + " has an inappropriate extension. Returning.");
                        return;
                    }
                }
                MessageBox.Show("The result image saved under " + dialog.FileName);
            }
        } //********************************* end Save result ******************************


        private int[] bigLUT = new int[66000];

        private void makeBigLUT(int[] LUT) // Making bigLUT
        {
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            for (int old_color_intensity = 0; old_color_intensity < 256; old_color_intensity++)
            {
                if ((old_color_intensity % 5) == 0)
                {
                    progressBar1.PerformStep();
                }

                for (int old_lightness = 1; old_lightness < 256; old_lightness++)
                {
                    int new_color_intensity = old_color_intensity * LUT[old_lightness] / old_lightness;
                    int arg = (old_lightness << 8) | old_color_intensity;
                    bigLUT[arg] = new_color_intensity;
                }
            }
        } //************************** end makeBigLUT *******************************


    } //******************************* end Form1 ***********************************************
} //********************************* end namespace *********************************************

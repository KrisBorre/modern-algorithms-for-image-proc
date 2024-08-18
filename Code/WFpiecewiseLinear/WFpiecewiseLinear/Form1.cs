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

        private int MinimumGrayValue;
        private int MaximumGrayValue;

        private Bitmap OriginalBitmap;
        private Bitmap ContrastEnhancedBitmap;
        private Bitmap HistogramBitmap;
        private CImage originalCImage;
        private CImage origGrayLevelCImage; // gray level copy for histogram
        private CImage contrastEnhancedCImage;
        private int MaximumHistogramFrequecy, nbyte, width, height;

        private int[] LUT = new int[256];
        private int[] histogram_frequency = new int[256];

        private string OpenImageFile;

        private Graphics histogramGraphics;
        private int clickCount, X1, Y1, X2, Y2; // (X1, Y1) and (X2, Y2) are the knick points of the piecewise linear curve.

        private bool BMP_Graph;

        private void buttonOpenImage_Click(object sender, EventArgs e) // Open image
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
                    OriginalBitmap = new Bitmap(OpenImageFile);
                    pictureBoxOriginalImage.Image = OriginalBitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    return;
                }
            }
            else return;

            width = OriginalBitmap.Width;
            height = OriginalBitmap.Height;

            originalCImage = new CImage(width, height, 24);
            origGrayLevelCImage = new CImage(width, height, 8);  // grayscale version of origIm
            contrastEnhancedCImage = new CImage(width, height, 24);

            label3.Text = "Opened image:" + openFileDialog1.FileName;
            label3.Visible = true;

            progressBar1.Visible = true;
            progressBar1.Value = 0;
            progressBar1.Step = 1;

            if (OriginalBitmap.PixelFormat == PixelFormat.Format8bppIndexed)
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
                        color = OriginalBitmap.GetPixel(i % width, i / width);
                        for (int c = 0; c < nbyte; c++)
                        {
                            if (c == 0) originalCImage.Grid[nbyte * i] = color.B;
                            if (c == 1) originalCImage.Grid[nbyte * i + 1] = color.G;
                            if (c == 2) originalCImage.Grid[nbyte * i + 2] = color.R;
                        }
                    }
                } //=============================== end for ( int y... =========================
            }
            else if (OriginalBitmap.PixelFormat == PixelFormat.Format24bppRgb)
            {
                BMP_Graph = true;
                BitmapToGrid(OriginalBitmap, originalCImage.Grid);
            }
            else
            {
                MessageBox.Show("Form1: Inappropriate pixel format. Returning.");
                return;
            }

            ContrastEnhancedBitmap = new Bitmap(OriginalBitmap.Width, OriginalBitmap.Height, PixelFormat.Format24bppRgb);
            pictureBoxContrastEnhancedImage.Image = ContrastEnhancedBitmap;
            HistogramBitmap = new Bitmap(256, 256);

            if (BMP_Graph)
            {
                histogramGraphics = Graphics.FromImage(HistogramBitmap);
            }
            else
            {
                histogramGraphics = pictureBoxHistogram.CreateGraphics();
            }

            // Calculating the histogram:
            origGrayLevelCImage.ColorToGrayMC(originalCImage, this);

            for (int gray_value = 0; gray_value < 256; gray_value++)
            {
                histogram_frequency[gray_value] = 0;
            }
            for (int i = 0; i < width * height; i++)
            {
                histogram_frequency[origGrayLevelCImage.Grid[i]]++;
            }

            // Here we determine the Maximum Histogram Frequency.
            MaximumHistogramFrequecy = 0;
            for (int gray_value = 0; gray_value < 256; gray_value++)
            {
                if (histogram_frequency[gray_value] > MaximumHistogramFrequecy)
                {
                    MaximumHistogramFrequecy = histogram_frequency[gray_value];
                }
            }

            MinimumGrayValue = 255;
            MaximumGrayValue = 0;

            // Here we determine the actual minimum gray value and the actual maximum gray value.
            for (MinimumGrayValue = 0; MinimumGrayValue < 256; MinimumGrayValue++)
            {
                if (histogram_frequency[MinimumGrayValue] > 0) break;
            }
            for (MaximumGrayValue = 255; MaximumGrayValue >= 0; MaximumGrayValue--)
            {
                if (histogram_frequency[MaximumGrayValue] > 0) break;
            }

            // Drawing the histogram:
            SolidBrush myBrush = new SolidBrush(Color.LightGray);
            Rectangle rect = new Rectangle(0, 0, 256, 256);
            histogramGraphics.FillRectangle(myBrush, rect);
            Pen redPen = new Pen(Color.Red);
            Pen greenPen = new Pen(Color.Green);
            for (int gray_value = 0; gray_value < 256; gray_value++)
            {
                int hh = histogram_frequency[gray_value] * 255 / MaximumHistogramFrequecy;
                if (histogram_frequency[gray_value] > 0 && hh < 1) hh = 1;
                histogramGraphics.DrawLine(redPen, gray_value, 255, gray_value, 255 - hh);

                if (gray_value == MinimumGrayValue || gray_value == MaximumGrayValue)
                {
                    histogramGraphics.DrawLine(greenPen, gray_value, 255, gray_value, 255 - hh);
                }
            }

            // Calculating the standard LUT:
            int[] LUT = new int[256];
            int X = (MinimumGrayValue + MaximumGrayValue) / 2;
            int Y = 128;

            for (int gray_value = 0; gray_value < 256; gray_value++)
            {
                if (gray_value <= MinimumGrayValue)
                {
                    LUT[gray_value] = 0;
                }

                if (gray_value > MinimumGrayValue && gray_value <= X)
                {
                    LUT[gray_value] = (gray_value - MinimumGrayValue) * Y / (X - MinimumGrayValue);
                }

                if (gray_value > X && gray_value <= MaximumGrayValue)
                {
                    LUT[gray_value] = Y + (gray_value - X) * (255 - Y) / (MaximumGrayValue - X);
                }

                if (gray_value >= MaximumGrayValue)
                {
                    LUT[gray_value] = 255;
                }
            }

            int yy = 255;
            Pen bluePen = new Pen(Color.Blue);
            histogramGraphics.DrawLine(pen: bluePen, x1: 0, y1: yy, x2: MinimumGrayValue, y2: yy);
            histogramGraphics.DrawLine(bluePen, MinimumGrayValue, yy, X, yy - Y);
            histogramGraphics.DrawLine(bluePen, X, yy - Y, MaximumGrayValue, 0);
            histogramGraphics.DrawLine(bluePen, MaximumGrayValue, 0, yy, 0);

            // nbyte = 3;  origIm and contrastIm are both 24-bit images
            for (int i = 0; i < nbyte * width * height; i++)
            {
                contrastEnhancedCImage.Grid[i] = (byte)LUT[(int)originalCImage.Grid[i]];
            }

            progressBar1.Visible = true;
            GridToBitmap(ContrastEnhancedBitmap, contrastEnhancedCImage.Grid);
            clickCount = 0;

            if (BMP_Graph)
            {
                pictureBoxHistogram.Image = HistogramBitmap;
            }

            pictureBoxContrastEnhancedImage.Image = ContrastEnhancedBitmap;
            label1.Visible = true;
            progressBar1.Visible = false;
        } //******************************* end Open image ******************************************

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        // making new LUT and the resulting image
        {
            int nbyte = 3;
            SolidBrush myBrush = new SolidBrush(Color.LightGray);
            Rectangle rect = new Rectangle(0, 0, 256, 256);

            clickCount++;
            if (clickCount == 3) clickCount = 1;
            int oldX = -1, oldY = -1, yy;
            Pen redPen = new Pen(Color.Red);
            Pen bluePen = new Pen(Color.Blue);

            if (clickCount == 1)
            {
                X1 = e.X;
                if (X1 < MinimumGrayValue) X1 = MinimumGrayValue;
                if (X1 > MaximumGrayValue) X1 = MaximumGrayValue;
                Y1 = 255 - e.Y; // (X, Y) is the clicked point in the graph of the LUT
                if (X1 != oldX || Y1 != oldY) //-------------------------------------------------------
                {
                    // Calculating the LUT for X1 and Y1:
                    for (int gray_value = 0; gray_value <= X1; gray_value++)
                    {
                        if (gray_value <= MinimumGrayValue)
                        {
                            LUT[gray_value] = 0;
                        }

                        if (gray_value > MinimumGrayValue && gray_value <= X1)
                        {
                            LUT[gray_value] = (gray_value - MinimumGrayValue) * Y1 / (X1 - MinimumGrayValue);
                        }

                        if (LUT[gray_value] > 255)
                        {
                            LUT[gray_value] = 255;
                        }
                    }
                }

                histogramGraphics.FillRectangle(myBrush, rect);

                for (int gray_value = 0; gray_value < 256; gray_value++)
                {
                    int hh = histogram_frequency[gray_value] * 255 / MaximumHistogramFrequecy;
                    if (histogram_frequency[gray_value] > 0 && hh < 1) hh = 1;
                    histogramGraphics.DrawLine(pen: redPen, x1: gray_value, y1: 255, x2: gray_value, y2: 255 - hh);
                }

                yy = 255;
                histogramGraphics.DrawLine(pen: bluePen, x1: 0, y1: yy - LUT[0], x2: MinimumGrayValue, y2: yy - LUT[0]);
                histogramGraphics.DrawLine(pen: bluePen, x1: MinimumGrayValue, y1: yy - LUT[0], x2: X1, y2: yy - LUT[X1]);
                oldX = X1;
                oldY = Y1;
            } //------------------------ end if (cntClick == 1) --------------------------------------

            if (clickCount == 2)
            {
                X2 = e.X;
                if (X2 < MinimumGrayValue)
                {
                    X2 = MinimumGrayValue;
                }
                if (X2 > MaximumGrayValue)
                {
                    X2 = MaximumGrayValue;
                }

                if (X2 < X1)
                {
                    X2 = X1 + 1;
                }

                Y2 = 255 - e.Y; // (X2, Y2) is the second clicked point in the graph of the LUT

                if (Y2 < Y1)
                {
                    Y2 = Y1 + 1;
                }

                if (X2 != oldX || Y2 != oldY) //-------------------------------------------------------
                {
                    // Calculating the LUT for X2 and Y2:
                    for (int gray_value = X1 + 1; gray_value < 256; gray_value++)
                    {
                        if (gray_value > X1 && gray_value <= X2)
                        {
                            LUT[gray_value] = Y1 + (gray_value - X1) * (Y2 - Y1) / (X2 - X1);
                        }

                        if (gray_value > X2 && gray_value <= MaximumGrayValue)
                        {
                            LUT[gray_value] = Y2 + (gray_value - X2) * (255 - Y2) / (MaximumGrayValue - X2);
                        }

                        if (LUT[gray_value] > 255)
                        {
                            LUT[gray_value] = 255;
                        }

                        if (gray_value >= MaximumGrayValue)
                        {
                            LUT[gray_value] = 255;
                        }
                    }
                }

                yy = 255;
                histogramGraphics.DrawLine(pen: bluePen, x1: X1, y1: yy - LUT[X1], x2: X2, y2: yy - LUT[X2]);
                histogramGraphics.DrawLine(bluePen, X2, yy - LUT[X2], MaximumGrayValue, 0);
                histogramGraphics.DrawLine(bluePen, MaximumGrayValue, 0, 255, 0);

                oldX = X2;
                oldY = Y2;
            } //------------------------------- end if (cntClick == 2) ------------------------------

            if (BMP_Graph)
            {
                pictureBoxHistogram.Refresh();
            }

            if (clickCount == 2)
            {
                makeBigLUT(LUT);
            }

            // Calculating contrastEnhancedImage:
            //int[] GV = new int[3];
            int arg, colOld, colNew;
            for (int i = 0; i < nbyte * width * height; i++)
            {
                contrastEnhancedCImage.Grid[i] = 0;
            }

            if (clickCount == 2)
            {
                for (int y = 0, yn = 0; y < width * height; y += width, yn += nbyte * width) //=============
                {
                    for (int x = 0, xn = 0; x < width; x++, xn += nbyte)
                    {
                        int lum = origGrayLevelCImage.Grid[x + y];
                        for (int c = 0; c < nbyte; c++)
                        {
                            colOld = originalCImage.Grid[c + xn + yn]; // xn + yn = nbyte*(x + width * y);
                            arg = (lum << 8) | colOld;
                            colNew = bigLUT[arg];
                            if (colNew > 255) colNew = 255;
                            contrastEnhancedCImage.Grid[c + xn + yn] = (byte)colNew;
                        }
                    }
                } //=============================== end for (int y = 0 ... ======================
                  // Calculating "ContrastBmp":
                GridToBitmap(ContrastEnhancedBitmap, contrastEnhancedCImage.Grid);
            }
            progressBar1.Visible = false;
            pictureBoxContrastEnhancedImage.Image = ContrastEnhancedBitmap;
            if (clickCount == 2) label2.Visible = true;
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


        private void buttonSaveResult_Click(object sender, EventArgs e)  // Save result
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
                        ContrastEnhancedBitmap.Save(tmpFileName, ImageFormat.Jpeg); // saving tmpFile
                    }
                    else
                    {
                        if (dialog.FileName.Contains(".bmp"))
                        {
                            ContrastEnhancedBitmap.Save(tmpFileName, ImageFormat.Bmp);
                        }
                        else
                        {
                            MessageBox.Show("The file " + dialog.FileName + " has an inappropriate extension. Returning.");
                            return;
                        }
                    }
                    OriginalBitmap.Dispose();
                    File.Replace(tmpFileName, OpenImageFile, OpenImageFile.Insert(OpenImageFile.IndexOf("."), "BackUp"));
                    // Replaces the contents of 'OpenImageFile' with the contents of the file 'tmpFileName', 
                    // deleting 'tmpFileName', and creating a backup of the 'OpenImageFile'.
                    OriginalBitmap = new Bitmap(OpenImageFile);
                    pictureBoxOriginalImage.Image = OriginalBitmap;
                }
                else
                {
                    if (dialog.FileName.Contains(".jpg")) ContrastEnhancedBitmap.Save(dialog.FileName, ImageFormat.Jpeg);
                    else
                      if (dialog.FileName.Contains(".bmp")) ContrastEnhancedBitmap.Save(dialog.FileName, ImageFormat.Bmp);
                    else
                    {
                        MessageBox.Show("The file " + dialog.FileName + " has an inappropriate extension. Returning.");
                        return;
                    }
                }
                MessageBox.Show("The result image saved under " + dialog.FileName);
            }
        } //********************************* end Save result ******************************


        private int[] bigLUT = new int[66000]; // 65536 + 256 = 65792

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
                    // bytes 3 and 4 are not used.
                    int arg = (old_lightness << 8) | old_color_intensity;
                    bigLUT[arg] = new_color_intensity;
                }
            }
        } //************************** end makeBigLUT *******************************


    } //******************************* end Form1 ***********************************************
} //********************************* end namespace *********************************************

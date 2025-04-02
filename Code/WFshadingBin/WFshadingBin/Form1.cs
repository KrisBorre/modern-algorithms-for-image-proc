using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

// Shading correction
// page 66
namespace WFshadingBin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Shading correction";
        }

        private Bitmap original_Bitmap;
        private Bitmap subtraction_Bitmap;
        private Bitmap division_Bitmap;

        private CImage origImage;
        private CImage sigmaFilteredCImage;
        private CImage subtractionImage;
        private CImage divisionImage;
        private CImage grayImage;
        private CImage meanImage;
        private CImage binairImage;

        int width, height, nbyteBmp, nbyteIm, threshold, threshold1;
        bool SHADING = false;
        //double ScaleX, ScaleY;
        //double Scale1;
        //int marginX, marginY;
        string openImageFile;

        private void button1_Click(object sender, EventArgs e) // Open image
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            buttonShadingCorrection.Visible = false;
            buttonSaveSubtraction.Visible = false;
            buttonSaveDivision.Visible = false;
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    original_Bitmap = new Bitmap(openFileDialog1.FileName);
                    openImageFile = openFileDialog1.FileName;
                    pictureBox1.Image = original_Bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " +
                     ex.Message);
                }
            }
            else return;

            label1.Visible = true;
            label2.Visible = true;
            label5.Visible = true;
            label5.Text = "Opened image:" + openImageFile;
            label6.Visible = true;
            buttonShadingCorrection.Visible = true;
            numericUpDownWindow.Visible = true;
            numericUpDownLightness.Visible = true;

            width = original_Bitmap.Width;
            height = original_Bitmap.Height;

            progressBar1.Visible = true;
            progressBar1.Value = 0;

            if (original_Bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                nbyteBmp = 1;
            }
            else
            {
                if (original_Bitmap.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    nbyteBmp = 3;
                }
                else
                {
                    MessageBox.Show("Pixel format=" + original_Bitmap.PixelFormat + " not used in this project");
                    return;
                }
            }

            nbyteIm = BitmapToImage(original_Bitmap, ref origImage);
            int N_Bits = nbyteIm * 8;
            sigmaFilteredCImage = new CImage(width, height, N_Bits);
            subtractionImage = new CImage(width, height, N_Bits);
            divisionImage = new CImage(width, height, N_Bits);
            grayImage = new CImage(width, height, 8);
            meanImage = new CImage(width, height, 8);
            binairImage = new CImage(width, height, 8);

            sigmaFilteredCImage.SigmaFilterSimpleUni(input: origImage, hWind: 1, Toleranz: 30);

            if (origImage.N_Bits == 24)
            {
                grayImage.ColorToGray(sigmaFilteredCImage, this);
            }
            else
            {
                grayImage.Copy(sigmaFilteredCImage);
            }

            threshold1 = -1;
            threshold = -1;
            //ScaleX = (double)pictureBox1.Width / (double)width;
            //ScaleY = (double)pictureBox1.Height / (double)height;
            //Scale1 = Math.Min(ScaleX, ScaleY);
            //marginX = (pictureBox1.Width - (int)(Scale1 * width)) / 2;
            //marginY = (pictureBox1.Height - (int)(Scale1 * height)) / 2;
            progressBar1.Visible = false;

            SHADING = false;
        } //****************************** end Open image ***************************


        public int BitmapToImage(Bitmap bmp, ref CImage image)
        // Converts any bitmap to a color or to a grayscale image.
        {
            int nbyteIm = 1, rv = 0, x, y;
            Color color;

            if (nbyteBmp == 1)  // nbyteBmp is member of "Form1" according to the PixelFormat of "bmp"
            {
                x = 10;
                y = 2;
                color = bmp.GetPixel(x, y);
                if (color.R != color.G) nbyteIm = 3;
                image = new CImage(bmp.Width, bmp.Height, nbyteIm * 8);

                progressBar1.Visible = true;
                progressBar1.Value = 0;

                for (y = 0; y < bmp.Height; y++) //========================================================
                {
                    int jump = bmp.Height / 100;
                    if (y % jump == jump - 1) progressBar1.PerformStep();

                    for (x = 0; x < bmp.Width; x++) //======================================================
                    {
                        color = bmp.GetPixel(x, y);
                        if (nbyteIm == 3)
                        {
                            image.Grid[3 * (x + bmp.Width * y) + 0] = color.B;
                            image.Grid[3 * (x + bmp.Width * y) + 1] = color.G;
                            image.Grid[3 * (x + bmp.Width * y) + 2] = color.R;
                        }
                        else // nbyteIm == 1:
                        {
                            image.Grid[x + bmp.Width * y] = color.R;
                        }
                    } //================================== end for (x ... ===================================
                } //==================================== end for (y ... =====================================

                rv = nbyteIm;
            }
            else // nbyteBmp == 3 and nbyteIm == 3:
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int Str = bmpData.Stride;
                int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                nbyteIm = 3;
                image = new CImage(bmp.Width, bmp.Height, nbyteIm * 8);

                for (y = 0; y < bmp.Height; y++) //=============================================
                {
                    int jump = bmp.Height / 100;
                    if (y % jump == jump - 1) progressBar1.PerformStep();

                    for (x = 0; x < bmp.Width; x++)
                    {
                        for (int c = 0; c < nbyteIm; c++)
                        {
                            image.Grid[c + nbyteIm * (x + bmp.Width * y)] = rgbValues[c + nbyteBmp * x + Math.Abs(bmpData.Stride) * y];
                        }
                    }
                } //========================= end for (y = 0; ... ============================== 

                rv = nbyteIm;
                bmp.UnlockBits(bmpData);
            }
            return rv;
        } //****************************** end BitmapToImage ****************************************

        // not called
        private int MessReturn(string s)
        {
            if (MessageBox.Show(s, "Return", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return -1;
            return 1;
        }


        private int ImageToBitmapNew(CImage image, Bitmap bmp, int progPart)
        // Any image and color bitmap.
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
            {
                if (image.MessReturn("ImageToBitmapNew: we don't use this pixel format") < 0) return -1;
            }

            IntPtr ptr = bmpData.Scan0;
            int size = bmp.Width * bmp.Height;
            int length = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[length];

            progressBar1.Visible = true;
            int nbyteIm = image.N_Bits / 8;

            for (int y = 0; y < bmp.Height; y++) //=================================================================
            {
                int jump = bmp.Height / progPart;
                if (y % jump == jump - 1) progressBar1.PerformStep();

                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = Color.FromArgb(0, 0, 0);

                    if (nbyteIm == 3)
                    {
                        color = Color.FromArgb(image.Grid[2 + 3 * (x + image.width * y)], image.Grid[1 + 3 * (x + image.width * y)], image.Grid[0 + 3 * (x + image.width * y)]);
                    }
                    else
                    {
                        color = Color.FromArgb(image.Grid[x + image.width * y], image.Grid[x + image.width * y], image.Grid[x + image.width * y]);
                    }

                    rgbValues[3 * x + Math.Abs(bmpData.Stride) * y + 0] = color.B;
                    rgbValues[3 * x + Math.Abs(bmpData.Stride) * y + 1] = color.G;
                    rgbValues[3 * x + Math.Abs(bmpData.Stride) * y + 2] = color.R;
                } //==================================== end for (int x ... =============================
            }  //===================================== end for (int y ... ===============================

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, length);
            bmp.UnlockBits(bmpData);
            return 1;
        } //****************************** end ImageToBitmapNew ****************************************


        private int Round(double x)
        {
            if (x < 0.0) return (int)(x - 0.5);
            return (int)(x + 0.5);
        }

        /// <summary>
        /// calculates the image with corrected shading in two ways.
        /// </summary>
        public void CorrectShading()
        {
            int c, i, x, y;
            int[] colorDiv = { 0, 0, 0 };
            int[] colorSub = { 0, 0, 0 };
            int Lightness = (int)numericUpDownLightness.Value;
            int hWind = (int)(numericUpDownWindow.Value * width / 2000);
            meanImage.FastAverageM(input: this.grayImage, halfWidthOfAveragingWindow: hWind, fm1: this);
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            pictureBox5.Visible = true;

            int[] histoSub = new int[256];
            int[] histoDiv = new int[256];
            for (i = 0; i < 256; i++) histoSub[i] = histoDiv[i] = 0;
            byte lumDiv = 0;
            byte lumSub = 0;
            int jump = height / 17; // width and height are properties of Form1
            for (y = 0; y < height; y++) //==================================================
            {
                if (y % jump == jump - 1) progressBar1.PerformStep();
                for (x = 0; x < width; x++)
                {                               // nbyteIm is member of 'Form1'
                    for (c = 0; c < nbyteIm; c++) //==============================================
                    {
                        colorDiv[c] = Round(sigmaFilteredCImage.Grid[c + nbyteIm * (x + width * y)] * Lightness / (double)meanImage.Grid[x + width * y]); // Division

                        if (colorDiv[c] < 0) colorDiv[c] = 0;
                        if (colorDiv[c] > 255) colorDiv[c] = 255;
                        divisionImage.Grid[c + nbyteIm * (x + width * y)] = (byte)colorDiv[c];

                        colorSub[c] = sigmaFilteredCImage.Grid[c + nbyteIm * (x + width * y)] + Lightness - meanImage.Grid[x + width * y]; // Subtraction

                        if (colorSub[c] < 0) colorSub[c] = 0;
                        if (colorSub[c] > 255) colorSub[c] = 255;
                        subtractionImage.Grid[c + nbyteIm * (x + width * y)] = (byte)colorSub[c];
                    } //======================= end for (c... ==================================

                    if (nbyteIm == 1)
                    {
                        lumDiv = (byte)colorDiv[0];
                        lumSub = (byte)colorSub[0];
                    }
                    else
                    {
                        lumDiv = sigmaFilteredCImage.MaxC((byte)colorDiv[2], (byte)colorDiv[1], (byte)colorDiv[0]);
                        lumSub = sigmaFilteredCImage.MaxC((byte)colorSub[2], (byte)colorSub[1], (byte)colorSub[0]);
                    }

                    histoDiv[lumDiv]++;
                    histoSub[lumSub]++;
                }
            } //============================ end for (y... ===================================

            // Calculating  MinLight and MaxLight for 'Div':
            int MaxLightDiv, MaxLightSub, MinLightDiv, MinLightSub, Sum = 0;
            for (MinLightDiv = 0; MinLightDiv < 256; MinLightDiv++)
            {
                Sum += histoDiv[MinLightDiv];
                if (Sum > width * height / 100) break;
            }
            Sum = 0;
            for (MaxLightDiv = 255; MaxLightDiv >= 0; MaxLightDiv--)
            {
                Sum += histoDiv[MaxLightDiv];
                if (Sum > width * height / 100) break;
            }

            // Calculating  MinLight and MaxLight for 'Sub':
            Sum = 0;
            for (MinLightSub = 0; MinLightSub < 256; MinLightSub++)
            {
                Sum += histoSub[MinLightSub];
                if (Sum > width * height / 100) break;
            }
            Sum = 0;
            for (MaxLightSub = 255; MaxLightSub >= 0; MaxLightSub--)
            {
                Sum += histoSub[MaxLightSub];
                if (Sum > width * height / 100) break;
            }

            // Calculating LUT for 'Div':
            byte[] LUT_division = new byte[256];
            for (i = 0; i < 256; i++)
            {
                if (i <= MinLightDiv) LUT_division[i] = 0;
                else if (i > MinLightDiv && i <= MaxLightDiv)
                {
                    LUT_division[i] = (byte)(255 * (i - MinLightDiv) / (MaxLightDiv - MinLightDiv));
                }
                else LUT_division[i] = 255;
            }

            // Calculating LUTsub for 'Sub':
            byte[] LUT_subtraction = new byte[256];
            for (i = 0; i < 256; i++)
            {
                if (i <= MinLightSub) LUT_subtraction[i] = 0;
                else if (i > MinLightSub && i <= MaxLightSub)
                {
                    LUT_subtraction[i] = (byte)(255 * (i - MinLightSub) / (MaxLightSub - MinLightSub));
                }
                else LUT_subtraction[i] = 255;
            }

            // Calculating contrasted "Div" and "Sub":
            for (i = 0; i < 256; i++) histoDiv[i] = histoSub[i] = 0;
            jump = width * height / 17;

            for (i = 0; i < width * height; i++) //====================================
            {
                if (i % jump == jump - 1) progressBar1.PerformStep();

                for (c = 0; c < nbyteIm; c++)
                {
                    divisionImage.Grid[c + nbyteIm * i] = LUT_division[divisionImage.Grid[c + nbyteIm * i]];
                    subtractionImage.Grid[c + nbyteIm * i] = LUT_subtraction[subtractionImage.Grid[c + nbyteIm * i]];
                }

                if (nbyteIm == 1)
                {
                    lumDiv = divisionImage.Grid[0 + nbyteIm * i];
                    lumSub = subtractionImage.Grid[0 + nbyteIm * i];
                }
                else
                {
                    lumDiv = sigmaFilteredCImage.MaxC(divisionImage.Grid[2 + nbyteIm * i], divisionImage.Grid[1 + nbyteIm * i], divisionImage.Grid[0 + nbyteIm * i]);

                    lumSub = sigmaFilteredCImage.MaxC(subtractionImage.Grid[2 + nbyteIm * i], subtractionImage.Grid[1 + nbyteIm * i], subtractionImage.Grid[0 + nbyteIm * i]);
                }

                histoDiv[lumDiv]++;
                histoSub[lumSub]++;
            } //========================== end for (i = 0; ... ==============================

            // Displaying the histograms:
            Bitmap BmpPictBox4 = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            Graphics g4 = Graphics.FromImage(BmpPictBox4);
            pictureBox4.Image = BmpPictBox4;

            Bitmap BmpPictBox5 = new Bitmap(pictureBox5.Width, pictureBox5.Height);
            Graphics g5 = Graphics.FromImage(BmpPictBox5);

            pictureBox5.Image = BmpPictBox5;
            int MaxHisto1 = 0, SecondMax1 = 0;
            int MaxHisto = 0, SecondMax = 0;

            for (i = 0; i < 256; i++)
            {
                if (histoSub[i] > MaxHisto1)
                {
                    MaxHisto1 = histoSub[i];
                }

                if (histoDiv[i] > MaxHisto)
                {
                    MaxHisto = histoDiv[i];
                }
            }

            for (i = 0; i < 256; i++)
            {
                if (histoSub[i] != MaxHisto1 && histoSub[i] > SecondMax1)
                {
                    SecondMax1 = histoSub[i];
                }
            }

            MaxHisto1 = SecondMax1 * 4 / 3;

            for (i = 0; i < 256; i++)
            {
                if (histoDiv[i] != MaxHisto && histoDiv[i] > SecondMax)
                {
                    SecondMax = histoDiv[i];
                }
            }

            MaxHisto = SecondMax * 4 / 3;

            Pen redPen = new Pen(Color.Red);
            Pen greenPen = new Pen(Color.Green);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            Rectangle Rect1 = new Rectangle(0, 0, pictureBox4.Width, pictureBox4.Height);
            g4.FillRectangle(whiteBrush, Rect1);
            Rectangle Rect = new Rectangle(0, 0, pictureBox5.Width, pictureBox5.Height);
            g5.FillRectangle(whiteBrush, Rect);

            //Drawing the histograms:
            for (i = 0; i < 256; i++)
            {
                g4.DrawLine(redPen, i, pictureBox4.Height - histoSub[i] * 200 / MaxHisto1, i, pictureBox4.Height);
                g5.DrawLine(redPen, i, pictureBox5.Height - histoDiv[i] * 200 / MaxHisto, i, pictureBox5.Height);
            }
            // Vertical lines in histogram:
            for (i = 0; i < 256; i += 50)
            {
                g4.DrawLine(greenPen, i, pictureBox4.Height - 200, i, pictureBox4.Height);
                g5.DrawLine(greenPen, i, pictureBox5.Height - 200, i, pictureBox5.Height);
            }
            pictureBox4.Image = BmpPictBox4;
            pictureBox5.Image = BmpPictBox5;

        } //***************************** end CorrectShading **********************************************


        private void button2_Click(object sender, EventArgs e) // Shading correction
        {
            progressBar1.Value = 0;
            CorrectShading();

            subtraction_Bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            division_Bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            ImageToBitmapNew(subtractionImage, subtraction_Bitmap, 33);
            ImageToBitmapNew(divisionImage, division_Bitmap, 33);

            progressBar1.Visible = false;
            pictureBox2.Image = subtraction_Bitmap;
            pictureBox3.Image = division_Bitmap;
            label3.Text = "If shading OK click threshold for 'SubIm'";
            label3.Visible = true;
            label4.Text = "If shading OK click threshold for 'DivIm'";
            label4.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;

            SHADING = true;
        } //*********************************** end Shading correction ****************************


        private void Save(SaveFileDialog dialog, string OpenImageFile, Bitmap Bmp)
        {
            string tmpFileName;

            if (dialog.FileName == OpenImageFile)
            {
                tmpFileName = OpenImageFile.Insert(OpenImageFile.IndexOf("."), "$$$");

                if (dialog.FileName.Contains(".jpg"))
                {
                    Bmp.Save(tmpFileName, ImageFormat.Jpeg); // saving tmpFile
                }
                else
                {
                    if (dialog.FileName.Contains(".bmp"))
                    {
                        Bmp.Save(tmpFileName, ImageFormat.Bmp);
                    }
                    else
                    {
                        MessageBox.Show("The file " + dialog.FileName + " has an inappropriate extension. Returning.");
                        return;
                    }
                }

                original_Bitmap.Dispose();
                File.Replace(tmpFileName, OpenImageFile, OpenImageFile.Insert(OpenImageFile.IndexOf("."), "BackUp"));

                original_Bitmap = new Bitmap(OpenImageFile);
                pictureBox1.Image = original_Bitmap;
            }
            else
            {
                if (dialog.FileName.Contains(".jpg"))
                {
                    Bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                }
                else
                {
                    if (dialog.FileName.Contains(".bmp"))
                    {
                        Bmp.Save(dialog.FileName, ImageFormat.Bmp);
                    }
                    else
                    {
                        MessageBox.Show("The file " + dialog.FileName + " has an inappropriate extension. Returning.");
                        return;
                    }
                }
            }
            MessageBox.Show("The result image saved under " + dialog.FileName);
        } //********************** end Save ************************************************


        private void button4_Click(object sender, EventArgs e) // Save Div
        {
            if (!SHADING)
            {
                MessageBox.Show("Please press the button 'Shading'");
            }

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Save(dialog, openImageFile + "Div", division_Bitmap);
            }
            buttonSaveDivision.Visible = false;
            label4.Visible = false;
        }


        private void pictureBox4_MouseClick(object sender, MouseEventArgs e) // Thresholding Sub
        {
            if (!SHADING)
            {
                MessageBox.Show("Please click the button 'Shading correction'");
                return;
            }
            threshold1 = e.X;
            Graphics g = pictureBox4.CreateGraphics();
            Pen bluePen = new Pen(Color.Blue);
            g.DrawLine(bluePen, threshold1, 0, threshold1, pictureBox4.Height);
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            int nbyte = subtractionImage.N_Bits / 8;
            int jump = height / 100;
            for (int y = 0; y < height; y++)
            {
                if (y % jump == jump - 1) progressBar1.PerformStep();
                for (int x = 0; x < width; x++)
                {
                    int i = x + width * y;
                    if (nbyte == 1)
                    {
                        if (subtractionImage.Grid[i] > threshold1)
                        {
                            binairImage.Grid[i] = 255;
                        }
                        else binairImage.Grid[i] = 0;
                    }
                    else
                    {
                        if (subtractionImage.MaxC(subtractionImage.Grid[2 + 3 * i], subtractionImage.Grid[1 + 3 * i], subtractionImage.Grid[0 + 3 * i]) > threshold1)
                        {
                            binairImage.Grid[i] = 255;
                        }
                        else binairImage.Grid[i] = 0;
                    }
                    subtraction_Bitmap.SetPixel(x, y, Color.FromArgb(binairImage.Grid[i], binairImage.Grid[i], binairImage.Grid[i]));
                }
            }
            pictureBox2.Image = subtraction_Bitmap;
            buttonSaveSubtraction.Visible = true;
            label3.Text = "If threshold OK click 'Save subtraction'";
            label3.Visible = true;
            threshold1 = -1;
            buttonSaveSubtraction.Visible = true;
            progressBar1.Visible = false;

        } //******************************* end pictureBox4_MouseClick ***************************************


        private void pictureBox5_MouseClick(object sender, MouseEventArgs e) // Thresholding DivIm
        {
            if (!SHADING)
            {
                MessageBox.Show("Please click the button 'Shading correction'");
                return;
            }
            threshold = e.X;
            Graphics g = pictureBox5.CreateGraphics();
            Pen bluePen = new Pen(Color.Blue);
            g.DrawLine(bluePen, threshold, 0, threshold, pictureBox5.Height);
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            int nbyte = divisionImage.N_Bits / 8;
            int jump = height / 100;
            for (int y = 0; y < height; y++)
            {
                if (y % jump == jump - 1) progressBar1.PerformStep();
                for (int x = 0; x < width; x++)
                {
                    int i = x + width * y;
                    if (nbyte == 1)
                    {
                        if (divisionImage.Grid[i] > threshold)
                        {
                            binairImage.Grid[i] = 255;
                        }
                        else binairImage.Grid[i] = 0;
                    }
                    else
                    {
                        if (divisionImage.MaxC(divisionImage.Grid[2 + 3 * i], divisionImage.Grid[1 + 3 * i], divisionImage.Grid[0 + 3 * i]) > threshold)
                        {
                            binairImage.Grid[i] = 255;
                        }
                        else binairImage.Grid[i] = 0;
                    }
                    division_Bitmap.SetPixel(x, y, Color.FromArgb(binairImage.Grid[i], binairImage.Grid[i], binairImage.Grid[i]));
                }
            }
            pictureBox3.Image = division_Bitmap;
            buttonSaveDivision.Visible = true;
            label4.Text = "If threshold OK click 'Save division'";
            label4.Visible = true;

            threshold = -1;
            buttonSaveDivision.Visible = true;
            progressBar1.Visible = false;
        } //*************************** end pictureBox5_MouseClick ************************************


        private void button3_Click(object sender, EventArgs e) // Save sub
        {
            if (!SHADING)
            {
                MessageBox.Show("Please press the button 'Shading'");
            }

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Save(dialog, openImageFile + "Sub", subtraction_Bitmap);
            }
            buttonSaveSubtraction.Visible = false;
            label3.Visible = false;

        } //****************************** end Save sub ************************************
    } //******************************** end class Form1 ******************************************
} //********************************** end namespace **********************************************

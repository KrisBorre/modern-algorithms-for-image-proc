using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

// page 81
namespace WFshadBinImpulse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "shading corrected image processing";
        }

        private Bitmap origBmp;
        private Bitmap shadingCorrectedBmp; // result of shading correction
        private Bitmap resultBmp; // result of processing histo

        private CImage originalCImage;  // copy of original image
        private CImage sigmaFilteredCImage;  // sigma filtered original image
        private CImage grayscaleCImage;  // grayscale image for calculating MeanIm
        private CImage localMeanCImage;  // local mean
        private CImage shadingCorrectedCImage;  // shading corrected image and the result
        private CImage impulseCImage;  // shading corrected image and the result
        private CImage binCImage;  // shading corrected image and the result

        public Point[] v = new Point[20]; // corners of excluded rectangles, used in CPnoise Sort

        private int number, maxNumber = 12; // number and max. number of defined elements "v"
        private bool DIV, Drawn = false, OPEN = false, SHAD = false, BIN = false, CHOICE = false;
        private int KIND = 1;
        //private int marginX, marginY;
        private int nbyteIm, nbyteBmp, width, height, threshold;
        //private double ScaleX, ScaleY;
        //private double Scale1;
        private string openImageFile;

        private void buttonOpenImage_Click(object sender, EventArgs e)  // Open image
        {
            pictureBoxOriginalImage.Visible = false;
            pictureBoxProcessesImage.Visible = false;
            pictureBoxThresholding.Visible = true;
            groupBox1.Visible = false;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label12.Visible = false;
                try
                {
                    origBmp = new Bitmap(openFileDialog1.FileName);
                    openImageFile = openFileDialog1.FileName;
                    number = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            else return;

            label12.Text = "Opened image: " + openImageFile;
            label12.Visible = true;
            label11.Visible = true;
            label6.Visible = true;

            buttonShading.Visible = false;
            buttonImpulseNoise.Visible = false;
            buttonSave.Visible = false;
            numericUpDownWindow.Visible = false;
            numericUpDownLightness.Visible = false;
            numericUpDownDeleteDark.Visible = false;
            numericUpDownDeleteLight.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            pictureBoxProcessesImage.Visible = false;
            pictureBoxThresholding.Visible = false;

            groupBox1.Visible = true;

            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;

            if (origBmp.PixelFormat == PixelFormat.Format8bppIndexed) nbyteBmp = 1;
            else
              if (origBmp.PixelFormat == PixelFormat.Format24bppRgb) nbyteBmp = 3;
            else
            {
                MessageBox.Show("Pixel format=" + origBmp.PixelFormat + " not used in this project.");
                return;
            }

            nbyteIm = BitmapToImage(origBmp, ref originalCImage);

            pictureBoxOriginalImage.Visible = true;
            pictureBoxOriginalImage.Image = origBmp;

            width = origBmp.Width;
            height = origBmp.Height;

            int N_Bits = nbyteIm * 8;

            sigmaFilteredCImage = new CImage(width, height, N_Bits);
            sigmaFilteredCImage.SigmaFilterSimpleUni(originalCImage, 2, 50);
            grayscaleCImage = new CImage(width, height, 8);
            if (nbyteIm == 3) grayscaleCImage.ColorToGray(sigmaFilteredCImage);
            else grayscaleCImage.Copy(sigmaFilteredCImage);
            localMeanCImage = new CImage(width, height, 8);
            shadingCorrectedCImage = new CImage(width, height, N_Bits);
            impulseCImage = new CImage(width, height, N_Bits);
            binCImage = new CImage(width, height, 8);

            //ScaleX = (double)pictureBox1.Width / (double)width;
            //ScaleY = (double)pictureBox1.Height / (double)height;
            //Scale1 = Math.Min(ScaleX, ScaleY);
            //marginX = (pictureBox1.Width - (int)(Scale1 * width)) / 2;
            //marginY = (pictureBox1.Height - (int)(Scale1 * height)) / 2;
            shadingCorrectedBmp = new Bitmap(origBmp.Width, origBmp.Height, PixelFormat.Format24bppRgb);

            pictureBoxProcessesImage.Visible = false;
            pictureBoxThresholding.Visible = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            progressBar1.Visible = false;

            KIND = -1;
            OPEN = true;
        } //********************************** end Open image *************************************


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
                            image.Grid[x + bmp.Width * y] = color.R;
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
                        for (int c = 0; c < nbyteIm; c++)
                            image.Grid[c + nbyteIm * (x + bmp.Width * y)] =
                              rgbValues[c + nbyteBmp * x + Math.Abs(bmpData.Stride) * y];
                } //========================= end for (y = 0; ... ============================== 
                rv = nbyteIm;
                bmp.UnlockBits(bmpData);
            }
            return rv;
        } //****************************** end BitmapToImage ****************************************


        private int MessReturn(string s)
        {
            if (MessageBox.Show(s, "Return", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return -1;
            return 1;
        }

        private int Round(double x)
        {
            if (x < 0.0) return (int)(x - 0.5);
            return (int)(x + 0.5);
        }

        // not called
        public int MaxC(int R, int G, int B)
        {
            int max;
            if (R * 0.713 > G) max = (int)(R * 0.713);
            else max = G;
            if (B * 0.527 > max) max = (int)(B * 0.527);
            return max;
        }

        private byte MaxC(byte R, byte G, byte B)
        {
            byte max;
            if (R * 0.713 > G) max = (byte)(R * 0.713);
            else max = G;
            if (B * 0.527 > max) max = (byte)(B * 0.527);
            return max;
        }


        private void CorrectShading(bool DIV)
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }
            int c, i, x, y;
            int[] color = { 0, 0, 0 };
            int Lightness = (int)numericUpDownLightness.Value;
            int hWind = (int)(numericUpDownWindow.Value * width / 1000);

            this.localMeanCImage.FastAverageM(grayscaleCImage, hWind, this);
            //progressBar1.Visible = true;
            this.pictureBoxProcessesImage.Visible = true;
            this.pictureBoxThresholding.Visible = true;
            this.progressBar1.Value = 0;

            int[] histo = new int[256];
            for (i = 0; i < 256; i++) histo[i] = 0;
            byte lum = 0;
            int nbyteIm = sigmaFilteredCImage.N_Bits / 8;
            int jump = height / 50; // width and height are properties of Form1

            for (y = 0; y < height; y++) //==================================================
            {
                if (y % jump == jump - 1)
                {
                    this.progressBar1.PerformStep();
                }

                for (x = 0; x < width; x++)
                {                               // nbyteIm is member of 'Form1'
                    for (c = 0; c < nbyteIm; c++) //==============================================
                    {
                        if (DIV)
                        {
                            color[c] = Round(sigmaFilteredCImage.Grid[c + nbyteIm * (x + width * y)] * Lightness / (double)localMeanCImage.Grid[x + width * y]); // Division
                        }
                        else
                        {
                            color[c] = Round(sigmaFilteredCImage.Grid[c + nbyteIm * (x + width * y)] + Lightness - (double)localMeanCImage.Grid[x + width * y]); // Subtraction
                        }

                        if (color[c] < 0) color[c] = 0;
                        if (color[c] > 255) color[c] = 255;
                        this.shadingCorrectedCImage.Grid[c + nbyteIm * (x + width * y)] = (byte)color[c];
                    } //======================= end for (c... ==================================

                    if (nbyteIm == 1)
                    {
                        lum = (byte)color[0];
                    }
                    else
                    {
                        lum = (byte)this.MaxC((byte)color[2], (byte)color[1], (byte)color[0]);
                    }

                    histo[lum]++;
                }
            } //============================ end for (y... ===================================

            // Calculating  MinLight and MaxLight:
            int MaxLight, MinLight, Sum = 0;
            for (MinLight = 0; MinLight < 256; MinLight++)
            {
                Sum += histo[MinLight];
                if (Sum > width * height / 100) break;
            }
            Sum = 0;
            for (MaxLight = 255; MaxLight >= 0; MaxLight--)
            {
                Sum += histo[MaxLight];
                if (Sum > width * height / 100) break;
            }

            // Calculating LUT:
            byte[] LUT = new byte[256];
            for (i = 0; i < 256; i++)
            {
                if (i <= MinLight)
                {
                    LUT[i] = 0;
                }
                else if (i > MinLight && i <= MaxLight)
                {
                    LUT[i] = (byte)(255 * (i - MinLight) / (MaxLight - MinLight));
                }
                else
                {
                    LUT[i] = 255;
                }
            }

            // Calculating contrasted "ShadIm":
            for (i = 0; i < 256; i++) histo[i] = 0;
            jump = width * height / 50;
            for (i = 0; i < width * height; i++) //====================================
            {
                if (i % jump == jump - 1) this.progressBar1.PerformStep();

                for (c = 0; c < nbyteIm; c++)
                {
                    this.shadingCorrectedCImage.Grid[c + nbyteIm * i] = LUT[this.shadingCorrectedCImage.Grid[c + nbyteIm * i]];
                }

                if (nbyteIm == 1)
                {
                    lum = this.shadingCorrectedCImage.Grid[0 + nbyteIm * i];
                }
                else
                {
                    lum = (byte)this.MaxC(this.shadingCorrectedCImage.Grid[2 + nbyteIm * i], this.shadingCorrectedCImage.Grid[1 + nbyteIm * i], this.shadingCorrectedCImage.Grid[0 + nbyteIm * i]);
                }

                histo[lum]++;
            } //========================== end for (i = 0; ... ==============================

            // Displaying the histograms and the row sections:                        
            Bitmap BmpPictBox3 = new Bitmap(this.pictureBoxThresholding.Width, this.pictureBoxThresholding.Height);
            Graphics g3 = Graphics.FromImage(BmpPictBox3);
            this.pictureBoxThresholding.Image = BmpPictBox3;

            int MaxHisto = 0, SecondMax = 0;
            for (i = 0; i < 256; i++) if (histo[i] > MaxHisto) MaxHisto = histo[i];
            for (i = 0; i < 256; i++) if (histo[i] != MaxHisto && histo[i] > SecondMax) SecondMax = histo[i];
            MaxHisto = SecondMax * 4 / 3;
            Pen redPen = new Pen(Color.Red), bluePen = new Pen(Color.Blue), greenPen = new Pen(Color.LightGreen);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            Rectangle Rect = new Rectangle(0, 0, this.pictureBoxThresholding.Width, this.pictureBoxThresholding.Height);
            this.pictureBoxThresholding.Visible = true;

            g3.FillRectangle(whiteBrush, Rect);

            for (i = 0; i < 256; i++)
            {
                g3.DrawLine(redPen, i, pictureBoxThresholding.Height - 1 - histo[i] * pictureBoxThresholding.Height / MaxHisto, i, pictureBoxThresholding.Height - 1);
            }

            for (i = 0; i < 256; i += 50)
            {
                g3.DrawLine(greenPen, i, pictureBoxThresholding.Height - 200, i, pictureBoxThresholding.Height);
            }

            if (threshold >= 0) g3.DrawLine(bluePen, threshold, pictureBoxThresholding.Height - 200, threshold, pictureBoxThresholding.Height);
            this.pictureBoxThresholding.Visible = true;
            SHAD = true;
        } //***************************** end CorrectShading **********************************************



        private int ImageToBitmapNew(CImage image, Bitmap bmp)
        // Any image and color bitmap.
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
            {
                if (MessReturn("ImageToBitmapNew: we don't use this pixel format=" + bmp.PixelFormat) < 0)
                {
                    return -1;
                }
            }
            IntPtr ptr = bmpData.Scan0;
            int size = bmp.Width * bmp.Height;
            int length = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[length];

            int nbyteIm = image.N_Bits / 8;
            for (int y = 0; y < bmp.Height; y++) //=================================================================
            {
                int jump = bmp.Height / 100;
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


        private void buttonShading_Click(object sender, EventArgs e) // Shading
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }

            if (radioButton1.Checked)
            {
                buttonShading.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label9.Visible = true;
                numericUpDownWindow.Visible = true;
                numericUpDownLightness.Visible = true;
                numericUpDownWindow.Value = 11;  // Window in per mille
                numericUpDownLightness.Value = 255; // Light
                numericUpDownDeleteDark.Value = 50;  // Delete dark
                numericUpDownDeleteLight.Value = 0;   // Delete light

                DIV = true;
                KIND = 1;
                CHOICE = true;
            }
            else if (radioButton2.Checked)
            {
                buttonShading.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label9.Visible = true;
                numericUpDownWindow.Visible = true;
                numericUpDownLightness.Visible = true;
                numericUpDownWindow.Value = 11;  // Window in per mille
                numericUpDownLightness.Value = 140; // Light
                numericUpDownDeleteDark.Value = 0;  // Delete dark
                numericUpDownDeleteLight.Value = 70;   // Delete light

                DIV = false;
                KIND = 2;
                CHOICE = true;
            }
            else if (radioButton3.Checked)
            {
                buttonShading.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label9.Visible = true;
                numericUpDownWindow.Visible = true;
                numericUpDownLightness.Visible = true;
                numericUpDownWindow.Value = 500;  // Window in per mille
                numericUpDownLightness.Value = 128; // Light
                DIV = true;
                KIND = 3;
                CHOICE = true;
            }
            else if (radioButton4.Checked)
            {
                buttonShading.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label9.Visible = true;
                numericUpDownWindow.Visible = true;
                numericUpDownLightness.Visible = true;
                numericUpDownWindow.Value = 11;  // Window in per mille
                numericUpDownLightness.Value = 255; // Light
                DIV = false;
                KIND = 4;
                CHOICE = true;
            }

            if (!CHOICE)
            {
                MessageBox.Show("Please choose a radio button in 'groupBox1'");
                label6.Visible = true;
                groupBox1.Visible = true;
                return;
            }
            else
            {
                label1.Visible = true;
                label2.Visible = true;
                label9.Visible = true;
                buttonShading.Visible = true;
                numericUpDownWindow.Visible = true;
                numericUpDownLightness.Visible = true;
            }

            Shading_Correcting(KIND, this);
            label8.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            label9.Visible = false;
            label10.Text = "Shading corrected";
            label10.Visible = true;

            groupBox1.Visible = false;
            progressBar1.Visible = false;
            SHAD = true;
        } //******************** end button 2 Shading *******************************************


        private void Shading_Correcting(int KIND, Form1 fm1)
        {
            int width = originalCImage.width;
            int hWind = (int)numericUpDownWindow.Value * width / 200;
            grayscaleCImage.ColorToGrayMC(sigmaFilteredCImage, fm1);
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;
            progressBar1.Maximum = 100;

            //hWind = (int)numericUpDown1.Value * OrigIm.width / 200;
            //MeanIm.FastAverageM(grayscaleCImage, hWind, fm1);
            //int[] histo = new int[256];

            this.CorrectShading(DIV);
            ImageToBitmapNew(shadingCorrectedCImage, shadingCorrectedBmp);

            pictureBoxProcessesImage.Visible = true;
            pictureBoxProcessesImage.Image = shadingCorrectedBmp;
        } //****************************** end Shading_Cor ***********************


        private void buttonImpulseNoise_Click(object sender, EventArgs e) // Impulse Noise
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }
            if (!SHAD)
            {
                MessageBox.Show("Please click the button 'Shading'");
                return;
            }
            if (!BIN)
            {
                MessageBox.Show("Please click in the histogram");
                return;
            }
            resultBmp = new Bitmap(origBmp.Width, origBmp.Height, PixelFormat.Format24bppRgb);

            impulseCImage.Copy(binCImage);
            int nbyte = impulseCImage.N_Bits / 8;
            Drawn = true;
            progressBar1.Visible = true;
            impulseCImage.DeleteBit0(nbyte, this);

            int maxLi, minLi;
            int[] histo = new int[256];
            for (int i = 0; i < 256; i++) histo[i] = 0;

            if (nbyte == 3)
            {
                int lum;
                int i1 = origBmp.Width * origBmp.Height / 100 + 1;

                for (int i = 0; i < origBmp.Width * origBmp.Height; i++)
                {
                    lum = MaxC(impulseCImage.Grid[3 * i + 2], impulseCImage.Grid[3 * i + 1], impulseCImage.Grid[3 * i + 0]);
                    histo[lum]++;
                }
            }
            else
            {
                for (int i = 0; i < origBmp.Width * origBmp.Height; i++)
                {
                    histo[impulseCImage.Grid[i]]++;
                }
            }

            for (maxLi = 255; maxLi > 0; maxLi--) if (histo[maxLi] != 0) break;
            for (minLi = 0; minLi < 256; minLi++) if (histo[minLi] != 0) break;

            CPnoise PN = new CPnoise(Histo: histo, Qlength: 1000, Size: 4000);

            PN.Sort(image: impulseCImage, histo: histo, number: number, picBox1Width: pictureBoxOriginalImage.Width, picBox1Height: pictureBoxOriginalImage.Height, fm1: this);

            int maxDark = (int)numericUpDownDeleteDark.Value;
            int maxLight = (int)numericUpDownDeleteLight.Value;

            PN.DarkNoise(ref impulseCImage, minLi, maxLi, maxDark, this);
            impulseCImage.DeleteBit0(nbyte, this);

            PN.LightNoise(ref impulseCImage, minLi, maxLi, maxLight, this);

            for (int i = 0; i < nbyte * origBmp.Width * origBmp.Height; i++)
            {
                if (impulseCImage.Grid[i] == 252 || impulseCImage.Grid[i] == 254)
                {
                    impulseCImage.Grid[i] = 255;
                }
            }

            ImageToBitmapNew(impulseCImage, resultBmp);

            pictureBoxProcessesImage.Image = resultBmp;

            Graphics g = pictureBoxOriginalImage.CreateGraphics();
            Pen myPen = new Pen(Color.LightGray);

            for (int n = 0; n < number; n += 2)
            {
                g.DrawLine(myPen, v[n + 1].X, v[n + 0].Y, v[n + 1].X, v[n + 1].Y);
                g.DrawLine(myPen, v[n + 0].X, v[n + 0].Y, v[n + 1].X, v[n + 0].Y);
                g.DrawLine(myPen, v[n + 0].X, v[n + 0].Y, v[n + 0].X, v[n + 1].Y);
                g.DrawLine(myPen, v[n + 0].X, v[n + 1].Y, v[n + 1].X, v[n + 1].Y);
            }

            progressBar1.Visible = false;

            groupBox1.Visible = true;
            label6.Visible = true;

            label6.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Text = "Impulse noise removed";
            label10.Visible = true;
            buttonSave.Visible = true;

            CHOICE = false;
            pictureBoxThresholding.Visible = true;
        } //********************************** end Impulse noise *******************************


        private void pictureBoxOriginalImage_MouseClick(object sender, MouseEventArgs e) // small rectangles in OrigIm
        {
            Graphics g = pictureBoxOriginalImage.CreateGraphics();
            Pen myPen;

            if (Drawn)
            {
                pictureBoxOriginalImage.Image = origBmp;
                number = 0;
                Drawn = false;
            }

            int X = e.X;
            int Y = e.Y;
            v[number].X = X;
            v[number].Y = Y;
            if (number < maxNumber)
            {
                number++;
            }
            else
            {
                MessageBox.Show("Number=" + number + " is too large");
            }

            myPen = new Pen(Color.Blue);
            if ((number & 1) == 0)
            {
                for (int n = 0; n < number; n += 2)
                {
                    g.DrawLine(myPen, v[n + 1].X, v[n + 0].Y, v[n + 1].X, v[n + 1].Y);
                    g.DrawLine(myPen, v[n + 0].X, v[n + 0].Y, v[n + 1].X, v[n + 0].Y);
                    g.DrawLine(myPen, v[n + 0].X, v[n + 0].Y, v[n + 0].X, v[n + 1].Y);
                    g.DrawLine(myPen, v[n + 0].X, v[n + 1].Y, v[n + 1].X, v[n + 1].Y);
                }
            }
        } //***************************** end pictureBox1_MouseClick ***********************


        private void buttonSave_Click(object sender, EventArgs e) // Save result
        {
            SaveFileDialog dialog = new SaveFileDialog(); //Prompts the user to select a location for saving a file. 
                                                          // This class can either open and overwrite an existing file or create a new file.

            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string tmpFileName;
                if (dialog.FileName == openImageFile)
                {
                    tmpFileName = openImageFile.Insert(openImageFile.IndexOf("."), "$$$");
                    if (dialog.FileName.Contains(".jpg"))
                    {
                        resultBmp.Save(tmpFileName, ImageFormat.Jpeg); // saving tmpFile
                    }
                    else if (dialog.FileName.Contains(".bmp"))
                    {
                        resultBmp.Save(tmpFileName, ImageFormat.Bmp);
                    }
                    else
                    {
                        MessageBox.Show("The file " + dialog.FileName + " has an inappropriate extension. Returning.");
                        return;
                    }

                    origBmp.Dispose();
                    File.Replace(tmpFileName, openImageFile, openImageFile.Insert(openImageFile.IndexOf("."), "BackUp"));
                    origBmp = new Bitmap(openImageFile);
                    pictureBoxOriginalImage.Image = origBmp;
                }
                else
                {
                    if (dialog.FileName.Contains(".jpg"))
                    {
                        resultBmp.Save(dialog.FileName, ImageFormat.Jpeg);
                    }
                    else if (dialog.FileName.Contains(".bmp"))
                    {
                        resultBmp.Save(dialog.FileName, ImageFormat.Bmp);
                    }
                    else
                    {
                        MessageBox.Show("The file " + dialog.FileName + " has an inappropriate extension. Returning.");
                        return;
                    }
                }
                MessageBox.Show("The result image saved under " + dialog.FileName);
            }
        } //**************************** end Save result ***********************


        private void pictureBoxThresholding_MouseClick(object sender, MouseEventArgs e) // Thresholding
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }
            if (!SHAD)
            {
                MessageBox.Show("Please click the button 'Shading'");
                return;
            }

            threshold = e.X;
            Graphics g3 = pictureBoxThresholding.CreateGraphics();
            Pen bluePen = new Pen(Color.Blue);
            g3.DrawLine(bluePen, threshold, pictureBoxThresholding.Height, threshold, pictureBoxThresholding.Height - 200);
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            int nbyte = shadingCorrectedCImage.N_Bits / 8;
            resultBmp = new Bitmap(shadingCorrectedCImage.width, shadingCorrectedCImage.height, PixelFormat.Format24bppRgb);
            int jump = height / 100;

            for (int y = 0; y < height; y++)
            {
                if (y % jump == jump - 1) progressBar1.PerformStep();

                for (int x = 0; x < width; x++)
                {
                    int i = x + width * y;

                    if (nbyte == 1)
                    {
                        if (shadingCorrectedCImage.Grid[i] > threshold)
                        {
                            binCImage.Grid[i] = 255;
                        }
                        else
                        {
                            binCImage.Grid[i] = 0;
                        }
                    }
                    else
                    {
                        if (MaxC(shadingCorrectedCImage.Grid[2 + 3 * i], shadingCorrectedCImage.Grid[1 + 3 * i], shadingCorrectedCImage.Grid[0 + 3 * i]) > threshold)
                        {
                            binCImage.Grid[i] = 255;
                        }
                        else
                        {
                            binCImage.Grid[i] = 0;
                        }
                    }

                    resultBmp.SetPixel(x, y, Color.FromArgb(binCImage.Grid[i], binCImage.Grid[i], binCImage.Grid[i]));
                }
            }

            label7.Visible = true;
            label6.Visible = false;
            groupBox1.Visible = false;
            label8.Visible = false;
            CHOICE = false;
            pictureBoxProcessesImage.Image = resultBmp;
            threshold = -1;
            progressBar1.Visible = false;
            label10.Text = "Thresholded image";
            label10.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            buttonImpulseNoise.Visible = true;
            numericUpDownDeleteDark.Visible = true;
            numericUpDownDeleteLight.Visible = true;

            BIN = true;
        } //**************************** end pict3_MouseClick Thresholding ********************

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            buttonShading.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label9.Visible = true;
            numericUpDownWindow.Visible = true;
            numericUpDownLightness.Visible = true;
            numericUpDownWindow.Value = 11;  // Window in per mille
            numericUpDownLightness.Value = 255; // Light
            numericUpDownDeleteDark.Value = 50;  // Delete dark
            numericUpDownDeleteLight.Value = 0;   // Delete light
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            buttonShading.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label9.Visible = true;
            numericUpDownWindow.Visible = true;
            numericUpDownLightness.Visible = true;
            numericUpDownWindow.Value = 11;  // Window in per mille
            numericUpDownLightness.Value = 140; // Light
            numericUpDownDeleteDark.Value = 0;  // Delete dark
            numericUpDownDeleteLight.Value = 70;   // Delete light
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            buttonShading.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label9.Visible = true;
            numericUpDownWindow.Visible = true;
            numericUpDownLightness.Visible = true;
            numericUpDownWindow.Value = 500;  // Window in per mille
            numericUpDownLightness.Value = 128; // Light
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            buttonShading.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label9.Visible = true;
            numericUpDownWindow.Visible = true;
            numericUpDownLightness.Visible = true;
            numericUpDownWindow.Value = 11;  // Window in per mille
            numericUpDownLightness.Value = 255; // Light
        }

    } //****************************** end class Form1 **************************
} //******************************** end namespace ****************************************



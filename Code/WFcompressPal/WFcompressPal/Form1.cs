using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

// chapter 8: A new method of Image Compression
// page 127
namespace WFcompressPal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap origBmp;
        private Bitmap bmpPictBox2;
        CImage origIm;  // copy of original image
        CImage impulseIm;  // deleted impulse noise
        CImage sigmaIm;  // Sigma filtered
        CImage combIm; // cell complex of the edges
        CImage extremIm;  // result of the extrem filtering
        CImage edgeIm;  // shading corrected image and the result
        CImage restoreIm;  // shading corrected image and the result
        CImage segmentIm;  // shading corrected image and the result
        CImage pal;
        int[] palet;
        public int nCode, fWidth, fHeight;
        bool OPEN = false, IMPULSE = false, SEGMENTED = false, DETECTED = false, CODED = false;
        public iVect2[] V = new iVect2[200]; // corners of excluded rectangles, used in CPnoise Sort

        public double Scale1;
        public int marginX, marginY, nbyteIm, nbyteBmp, Threshold, Version;
        public Graphics g2;
        CListLines list;
        CListCode listCode;


        private void button1_Click(object sender, EventArgs e)  // Open image
        {
            pictureBox2.Visible = false;
            label3.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    origBmp = new Bitmap(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " +
                     ex.Message);
                }
            }
            else return;


            button2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            numericUpDown1.Visible = true;
            numericUpDown2.Visible = true;
            label6.Text = "Opened file: " + openFileDialog1.FileName;
            label6.Visible = true;
            label5.Visible = true;
            label7.Text = "Click 'Impulse noise'";
            label7.Visible = true;

            label3.Visible = false;
            label4.Visible = false;
            button3.Visible = false;
            numericUpDown3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;

            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;

            if (origBmp.PixelFormat == PixelFormat.Format8bppIndexed) nbyteBmp = 1;
            else
              if (origBmp.PixelFormat == PixelFormat.Format24bppRgb) nbyteBmp = 3;
            else
            {
                MessageBox.Show("Pixel format=" + origBmp.PixelFormat + " not used in this project");
                return;
            }
            nbyteIm = BitmapToImage(origBmp, ref origIm); // Defines OrigIm.

            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox1.Image = origBmp;
            label5.Text = "Original image";
            label3.Visible = false;

            double ScaleX = (double)pictureBox1.Width / (double)origIm.width;
            double ScaleY = (double)pictureBox1.Height / (double)origIm.height;
            if (ScaleX < ScaleY) Scale1 = ScaleX;
            else Scale1 = ScaleY;
            marginX = (pictureBox1.Width - (int)(Scale1 * origIm.width)) / 2;
            marginY = (pictureBox1.Height - (int)(Scale1 * origIm.height)) / 2;
            fWidth = origBmp.Width;
            fHeight = origBmp.Height;
            bmpPictBox2 = new Bitmap(origBmp.Width, origBmp.Height, PixelFormat.Format24bppRgb);

            g2 = Graphics.FromImage(bmpPictBox2);
            pictureBox2.Image = bmpPictBox2;
            progressBar1.Visible = false;
            OPEN = true;
        } //***************************** end Open image ****************************************

        // not called
        private void ImageToBitmap(CImage Image, int[] Palet, int nbyteIm, Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            int nbyteBmp;
            switch (bmp.PixelFormat)
            {
                case PixelFormat.Format24bppRgb: nbyteBmp = 3; break;
                case PixelFormat.Format8bppIndexed: nbyteBmp = 1; break;
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
                int jump;
                if (bmp.Height > 300) jump = bmp.Height / 100;
                else jump = 2;
                if (y % jump == jump - 1) progressBar1.PerformStep();

                for (int x = 0; x < bmp.Width; x++)
                {
                    if (nbyteBmp == 1)  // nbyteBmp is defined by the PixelFormat of "bmp"
                    {
                        if (nbyteIm == 3)
                            color = bmp.Palette.Entries[Image.Grid[3 * (x + bmp.Width * y)]]; // Grid is colore
                        else
                        {
                            color = bmp.Palette.Entries[Image.Grid[x + bmp.Width * y]]; // Grid is grayscale
                            rgbValues[x + Math.Abs(bmpData.Stride) * y + 0] = color.B;
                            rgbValues[x + Math.Abs(bmpData.Stride) * y + 1] = color.G;
                            rgbValues[x + Math.Abs(bmpData.Stride) * y + 2] = color.R;
                        }
                    }
                    else // nbyteBmp==3
                        for (int c = 0; c < nbyteBmp; c++)
                        {
                            if (nbyteIm == 3)
                                rgbValues[c + nbyteBmp * x + Math.Abs(bmpData.Stride) * y] =
                                                      Image.Grid[c + nbyteBmp * (x + bmp.Width * y)];
                            else
                            {
                                int Col = Palet[Image.Grid[x + bmp.Width * y]];  // Image is indexed
                                rgbValues[3 * x + Math.Abs(bmpData.Stride * y) + 0] = (byte)((Col >> 16) & 0XFF);
                                rgbValues[3 * x + Math.Abs(bmpData.Stride * y) + 1] = (byte)((Col >> 8) & 0XFF); ;
                                rgbValues[3 * x + Math.Abs(bmpData.Stride * y) + 2] = (byte)(Col & 0XFF); ;
                            }
                        }
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, length);
            bmp.UnlockBits(bmpData);
        } //****************************** end ImageToBitmap ****************************************


        private int MessReturn(string s)
        {
            if (MessageBox.Show(s, "Return", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return -1;
            return 1;
        }

        private int BitmapToImage(Bitmap bmp, ref CImage Image)
        // Converts any bitmap to a color image.
        {
            int nbyteIm = 3, rv = 0, x, y;
            Color color;
            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed) nbyteBmp = 1;
            else
              if (bmp.PixelFormat == PixelFormat.Format24bppRgb) nbyteBmp = 3;
            else
            {
                MessageBox.Show("BitmapToImagePixel format=" + bmp.PixelFormat + " not used in this project");
                return -1;
            }

            if (nbyteBmp == 1)  // nbyteBmp is member of "Form1" according to the PixelFormat of "bmp"
            {
                nbyteIm = 3;
                Image = new CImage(bmp.Width, bmp.Height, nbyteIm * 8); // always a color image

                progressBar1.Visible = true;
                for (y = 0; y < bmp.Height; y++) //========================================================
                {
                    int jump;
                    if (bmp.Height > 300) jump = bmp.Height / 100;
                    else jump = 2;
                    if (y % jump == jump - 1) progressBar1.PerformStep();

                    for (x = 0; x < bmp.Width; x++) //======================================================
                    {
                        color = bmp.GetPixel(x, y);
                        Image.Grid[3 * (x + bmp.Width * y) + 0] = color.B;
                        Image.Grid[3 * (x + bmp.Width * y) + 1] = color.G;
                        Image.Grid[3 * (x + bmp.Width * y) + 2] = color.R;
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
                Image = new CImage(bmp.Width, bmp.Height, nbyteIm * 8);
                for (y = 0; y < bmp.Height; y++) //=============================================
                {
                    int jump;
                    if (bmp.Height > 300) jump = bmp.Height / 100;
                    else jump = 2;
                    if (y % jump == jump - 1) progressBar1.PerformStep();
                    for (x = 0; x < bmp.Width; x++)
                        for (int c = 0; c < nbyteIm; c++)
                            Image.Grid[c + nbyteIm * (x + bmp.Width * y)] =
                              rgbValues[c + nbyteBmp * x + Math.Abs(bmpData.Stride) * y];
                } //========================= end for (y = 0; ... ============================== 
                rv = nbyteIm;
                bmp.UnlockBits(bmpData);
            }
            return rv;
        } //****************************** end BitmapToImage ****************************************

        // not called
        private int Round(double x)
        {
            if (x < 0.0) return (int)(x - 0.5);
            return (int)(x + 0.5);
        }

        private int MaxC(int R, int G, int B)
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



        private void button3_Click(object sender, EventArgs e) // Segment
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }

            if (!IMPULSE)
            {
                MessageBox.Show("Please click 'Impulse noise'");
                return;
            }

            sigmaIm = new CImage(origIm.width, origIm.height, nbyteIm * 8);

            progressBar1.Value = 0;
            sigmaIm.SigmaFilterSimpleUni(impulseIm, 1, 30, this);

            extremIm = new CImage(origIm.width, origIm.height, nbyteIm * 8);
            if (nbyteBmp == 3) extremIm.ExtremeFilterVarColor(sigmaIm, 2, this);
            else extremIm.ExtremeFilterLightUni(sigmaIm, 2, this);

            int rv, x, y;
            palet = new int[256]; // This is a palette containing an RGB int color for each of 256 indices
            pal = new CImage(origIm.width, origIm.height, 8); // This is an indexed image
            if (extremIm.N_Bits == 24) rv = pal.MakePalette(extremIm, palet, this);

            segmentIm = new CImage(sigmaIm.width, sigmaIm.height, 24);
            Bitmap bmp = new Bitmap(sigmaIm.width, sigmaIm.height, PixelFormat.Format24bppRgb);
            Color color;

            int PalColor, jump, value;
            if (sigmaIm.height > 300) jump = sigmaIm.height / (100 / 6);
            else jump = 2;
            progressBar1.Visible = true;
            for (y = 0; y < sigmaIm.height; y++)
            {
                if (y % jump == jump - 1) progressBar1.PerformStep();
                for (x = 0; x < sigmaIm.width; x++)
                {
                    value = pal.Grid[x + sigmaIm.width * y];
                    PalColor = palet[value];
                    color = Color.FromArgb((PalColor) & 255, (PalColor >> 8) & 255, (PalColor >> 16) & 255);
                    segmentIm.Grid[2 + 3 * (x + sigmaIm.width * y)] = (byte)(PalColor & 255);
                    segmentIm.Grid[1 + 3 * (x + sigmaIm.width * y)] = (byte)((PalColor >> 8) & 255);
                    segmentIm.Grid[0 + 3 * (x + sigmaIm.width * y)] = (byte)((PalColor >> 16) & 255);
                }
            }

            ImageToBitmapNew(segmentIm, bmpPictBox2); // SegmentIm is always color image but BmpPictBox2 can be indexed
            pictureBox2.Refresh(); // Image = bmp;

            label3.Text = "Segmented image, table 'Palet'";
            label3.Visible = true;
            pictureBox2.Visible = true;
            progressBar1.Value = 0;
            button4.Visible = true;
            label4.Visible = true;
            numericUpDown3.Visible = true;
            label7.Text = "Click 'Detect edges'";
            label7.Visible = true;
            SEGMENTED = true;
        } //********************************* end Segment ******************************************


        private int ImageToBitmapNew(CImage Image, Bitmap bmp)
        // Any image and color bitmap.
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
                if (MessReturn("ImageToBitmapNew: we don't use this pixel format=" + bmp.PixelFormat) < 0) return -1;
            IntPtr ptr = bmpData.Scan0;
            int size = bmp.Width * bmp.Height;
            int length = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[length];

            int nbyteIm = Image.N_Bits / 8;
            for (int y = 0; y < bmp.Height; y++) //=================================================================
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = Color.FromArgb(0, 0, 0); ;
                    if (nbyteIm == 3)
                    {
                        color = Color.FromArgb(Image.Grid[2 + 3 * (x + Image.width * y)],
                          Image.Grid[1 + 3 * (x + Image.width * y)], Image.Grid[0 + 3 * (x + Image.width * y)]);
                    }
                    else
                    {
                        color = Color.FromArgb(Image.Grid[x + Image.width * y],
                          Image.Grid[x + Image.width * y], Image.Grid[x + Image.width * y]);
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



        private void button2_Click(object sender, EventArgs e) // Impulse noise
        {
            impulseIm = new CImage(origIm.width, origIm.height, 24);

            impulseIm.Copy(origIm);

            int nbyte = impulseIm.N_Bits / 8;
            impulseIm.DeleteBit0(nbyte, this);

            int maxLight, minLight;
            int[] histo = new int[256];
            for (int i = 0; i < 256; i++) histo[i] = 0;

            int light, index;
            byte R = 1, G = 1, B = 1;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            progressBar1.Visible = true;
            int jump, nStep = 15;
            if (impulseIm.height > 2 * nStep) jump = impulseIm.height / nStep;
            else jump = 2;
            for (int y = 0; y < impulseIm.height; y++)
            {
                if (y % jump == jump - 1) progressBar1.PerformStep();
                for (int x = 0; x < impulseIm.width; x++) //======================================================
                {
                    index = x + y * impulseIm.width; // Index of the pixel (x, y)
                    if (nbyte == 1) light = impulseIm.Grid[index];
                    else
                    {
                        R = (byte)(impulseIm.Grid[nbyte * index + 2] & 254);
                        G = (byte)(impulseIm.Grid[nbyte * index + 1] & 254);
                        B = (byte)(impulseIm.Grid[nbyte * index + 0] & 254);
                        light = MaxC(R, G, B);
                    }

                    if (light < 0) light = 0;
                    if (light > 255) light = 255;
                    histo[light]++;
                } //============================ end for (int x = 0; .. ========================================
            } //============================== end for (int y = 0; .. ========================================
            for (maxLight = 255; maxLight > 0; maxLight--) if (histo[maxLight] != 0) break;
            for (minLight = 0; minLight < 256; minLight++) if (histo[minLight] != 0) break;

            CPnoise PN = new CPnoise(histo, 1000, 4000);
            int Number = 0;
            progressBar1.Visible = true;

            PN.Sort(impulseIm, histo, Number, pictureBox1.Width, pictureBox1.Height, this);

            int maxSizeD = (int)numericUpDown1.Value;
            int maxSizeL = (int)numericUpDown2.Value;

            PN.LightNoise(ref impulseIm, minLight, maxLight, maxSizeL, this);
            impulseIm.DeleteBit0(nbyte, this);

            PN.DarkNoise(ref impulseIm, minLight, maxLight, maxSizeD, this);

            progressBar1.Step = 1;
            int Len = nbyte * origBmp.Width * origBmp.Height;
            nStep = 10;
            jump = Len / nStep;
            for (int i = 0; i < nbyte * origBmp.Width * origBmp.Height; i++)
            {
                if ((i % jump) == jump - 1) progressBar1.PerformStep();
                if (impulseIm.Grid[i] == 252 || impulseIm.Grid[i] == 254) impulseIm.Grid[i] = 255;
            }

            ImageToBitmapNew(impulseIm, bmpPictBox2); // ImpulseIm is always color image but BmpPictBox2 can be indexed
            pictureBox2.Image = bmpPictBox2;

            progressBar1.Visible = false;
            pictureBox2.Visible = true;
            label3.Visible = true;
            label3.Text = "Impulse noise suppressed";
            button3.Visible = true;
            label7.Text = "Click 'Segment'";
            label7.Visible = true;
            IMPULSE = true;
        } //********************************* end impulse noise *************************************


        private int ColorDifSign(int iColp, int iColh)
        // Returns the sum of the absolut differences of the color components divided through 3
        // with the sign of MaxC(iColp) - MaxC(iColh).
        {
            int Dif = 0;
            Dif = Math.Abs((iColp & 0xff) - (iColh & 0xff)) + Math.Abs(((iColp >> 8) & 0xff) - ((iColh >> 8) & 0xff)) +
              Math.Abs(((iColp >> 16) & 0xff) - ((iColh >> 16) & 0xff));
            int Sign = 0;
            if (MaxC(iColp & 0xff, (iColp >> 8) & 0xff, (iColp >> 16) & 0xff) -
                     MaxC(iColh & 0xff, (iColh >> 8) & 0xff, (iColh >> 16) & 0xff) > 0) Sign = 1;
            else Sign = -1;
            return (Sign * Dif) / 3;
        }

        // not called
        public int MakeColorDifAr(int[] Palet, short[,] Array)
        // Makes the array with "ColorDifSign" of all pairs of 256 colors.
        {
            int x, y;
            short ColorDiff = 0;
            int Colorx, Colory;
            for (y = 0; y < 256; y++)
                for (x = 0; x < 256; x++)
                {
                    Colory = Palet[y];
                    Colorx = Palet[x];
                    ColorDiff = (short)ColorDifSign(Colory, Colorx);
                    Array[y, x] = ColorDiff;
                }
            return 1;
        }

        private void button4_Click(object sender, EventArgs e) // Detect edges
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }

            if (!IMPULSE)
            {
                MessageBox.Show("Please click 'Impulse noise'");
                return;
            }

            if (!SEGMENTED)
            {
                MessageBox.Show("Please click 'Segment'");
                return;
            }
            //pictureBox2.Visible = false;
            //label3.Visible = false;

            int CombWidth = 2 * sigmaIm.width + 1, CombHeight = 2 * sigmaIm.height + 1;
            combIm = new CImage(CombWidth, CombHeight, 8);

            int Threshold = (int)numericUpDown3.Value;
            combIm.LabelCellsSign(Threshold, extremIm, this);

            int jump, x, y;
            if (extremIm.height > 300) jump = extremIm.height / 25;
            else jump = 3;
            for (y = 0; y < extremIm.height; y++)
            {
                if (y % jump == jump - 1) progressBar1.PerformStep();
                for (x = 0; x < extremIm.width; x++)
                    combIm.Grid[2 * x + 1 + CombWidth * (2 * y + 1)] = pal.Grid[x + fWidth * y];
            }

            combIm.CleanCombNew(20, this);
            combIm.CheckComb(7);

            edgeIm = new CImage(fWidth, fHeight, 8);
            edgeIm.CracksToPixel(combIm, this);
            Bitmap EdgeBmp = new Bitmap(origIm.width, origIm.height, PixelFormat.Format24bppRgb);

            ImageToBitmapNew(edgeIm, bmpPictBox2); // EdgeIm is always color image but BmpPictBox2 can be indexed
            pictureBox2.Image = bmpPictBox2;

            pictureBox2.Image = bmpPictBox2;
            label3.Text = "Detected edges";
            label3.Visible = true;
            button5.Visible = true;
            label7.Text = "Click 'Encode'";
            label7.Visible = true;
            progressBar1.Visible = false;
            DETECTED = true;
        } //***************************** end Detect edges *******************************


        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            int StandX = (int)((e.X - marginX) / Scale1);
            int StandY = (int)((e.Y - marginY) / Scale1);
            if (MessReturn("MouseClick: Version=" + Version) < 0) return;
            switch (Version)
            {
                case 1: combIm.DrawComb(StandX, StandY, this); break;
                case 2: combIm.DrawCombPix(StandX, StandY, this); break;
                case 3: extremIm.DrawImageLine(StandY, StandX, Threshold, sigmaIm, combIm.Grid, this); break;
            }
        }

        private void button5_Click(object sender, EventArgs e) // Encode
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }

            if (!IMPULSE)
            {
                MessageBox.Show("Please click 'Impulse noise'");
                return;
            }

            if (!SEGMENTED)
            {
                MessageBox.Show("Please click 'Segment'");
                return;
            }

            if (!DETECTED)
            {
                MessageBox.Show("Please click 'Detect edges'");
                return;
            }

            list = new CListLines(200000, 200000, 2000000, combIm.width, combIm.height, extremIm.N_Bits);

            int nByte = list.SearchLin(ref combIm, this);
            if (nByte < 0) Application.Exit();

            listCode = new CListCode(origIm.width, origIm.height, origIm.N_Bits, list);

            nCode = listCode.Transform(origIm.width, origIm.height, origIm.N_Bits, palet, combIm, list, this);

            double CompressRate = (double)(origIm.width * origIm.height * (origIm.N_Bits / 8)) / (double)nCode;
            MessageBox.Show("Image encoded. nLine2=" + listCode.nLine2 + " Code length=" + nCode +
              " bytes. Comression rate=" + Math.Round(CompressRate, 1));
            progressBar1.Visible = false;
            button6.Visible = true;
            label7.Text = "Click 'Restore'";
            label7.Visible = true;
            CODED = true;
        } //**************************** end Encode ************************************************


        private void button6_Click(object sender, EventArgs e) // Restore
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }

            if (!IMPULSE)
            {
                MessageBox.Show("Please click 'Impulse noise'");
                return;
            }

            if (!SEGMENTED)
            {
                MessageBox.Show("Please click 'Segment'");
                return;
            }

            if (!DETECTED)
            {
                MessageBox.Show("Please click 'Detect edges'");
                return;
            }

            if (!CODED)
            {
                MessageBox.Show("Please click 'Encode'");
                return;
            }

            CImage MaskIm = new CImage(origBmp.Width, origBmp.Height, 8);
            restoreIm = new CImage(origBmp.Width, origBmp.Height, 24);
            listCode.Restore(ref restoreIm, ref MaskIm, this);

            restoreIm.Smooth(ref MaskIm, false, this);

            ImageToBitmapNew(restoreIm, bmpPictBox2); // RestoreIm is always color image but BmpPictBox2 can be indexed
            pictureBox2.Image = bmpPictBox2;

            pictureBox2.Image = bmpPictBox2;
            pictureBox2.Refresh();
            progressBar1.Visible = false;
            label3.Text = "Restored image";
            label3.Visible = true;
            label7.Text = "Click 'Save code \\DAT'";
            label7.Visible = true;
            button7.Visible = true;

        } //********************************* end Restore *****************************

        private void button7_Click(object sender, EventArgs e) // Save result
        {
            if (!OPEN)
            {
                MessageBox.Show("Please open an image");
                return;
            }

            if (!IMPULSE)
            {
                MessageBox.Show("Please click 'Impulse noise'");
                return;
            }

            if (!SEGMENTED)
            {
                MessageBox.Show("Please click 'Segment'");
                return;
            }

            if (!DETECTED)
            {
                MessageBox.Show("Please click 'Detect edges'");
                return;
            }
            if (!CODED)
            {
                MessageBox.Show("Please click 'Encode'");
                return;
            }
            MessageBox.Show("Do not open an image in the next window but rather find the directory 'DAT'" +
            " and click a file there. If there is no file with a suitable name, then write a name with" + " the extension '.dat' in the bottom line.");

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listCode.WriteCode(dialog.FileName, nCode);
            }

        }//************************* end save result *****************


        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            Version = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Version = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Version = 3;
        }

        // This file is in "for GitHub"
    } //*************************** end class Form1 ******************
} //***************************** end namespace **********************


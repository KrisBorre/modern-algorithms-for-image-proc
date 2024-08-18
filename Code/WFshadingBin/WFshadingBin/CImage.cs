using System;
using System.Windows.Forms;

namespace WFshadingBin
{
    public class CImage
    {
        public byte[] Grid;
        public int width, height, N_Bits;

        /// <summary>
        /// This constructor is not called
        /// </summary>
        public CImage() { } // default constructor

        public CImage(int nx, int ny, int nbits) // constructor
        {
            this.width = nx;
            this.height = ny;
            this.N_Bits = nbits;
            this.Grid = new byte[width * height * (N_Bits / 8)];
        }

        /// <summary>
        /// This constructor is not called
        /// </summary>
        /// <param name="nx"></param>
        /// <param name="ny"></param>
        /// <param name="nbits"></param>
        /// <param name="img"></param>
        public CImage(int nx, int ny, int nbits, byte[] img) // constructor
        {
            this.width = nx;
            this.height = ny;
            this.N_Bits = nbits;
            this.Grid = new byte[width * height * (N_Bits / 8)];

            for (int i = 0; i < width * height * N_Bits / 8; i++)
            {
                this.Grid[i] = img[i];
            }
        }


        public void Copy(CImage input)
        {
            width = input.width;
            height = input.height;
            N_Bits = input.N_Bits;

            for (int i = 0; i < width * height * N_Bits / 8; i++)
            {
                this.Grid[i] = input.Grid[i];
            }
        }

        /// <summary>
        /// returns the lightness of a color pixel
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public byte MaxC(byte R, byte G, byte B)
        {
            int max;
            if (0.713 * R > G) max = (int)(0.713 * R);
            else max = G;
            if (0.527 * B > max) max = (int)(0.527 * B);
            return (byte)max;
        }

        public int MessReturn(string s)
        {
            if (MessageBox.Show(s, "Return", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return -1;
            return 1;
        }


        public int SigmaSimpleUni(CImage input, int hWind, int Toleranz)
        // Simple sigma filter for both gray value and color images. 
        {
            int[] gvMin = new int[3], gvMax = new int[3], nPixel = new int[3], Sum = new int[3];
            int c;
            N_Bits = input.N_Bits;
            int nbyte = N_Bits / 8;
            for (int y = 0; y < height; y++) // ==================================================
            {
                int gv, y1, yStart = Math.Max(y - hWind, 0), yEnd = Math.Min(y + hWind, height - 1);
                for (int x = 0; x < width; x++) //===============================================
                {
                    int x1, xStart = Math.Max(x - hWind, 0), xEnd = Math.Min(x + hWind, width - 1);
                    for (c = 0; c < nbyte; c++)
                    {
                        Sum[c] = 0; nPixel[c] = 0;
                        gvMin[c] = Math.Max(0, input.Grid[c + nbyte * (x + width * y)] - Toleranz);
                        gvMax[c] = Math.Min(255, input.Grid[c + nbyte * (x + width * y)] + Toleranz);
                    }
                    for (y1 = yStart; y1 <= yEnd; y1++)
                        for (x1 = xStart; x1 <= xEnd; x1++)
                            for (c = 0; c < nbyte; c++)
                            {
                                gv = input.Grid[c + nbyte * (x1 + y1 * width)];
                                if (gv >= gvMin[c] && gv <= gvMax[c])
                                {
                                    Sum[c] += gv;
                                    nPixel[c]++;
                                }
                            }
                    for (c = 0; c < nbyte; c++)
                    {
                        if (nPixel[c] > 0)
                        {
                            this.Grid[c + nbyte * (x + width * y)] = (byte)((Sum[c] + nPixel[c] / 2) / nPixel[c]);
                        }
                        else
                        {
                            this.Grid[c + nbyte * (x + width * y)] = input.Grid[c + nbyte * (x + width * y)];
                        }
                    }
                } //================== end for (int x... =================================
            } //==================== end for (int y... ===================================
            return 1;
        } //********************** end SigmaSimpleUni **********************************


        public int ColorToGray(CImage inp, Form1 fm1)
        /* Transforms the colors of the color image "inp" in lightness=(r+g+b)/3 
        and puts these values to this.Grid. --------- */
        {
            int c, sum, x, y;
            if (inp.N_Bits != 24) return -1;

            fm1.progressBar1.Visible = true;
            N_Bits = 8; width = inp.width; height = inp.height;
            this.Grid = new byte[width * height * 8];

            int y1 = 1 + height / 100;

            for (y = 0; y < height; y++) //=========================
            {
                if (y % y1 == 1)
                {
                    fm1.progressBar1.PerformStep();
                }

                for (x = 0; x < width; x++) // =====================
                {
                    sum = 0;

                    for (c = 0; c < 3; c++)
                    {
                        sum += inp.Grid[c + 3 * (x + width * y)];
                    }

                    this.Grid[y * width + x] = (byte)(sum / 3);
                } // ========== for (x.  ====================
            }

            fm1.progressBar1.Visible = false;
            return 1;
        } //********************** end ColorToGray **********************


        /// <summary>
        /// ColorToGrayMC is not called
        /// </summary>
        /// <param name="inp"></param>
        /// <param name="fm1"></param>
        /// <returns></returns>
        public int ColorToGrayMC(CImage inp, Form1 fm1)
        /* Transforms the colors of the color image "inp" in lightness=MaxC(r,g,b) 
        and puts these values to this.Grid. --------- */
        {
            int gv, x, y;
            if (inp.N_Bits != 24) return -1;

            fm1.progressBar1.Visible = true;
            N_Bits = 8; width = inp.width; height = inp.height;
            this.Grid = new byte[width * height * 8];

            int y1 = 1 + height / 100;
            for (y = 0; y < height; y++) //=========================
            {
                if (y % y1 == 1) fm1.progressBar1.PerformStep();
                for (x = 0; x < width; x++) // =====================
                {
                    gv = MaxC(inp.Grid[2 + 3 * (x + width * y)],
                            inp.Grid[1 + 3 * (x + width * y)],
                            inp.Grid[0 + 3 * (x + width * y)]);
                    this.Grid[y * width + x] = (byte)gv;
                } // ========== for (x.  ====================
            }
            fm1.progressBar1.Visible = false;
            return 1;
        } //********************** end ColorToGrayMC **********************

        /// <summary>
        /// chapter 2 Noise Reduction page 10
        /// </summary>
        /// <param name="input"></param>
        /// <param name="halfWidthOfAveragingWindow"></param>
        /// <param name="fm1"></param>
        /// <returns></returns>
        public int FastAverageM(CImage input, int halfWidthOfAveragingWindow, Form1 fm1)
        // Filters the gray value image "Inp" and returns the result as *this."
        {
            if (input.N_Bits != 8)
            {
                MessageBox.Show("FastAverageM cannot process an image with " + input.N_Bits + " bits per pixel");
                return -1;
            }
            N_Bits = 8;
            width = input.width;
            height = input.height;
            this.Grid = new byte[width * height];
            int[] ColSum; int[] nC;
            ColSum = new int[width];
            nC = new int[width];
            for (int i = 0; i < width; i++) ColSum[i] = nC[i] = 0;

            int nS = 0, Sum = 0;
            fm1.progressBar1.Visible = false;
            for (int y = 0; y < height + halfWidthOfAveragingWindow; y++)
            {
                int yout = y - halfWidthOfAveragingWindow, ysub = y - 2 * halfWidthOfAveragingWindow - 1;
                Sum = 0;
                nS = 0;

                //int y1 = 1 + (height + hWind) / 100;
                for (int x = 0; x < width + halfWidthOfAveragingWindow; x++)
                {
                    int xout = x - halfWidthOfAveragingWindow, xsub = x - 2 * halfWidthOfAveragingWindow - 1; // 1. and 2. addition
                    if (y < height && x < width)
                    {
                        ColSum[x] += input.Grid[x + width * y];
                        nC[x]++;
                    } // 3. and 4. addition
                    if (ysub >= 0 && x < width)
                    {
                        ColSum[x] -= input.Grid[x + width * ysub];
                        nC[x]--;
                    }
                    if (yout >= 0 && x < width)
                    {
                        Sum += ColSum[x];
                        nS += nC[x];
                    }
                    if (yout >= 0 && xsub >= 0)
                    {
                        Sum -= ColSum[xsub];
                        nS -= nC[xsub];
                    }
                    if (xout >= 0 && yout >= 0)
                    {
                        this.Grid[xout + width * yout] = (byte)((Sum + nS / 2) / nS);
                    }
                }
            }

            return 1;
        } //*************************** end FastAverageM ********************************


        /// <summary>
        /// FastAverageNum is not called
        /// </summary>
        /// <param name="Inp"></param>
        /// <param name="fm1"></param>
        /// <returns></returns>
        public int FastAverageNum(CImage Inp, Form1 fm1)
        // Filters the image "Inp" and returns the result as *this."
        {
            if (Inp.N_Bits != 8)
            {
                MessageBox.Show("FastAverageM cannot process an image with " + Inp.N_Bits + " bits per pixel");
                return -1;
            }
            N_Bits = 8; width = Inp.width; height = Inp.height;
            this.Grid = new byte[width * height];
            int[] ColSum; int[] nC;
            ColSum = new int[width];
            nC = new int[width];
            for (int i = 0; i < width; i++) ColSum[i] = nC[i] = 0;

            int hWind = (int)fm1.numericUpDown1.Value * width / 200;
            fm1.numericUpDown1.Increment = 5;
            if (fm1.numericUpDown1.Value < 10) fm1.numericUpDown1.Increment = 1;

            int nS = 0, Sum = 0;
            for (int y = 0; y < height + hWind; y++)
            {
                int yout = y - hWind, ysub = y - 2 * hWind - 1;
                Sum = 0; nS = 0;

                //int y1 = 1 + (height + hWind) / 100;
                for (int x = 0; x < width + hWind; x++)
                {
                    int xout = x - hWind, xsub = x - 2 * hWind - 1; // 1. and 2. addition
                    if (y < height && x < width) { ColSum[x] += Inp.Grid[x + width * y]; nC[x]++; } // 3. and 4. addition
                    if (ysub >= 0 && x < width) { ColSum[x] -= Inp.Grid[x + width * ysub]; nC[x]--; }
                    if (yout >= 0 && x < width) { Sum += ColSum[x]; nS += nC[x]; }
                    if (yout >= 0 && xsub >= 0) { Sum -= ColSum[xsub]; nS -= nC[xsub]; }
                    if (xout >= 0 && yout >= 0)
                    {
                        this.Grid[xout + width * yout] = (byte)((Sum + nS / 2) / nS);
                    }
                }
            }
            return 1;
        } //*************************** end FastAverageNum ********************************

    } //*********************** end class CImage **************************
} //************************* end namespace **************************************


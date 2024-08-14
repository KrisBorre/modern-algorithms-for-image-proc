using System.Windows.Forms;

namespace PulseNoiseWF
{
    class CPnoise
    {
        unsafe
        public int[][] Index; // saving all pixels of the image ordered by lightness
        private int[] Component; // contains indices of pixels of a connected component
        private int[] nPixel; // number of pixels with certain lightness in the image
        private int MaxSize;  // admissible size of a component
        private Queue Q1;

        unsafe
        public CPnoise(int[] Histo, int Qlength, int Size)  // Constructor
        {
            MaxSize = Size;
            Q1 = new Queue(Qlength); // necessary to find connected components
            Component = new int[MaxSize];
            nPixel = new int[256]; // 256 is the number of lightness values
            for (int light = 0; light < 256; light++) nPixel[light] = 0;
            Index = new int[256][];
            for (int light = 0; light < 256; light++) Index[light] = new int[Histo[light] + 2];
        }

        ~CPnoise() { }

        public bool getCondition(int i, int x, int y, double marginX, double marginY, double Scale, Form1 fm1)
        { // Calculates bounds of the rectangle defined by global "fm1.v" and returns the condition
          // that the point (x, y) lies inside the rectangle.
            double fxmin = (fm1.v[i].X - marginX) / Scale; // "marginX" is the space of pictureBox1 left of image (may be 0)
            int xmin = (int)fxmin;

            double fxmax = (fm1.v[i + 1].X - marginX) / Scale; // Scale is the scale of the presentation of image
            int xmax = (int)fxmax;

            double fymin = (fm1.v[i].Y - marginY) / Scale; // "marginY" is the space of pictureBox1 above the image  (may be 0)
            int ymin = (int)fymin;

            double fymax = (fm1.v[i + 1].Y - marginY) / Scale;
            int ymax = (int)fymax;
            bool Condition = (y >= ymin && y <= ymax && x >= xmin && x <= xmax);
            return Condition;
        } //******************************* end getCond **********************************

        public int MaxC(int R, int G, int B)
        {
            int max;
            if (R * 0.713 > G) max = (int)(R * 0.713);
            else max = G;
            if (B * 0.527 > max) max = (int)(B * 0.527);
            return max;
        }


        public int Sort(CImage Image, int[] histo, int Number, int picBox1Width, int picBox1Height, Form1 fm1)
        {
            int light, i;
            double ScaleX = (double)picBox1Width / (double)Image.width;
            double ScaleY = (double)picBox1Height / (double)Image.height;
            double Scale; // Scale of the presentation of the image in "pictureBox1"
            if (ScaleX < ScaleY) Scale = ScaleX;
            else Scale = ScaleY;
            bool COLOR;
            if (Image.nBits == 24) COLOR = true;
            else COLOR = false;
            double marginX = (double)(picBox1Width - Scale * Image.width) * 0.5; // space left of the image
            double marginY = (double)(picBox1Height - Scale * Image.height) * 0.5; // space above the image
            bool Condition = false; // Condition for skipping pixel (x, y) if it lies in one of the global rectangles "fm1.v"
            fm1.progressBar1.Value = 0;
            fm1.progressBar1.Step = 1;
            fm1.progressBar1.Visible = true;
            fm1.progressBar1.Maximum = 100;
            for (light = 0; light < 256; light++) nPixel[light] = 0;
            for (light = 0; light < 256; light++)
                for (int light1 = 0; light1 < histo[light] + 1; light1++)
                    Index[light][light1] = 0;

            int y1 = 1 + Image.height / 100;
            for (int y = 0; y < Image.height; y++) //===============================================================
            {
                if (y % y1 == 1) fm1.progressBar1.PerformStep();
                for (int x = 0; x < Image.width; x++) //============================================================
                {
                    Condition = false;
                    for (int k = 0; k < Number; k += 2)
                        Condition = Condition || getCondition(k, x, y, marginX, marginY, Scale, fm1);
                    if (Condition) continue;
                    i = x + y * Image.width; // Index of the pixel (x, y)
                    if (COLOR)
                        light = MaxC(Image.Grid[3 * i + 2] & 254, Image.Grid[3 * i + 1] & 254, Image.Grid[3 * i + 0] & 254);
                    else light = Image.Grid[i] & 252;
                    if (light < 0) light = 0;
                    if (light > 255) light = 255;
                    Index[light][nPixel[light]] = i; // record of the index "i" of a pixel with lightness "light"
                    if (nPixel[light] < histo[light]) nPixel[light]++;
                } //============================ end for (int x=1; .. ========================================
            } //============================== end for (int y=1; .. ========================================
            fm1.progressBar1.Visible = false;
            return 1;
        } //******************************** end Sort *********************************************************

        public int Neighbor(CImage Image, int W, int n)
        // Returns the index of the nth neighbor of the pixel W. If the neighbor
        // is outside the grid, then it returns -1.
        {
            int dx, dy, x, y, xn, yn;
            if (n == 4) return -1; // "n==4" means Neigb==W
            yn = y = W / Image.width; xn = x = W % Image.width;
            dx = (n % 3) - 1; dy = n / 3 - 1;
            xn += dx; yn += dy;
            if (xn < 0 || xn >= Image.width || yn < 0 || yn >= Image.height) return -2;
            return xn + Image.width * yn;
        }

        // not called
        unsafe public int PositionInIndex(int lightNeb, int Neib)
        {
            for (int i = 0; i < nPixel[lightNeb]; i++)
                if (Index[lightNeb][i] == Neib) return i;
            return -1;
        }

        private int Lumi(byte R, byte G, byte B) // not used
        {
            return (int)((R + G + B) / 3);
        }

        // not called
        public int MessReturn(string s)
        {
            if (MessageBox.Show(s, "Return", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return -1;
            return 1;
        }


        private int BreadthFirst_DarkNoise(ref CImage image, int i, int light, int maxSize)
        /* Looks for pixels with lightness <=light composing with the pixel "Index[light][i]"
           an 8-connected subset. The size of the subset must be less than "maxSize".
           Instead of labeling the pixels of the subset, indices of pixels of the subset are saved in Comp.
           Variable "index" is the index of the starting pixel in Index[light][i];
           Pixels which are put into queue and into Comp[] are labeled in "Image.Grid(green)" by setting Bit 0 to 1.
           Pixels which belong to a too big component and having the gray value equal to "light" are
           labeled in "Image.Grid(red)" by setting Bit 0 to 1. If such a labeled pixel is found in the while loop 
           then "small" is set to 0. The instruction for breaking the loop is at the end of the loop. --*/
        {
            int lightness_of_the_neighbor,
                index, LabelQ1, LabelBig2, the_maximum_number_of_neighbors_of_a_pixel,
                the_index_of_a_neighbor,
                index_of_the_next_pixel_in_the_queue,
                number_of_pixel_indices_in_Component;
            bool small;
            bool COLOR = (image.nBits == 24);
            index = Index[light][i];
            int[] MinBound = new int[3]; // color of a pixel with minimum lightness among pixels near the subset
            for (int c = 0; c < 3; c++) { MinBound[c] = 300; }
            for (int p = 0; p < MaxSize; p++) Component[p] = -1; // MaxSize is element of class CPnoise
            number_of_pixel_indices_in_Component = 0;
            the_maximum_number_of_neighbors_of_a_pixel = 8; // maximum number of neighbors
            small = true;
            Component[number_of_pixel_indices_in_Component] = index;
            number_of_pixel_indices_in_Component++;
            if (COLOR)
                image.Grid[1 + 3 * index] |= 1; // Labeling as in Comp (LabelQ1)
            else
                image.Grid[index] |= 1; // Labeling as in Comp
            Q1.input = Q1.output = 0;
            Q1.Put(index); // putting index into the queue
            while (Q1.Empty() == 0) //=  loop running while queue not empty =======================
            {
                index_of_the_next_pixel_in_the_queue = Q1.Get();
                for (int n = 0; n <= the_maximum_number_of_neighbors_of_a_pixel; n++) // == all neighbors of nextIndex =====================
                {
                    the_index_of_a_neighbor = Neighbor(image, index_of_the_next_pixel_in_the_queue, n); // the index of the nth neighbor of nextIndex 
                    if (the_index_of_a_neighbor < 0) continue; // Neib<0 means outside the image
                    if (COLOR)
                    {
                        LabelQ1 = image.Grid[1 + 3 * the_index_of_a_neighbor] & 1;
                        LabelBig2 = image.Grid[2 + 3 * the_index_of_a_neighbor] & 1;
                        lightness_of_the_neighbor = MaxC(image.Grid[2 + 3 * the_index_of_a_neighbor], image.Grid[1 + 3 * the_index_of_a_neighbor], image.Grid[0 + 3 * the_index_of_a_neighbor]) & 254; // MaskColor;
                    }
                    else
                    {
                        LabelQ1 = image.Grid[the_index_of_a_neighbor] & 1;
                        LabelBig2 = image.Grid[the_index_of_a_neighbor] & 2;
                        lightness_of_the_neighbor = image.Grid[the_index_of_a_neighbor] & 252; // MaskGV;
                    }
                    if (lightness_of_the_neighbor == light && LabelBig2 > 0) small = false;
                    if (lightness_of_the_neighbor <= light) //------------------------------------------------------------
                    {
                        if (LabelQ1 > 0) continue;
                        Component[number_of_pixel_indices_in_Component] = the_index_of_a_neighbor; // putting the element with index Neib into Component
                        number_of_pixel_indices_in_Component++;
                        if (COLOR)
                            image.Grid[1 + 3 * the_index_of_a_neighbor] |= 1; // Labeling with "1" as in Comp 
                        else
                            image.Grid[the_index_of_a_neighbor] |= 1; // Labeling with "1" as in Comp 
                        if (number_of_pixel_indices_in_Component > maxSize)
                        {
                            small = false;
                            break;
                        }
                        Q1.Put(the_index_of_a_neighbor);
                    }
                    else // lightNeb < light
                    {
                        if (the_index_of_a_neighbor != index) //-----------------------------------------------------
                        {
                            if (COLOR)
                            {
                                if (lightness_of_the_neighbor < MaxC(MinBound[2], MinBound[1], MinBound[0]))
                                    for (int c = 0; c < 3; c++) { MinBound[c] = image.Grid[c + 3 * the_index_of_a_neighbor]; }
                            }
                            else
                              if (lightness_of_the_neighbor < MinBound[0]) MinBound[0] = lightness_of_the_neighbor;
                        } //------------------ end if (Neib != index) ----------------------------       
                    } //-------------------- end if (lightNeb<=light) and else ----------------------
                } // ===================== end for (n=0; .. ======================================
                if (!small) break;
            } // ===================== end while =================================================

            // Deleting 
            int lightnessComponent; // lightness of a pixel whose index is contained in "Component"
            for (int m = 0; m < number_of_pixel_indices_in_Component; m++) //======================================================
            {
                if (small && MinBound[0] < 300) //--"300" means MinBound was not calculated ---
                {
                    if (COLOR)
                        for (int c = 0; c < 3; c++) { image.Grid[c + 3 * Component[m]] = (byte)MinBound[c]; }
                    else
                        image.Grid[Component[m]] = (byte)MinBound[0];
                }
                else
                {
                    if (COLOR)
                    {
                        lightnessComponent = MaxC(image.Grid[2 + 3 * Component[m]], image.Grid[1 + 3 * Component[m]], image.Grid[0 + 3 * Component[m]]) & 254;
                    }
                    else
                    {
                        lightnessComponent = image.Grid[Component[m]] & 252; // MaskGV;
                    }

                    if (lightnessComponent == light) //----------------------------------------------------------------
                    {
                        if (COLOR) image.Grid[2 + 3 * Component[m]] |= 1; // setting label 2
                        else image.Grid[Component[m]] |= 2;
                    }
                    else // lightComp!=light
                    {
                        if (COLOR)
                        {
                            image.Grid[1 + 3 * Component[m]] &= (byte)254; // deleting label 1
                            image.Grid[2 + 3 * Component[m]] &= (byte)254; // deleting label 2
                        }
                        else
                            image.Grid[Component[m]] &= 252; // (byte)MaskGV; // deleting the labels
                    } //------------------------------ end if (bric == light) and else ------------------
                } //-------------------------------- end if (small != false) and else -----------------
            } //================================== end for (int m=0 .. ================================
            return number_of_pixel_indices_in_Component;
        } //************************************ end BreadthFirst_D ************************************


        public int DarkNoise(ref CImage image, int minLight, int maxLight, int maxSize, Form1 fm1)
        {
            bool COLOR = (image.nBits == 24);
            int ind3 = 0, // index multiplied with 3
                LabelBig2, Lum, rv = 0;
            if (maxSize == 0) return 0;
            fm1.progressBar1.Maximum = 100;
            fm1.progressBar1.Step = 1;
            fm1.progressBar1.Value = 0;
            fm1.progressBar1.Visible = false;
            int bri1 = 2;
            fm1.progressBar1.Visible = true;
            for (int light = maxLight - 2; light >= minLight; light--) //=========================================
            {
                if ((light % bri1) == 1) fm1.progressBar1.PerformStep();
                for (int i = 0; i < nPixel[light]; i++) //========================================
                {
                    ind3 = 3 * Index[light][i];
                    if (COLOR)
                    {
                        LabelBig2 = image.Grid[2 + ind3] & 1;
                        Lum = MaxC(image.Grid[2 + ind3], image.Grid[1 + ind3], image.Grid[0 + ind3]) & 254;
                    }
                    else
                    {
                        LabelBig2 = image.Grid[Index[light][i]] & 2;
                        Lum = image.Grid[Index[light][i]] & 252;
                    }

                    if (Lum == light && LabelBig2 == 0)
                    {
                        rv = BreadthFirst_DarkNoise(ref image, i, light, maxSize);
                        if (rv < 0) return -1;
                    }

                } //============================= end for (int i.. =======================
            } //=============================== end for (int light.. ========================
            fm1.progressBar1.Visible = false;
            return rv;
        } //********************************* end DarkNoise *******************************


        public int LightNoise(ref CImage image, int minLight, int maxLight, int maxSize, Form1 fm1)
        {
            bool COLOR = (image.nBits == 24);
            int ind3 = 0, // index multiplied with 3
                LabelBig2, Lum, rv = 0;
            if (maxSize == 0) return 0;
            fm1.progressBar1.Minimum = 0;
            fm1.progressBar1.Maximum = 100;
            fm1.progressBar1.Step = 1;
            fm1.progressBar1.Value = 0;
            fm1.progressBar1.Visible = true;

            for (int light = minLight; light <= 255; light++) //=========================================
            {
                int bri1 = 2; // (maxLight - minLight + 1) / 100;
                if ((light % bri1) == 1) fm1.progressBar1.PerformStep();
                for (int i = 0; i <= nPixel[light]; i++) //========================================
                {
                    ind3 = 3 * Index[light][i];
                    if (COLOR)
                    {
                        LabelBig2 = image.Grid[2 + ind3] & 1;
                        Lum = MaxC(image.Grid[2 + ind3], image.Grid[1 + ind3], image.Grid[0 + ind3]) & 254;
                    }
                    else
                    {
                        LabelBig2 = image.Grid[Index[light][i]] & 2;
                        Lum = image.Grid[Index[light][i]];
                    }

                    if (Lum == light && LabelBig2 == 0)
                    {
                        rv = BreadthFirst_LightNoise(ref image, i, light, maxSize);
                    }
                } //============================= end for (int i.. =======================
            } //=============================== end for (int light.. ========================
            fm1.progressBar1.Visible = false;
            return rv;
        } //********************************* end LightNoise ******************************


        private int BreadthFirst_LightNoise(ref CImage image, int i, int light, int maxSize)
        // Looks for pixels with gray values >=light composing with the pixel "Index[light][i]"
        // an 8-connected subset. The size of the subset must be less than "maxSize".
        // Instead of labeling the pixels of the subset, indices of pixels of the subset are saved in Comp.
        // Variable "i" is the index of the starting pixel in Index[light][i];
        // Pixels which are put into queue and into Comp[] are labeled in "Image.Grid(green)" by setting Bit 0 to 1.
        // Pixels wich belong to a too big component and having the gray value equal to "light" are
        // labeled in "Image.Grid(red)" by setting Bit 0 to 1. If such a labeled pixel is found in the while loop
        // then "small" is set to 0. The insruction for breaking the loop is at the end of the loop. 
        {
            int lightNeb, index, LabelQ1, LabelBig2, MaskBri = 252, MaskColor = 254, maxNeib = 8, neighbor, nextIndex;
            bool small = true;
            int[] MaxBound = new int[3];
            bool COLOR = (image.nBits == 24);
            index = Index[light][i];
            for (int c = 0; c < 3; c++) { MaxBound[c] = -255; }
            for (int p = 0; p < MaxSize; p++) Component[p] = -1;
            int numbPix = 0;
            Component[numbPix] = index;
            numbPix++;
            if (COLOR)
                image.Grid[1 + 3 * index] |= 1; // Labeling as in Comp
            else
                image.Grid[index] |= 1; // Labeling as in Comp
            Q1.input = Q1.output = 0;
            Q1.Put(index); // putting index into the queue

            while (Q1.Empty() == 0) //=== loop running while queue not empty ========================
            {
                nextIndex = Q1.Get();
                for (int n = 0; n <= maxNeib; n++) //======== all neighbors of nextIndex ================
                {
                    neighbor = Neighbor(image, nextIndex, n); // the index of the nth neighbor of nextIndex 
                    if (neighbor < 0) continue; // Neib<0 means outside the image
                    if (COLOR)
                    {
                        LabelQ1 = image.Grid[1 + 3 * neighbor] & 1;
                        LabelBig2 = image.Grid[2 + 3 * neighbor] & 1;
                        lightNeb = MaxC(image.Grid[2 + 3 * neighbor], image.Grid[1 + 3 * neighbor], image.Grid[0 + 3 * neighbor]) & 254; // MaskColor;
                    }
                    else
                    {
                        LabelQ1 = image.Grid[neighbor] & 1;
                        LabelBig2 = image.Grid[neighbor] & 2;
                        lightNeb = image.Grid[neighbor] & MaskBri;
                    }
                    if (lightNeb == light && LabelBig2 > 0) small = false;

                    if (lightNeb >= light) //------------------------------------------------------------
                    {
                        if (LabelQ1 > 0) continue;
                        Component[numbPix] = neighbor; // putting the element with index Neib into Component
                        numbPix++;
                        if (COLOR)
                            image.Grid[1 + 3 * neighbor] |= 1; // Labeling with "1" as in Comp 
                        else
                            image.Grid[neighbor] |= 1; // Labeling with "1" as in Comp 

                        if (numbPix > maxSize)
                        {
                            small = false;
                            break;
                        }
                        Q1.Put(neighbor);
                    }
                    else // lightNeb<light
                    {
                        if (neighbor != index) //-----------------------------------------------------
                        {
                            if (COLOR)
                            {
                                if (lightNeb > MaxC(MaxBound[2], MaxBound[1], MaxBound[0]))
                                {
                                    for (int c = 0; c < 3; c++)
                                    {
                                        MaxBound[c] = (image.Grid[c + 3 * neighbor] & MaskColor);
                                    }
                                }
                            }
                            else
                            {
                                if (lightNeb > MaxBound[0]) MaxBound[0] = lightNeb;
                            }
                        } //------------------ end if (Neib!=index) ----------------------------       
                    } //-------------------- end if (lightNeb<=light) and else ------------------------
                } // =================== end for (n=0; .. ====================================
                if (!small) break;
            } // ===================== end while ==============================================
            int lightnessComponent, // lightness of a pixel whose index is contained in "Component"
                number_of_pixels_whose_lightness_was_changed = 0;
            for (int m = 0; m < numbPix; m++) //========================================================
            {
                if (small && MaxBound[0] >= 0) //it was >-255; ----"-1" means MaxBound was not calculated ---------
                {
                    if (COLOR)
                    {
                        for (int c = 0; c < 3; c++)
                        {
                            image.Grid[c + 3 * Component[m]] = (byte)MaxBound[c];
                        }
                    }
                    else
                    {
                        image.Grid[Component[m]] = (byte)MaxBound[0];
                        number_of_pixels_whose_lightness_was_changed++;
                    }
                }
                else
                {
                    if (COLOR)
                    {
                        lightnessComponent = MaxC(image.Grid[2 + 3 * Component[m]], image.Grid[1 + 3 * Component[m]], image.Grid[0 + 3 * Component[m]]) & MaskColor;
                    }
                    else
                    {
                        lightnessComponent = image.Grid[Component[m]] & MaskBri;
                    }

                    if (lightnessComponent == light) //------------------------------------------------------ 
                    {
                        if (COLOR) image.Grid[2 + 3 * Component[m]] |= 1;
                        else image.Grid[Component[m]] |= 2;
                    }
                    else
                    {
                        if (COLOR)
                        {
                            image.Grid[1 + 3 * Component[m]] &= (byte)MaskColor; // deleting label 1
                            image.Grid[2 + 3 * Component[m]] &= (byte)MaskColor; // deleting label 2
                        }
                        else
                            image.Grid[Component[m]] &= (byte)MaskBri; // deleting the labels
                    } //----------------------- end if (lightComp==light) and else ---------------
                } //------------------------- end if (small && MaxBound[0]>0) and else ----------
            } //=========================== end for (int m=0 .. ================================
            return number_of_pixels_whose_lightness_was_changed; // numbPix;
        } //************************************ end BreadthFirst_L *****************************

    } //************************************** end class CPnoise ********************************
} //**************************************** end namespace PulseNoiseWF *************************

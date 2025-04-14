using System.Drawing;

namespace WFcompressPal
{
    public struct iVect2
    {
        public int X, Y;

        public iVect2(int x, int y) // constructor
        {
            X = x;
            Y = y;
        }

        public static iVect2 operator +(iVect2 a, iVect2 b)
        {
            return new iVect2(a.X + b.X, a.Y + b.Y);
        }

        public static iVect2 operator -(iVect2 a, iVect2 b)
        {
            return new iVect2(a.X - b.X, a.Y - b.Y);
        }

        public static iVect2 operator -(iVect2 a)
        {
            return new iVect2(-a.X, -a.Y);
        }

        public static bool operator ==(iVect2 a, iVect2 b) // is used in 'CheckComb'
        {
            if (a.X == b.X && a.Y == b.Y) return true;
            return false;
        }

        public static bool operator !=(iVect2 a, iVect2 b)
        {
            if (a.X != b.X || a.Y != b.Y) return true;
            return false;
        } //--*/
    } //********************* end public struct iVect2 ***************************/


    public class CInscript
    {
        private double zoom;
        private int marginX, marginY, width;
        private Color color1;
        private Graphics g;
        private Pen myPen;

        public CInscript() { } // default constructor
        //CInscript(Zoom, marginX, marginY, 1, Color.White);

        public CInscript(int picBoxInd, double scale, int marx, int mary, int width, Color color, Form1 fm1) // constructor
        {
            zoom = scale;
            marginX = marx;
            marginY = mary;
            this.width = width;
            color1 = color;
            if (picBoxInd == 1) g = fm1.pictureBox1.CreateGraphics();
            else g = fm1.pictureBox2.CreateGraphics();
            myPen = new Pen(color1);
        }

        private int Minus(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            int sizeV = 2;
            //iVect2[] Vert = {  {2, 9),  {10, 9) }; // "-"
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(2, 9);
            vert[1] = new iVect2(10, 9); // "-"
            xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        } //*************************** end Minus ****************************

        private int Plus(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            int sizeV = 5;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(0, 9); vert[1] = new iVect2(12, 9);
            vert[2] = new iVect2(6, 9);
            vert[3] = new iVect2(6, 15); vert[4] = new iVect2(6, 3); // "+"
            xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        } //*************************** end Minus ****************************


        private int Stop(int x0, int y0)
        {
            int xmax = 0, xold, yold;
            xold = 4 - width; yold = 18 - width;
            //x=4; y=0;
            xmax = 4;
            SolidBrush myBrush = new SolidBrush(Color.White);
            Rectangle rect = new Rectangle(marginX + x0 + 4 - width + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold), width, width);
            g.FillRectangle(myBrush, rect);
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int Equal(int x0, int y0)
        {
            int x, xmax = 0, xold, y, yold;
            const int sizeV = 4;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(1, 11); vert[1] = new iVect2(11, 11);
            vert[2] = new iVect2(1, 7); vert[3] = new iVect2(11, 7); // "="
            xmax = 10;
            xold = vert[0].X;
            yold = vert[0].Y;
            x = vert[1].X; y = vert[1].Y;
            g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
              marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
            xold = vert[2].X;
            yold = vert[2].Y;
            x = vert[3].X; y = vert[3].Y;
            g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
              marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int Null(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 9;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(9, 15); vert[1] = new iVect2(6, 18);
            vert[2] = new iVect2(3, 18); vert[3] = new iVect2(0, 15);
            vert[4] = new iVect2(0, 3); vert[5] = new iVect2(3, 0);
            vert[6] = new iVect2(6, 0); vert[7] = new iVect2(9, 3);
            vert[8] = new iVect2(9, 15); // "O"
            xmax = 0;
            xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 7));
        }

        private int F1(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 5;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(3, 18); vert[1] = new iVect2(7, 18);
            vert[2] = new iVect2(5, 18); vert[3] = new iVect2(5, 0);
            vert[4] = new iVect2(0, 5); // "T"
            xmax = xold = vert[0].X;
            xmax = 0;
            xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 7));
        }

        private int F2(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 8;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(9, 18); vert[1] = new iVect2(0, 18);
            vert[2] = new iVect2(9, 6); vert[3] = new iVect2(9, 3);
            vert[4] = new iVect2(6, 0); vert[5] = new iVect2(3, 0);
            vert[6] = new iVect2(0, 3); vert[7] = new iVect2(0, 6); // "2"
            xmax = xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int F3(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 13;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(0, 3); vert[1] = new iVect2(3, 0);
            vert[2] = new iVect2(6, 0); vert[3] = new iVect2(9, 3);
            vert[4] = new iVect2(9, 6); vert[5] = new iVect2(6, 9);
            vert[6] = new iVect2(3, 9); vert[7] = new iVect2(6, 9);
            vert[8] = new iVect2(9, 12); vert[9] = new iVect2(9, 15);
            vert[10] = new iVect2(6, 18); vert[11] = new iVect2(3, 18);
            vert[12] = new iVect2(0, 15); // "3"
            xmax = xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int F4(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 4;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(7, 18); vert[1] = new iVect2(7, 0);
            vert[2] = new iVect2(0, 12); vert[3] = new iVect2(9, 12); // "4"
            xmax = xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int F5(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 9;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(0, 16); vert[1] = new iVect2(2, 18);
            vert[2] = new iVect2(6, 18); vert[3] = new iVect2(9, 15);
            vert[4] = new iVect2(9, 10); vert[5] = new iVect2(6, 7);
            vert[6] = new iVect2(1, 7); vert[7] = new iVect2(3, 0);
            vert[8] = new iVect2(9, 0); // "3"
            xmax = xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int F6(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 12;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(9, 0); vert[1] = new iVect2(5, 2);
            vert[2] = new iVect2(2, 6); vert[3] = new iVect2(0, 12);
            vert[4] = new iVect2(0, 15); vert[5] = new iVect2(3, 18);
            vert[6] = new iVect2(6, 18); vert[7] = new iVect2(9, 15);
            vert[8] = new iVect2(9, 10); vert[9] = new iVect2(6, 7);
            vert[10] = new iVect2(3, 7); vert[11] = new iVect2(2, 7); // "6"
            xmax = xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int F7(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 4;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(5, 18); vert[1] = new iVect2(9, 0);
            vert[2] = new iVect2(2, 0); vert[3] = new iVect2(2, 2); // "7"
            xmax = xold = vert[0].X;
            xmax = 0;
            xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 7));
        }

        private int F8(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 13;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(0, 3); vert[1] = new iVect2(3, 0);
            vert[2] = new iVect2(6, 0); vert[3] = new iVect2(9, 3);
            vert[4] = new iVect2(9, 6); vert[5] = new iVect2(0, 10);
            vert[6] = new iVect2(0, 15); vert[7] = new iVect2(3, 18);
            vert[8] = new iVect2(6, 18); vert[9] = new iVect2(9, 15);
            vert[10] = new iVect2(9, 10); vert[11] = new iVect2(0, 6);
            vert[12] = new iVect2(0, 3); // "8"
            xmax = xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;

                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int F9(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 12;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(0, 18); vert[1] = new iVect2(3, 16);
            vert[2] = new iVect2(7, 10); vert[3] = new iVect2(9, 6);
            vert[4] = new iVect2(9, 3); vert[5] = new iVect2(6, 0);
            vert[6] = new iVect2(3, 0); vert[7] = new iVect2(0, 3);
            vert[8] = new iVect2(0, 8); vert[9] = new iVect2(3, 11);
            vert[10] = new iVect2(6, 11); vert[11] = new iVect2(7, 10); // "6"
            xmax = xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int A(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            int sizeV = 5;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(0, 18); vert[1] = new iVect2(6, 0);
            vert[2] = new iVect2(12, 18); vert[3] = new iVect2(10, 12);
            vert[4] = new iVect2(2, 12); // "-"
            xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        } //*************************** end A ****************************



        private int B(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 12;
            iVect2[] vert = new iVect2[sizeV];
            vert[0] = new iVect2(0, 18); vert[1] = new iVect2(0, 0);
            vert[2] = new iVect2(6, 0); vert[3] = new iVect2(9, 2);
            vert[4] = new iVect2(9, 5); vert[5] = new iVect2(6, 9);
            vert[6] = new iVect2(0, 9); vert[7] = new iVect2(6, 9);
            vert[8] = new iVect2(9, 12); vert[9] = new iVect2(9, 15);
            vert[10] = new iVect2(6, 18); vert[11] = new iVect2(0, 18); // "B"
            xold = vert[0].X;
            yold = vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = vert[iv].X; y = vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int C(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 8;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(9, 15); Vert[1] = new iVect2(6, 18);
            Vert[2] = new iVect2(3, 18); Vert[3] = new iVect2(0, 15);
            Vert[4] = new iVect2(0, 3); Vert[5] = new iVect2(3, 0);
            Vert[6] = new iVect2(6, 0); Vert[7] = new iVect2(9, 3); // "T"
            xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int E(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 7;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(11, 18); Vert[1] = new iVect2(0, 18);
            Vert[2] = new iVect2(0, 9); Vert[3] = new iVect2(6, 9);
            Vert[4] = new iVect2(0, 9); Vert[5] = new iVect2(0, 0);
            Vert[6] = new iVect2(11, 0);
            xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int I(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 6;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(0, 18); Vert[1] = new iVect2(10, 18);
            Vert[2] = new iVect2(5, 18); Vert[3] = new iVect2(5, 0);
            Vert[4] = new iVect2(0, 0); Vert[5] = new iVect2(10, 0); // "I"
            xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int K(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 6;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(0, 18); Vert[1] = new iVect2(0, 0);
            Vert[2] = new iVect2(0, 12); Vert[3] = new iVect2(10, 0);
            Vert[4] = new iVect2(5, 6); Vert[5] = new iVect2(10, 18);
            xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int L(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 3;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(0, 0); Vert[1] = new iVect2(0, 18);
            Vert[2] = new iVect2(11, 18); // "L"
            xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int M(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 5;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(0, 18); Vert[1] = new iVect2(0, 0);
            Vert[2] = new iVect2(6, 9); Vert[3] = new iVect2(12, 0);
            Vert[4] = new iVect2(12, 18); //'M'
            xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int N(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 4;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(0, 18); Vert[1] = new iVect2(0, 0);
            Vert[2] = new iVect2(10, 18); Vert[3] = new iVect2(10, 0);
            xmax = xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int R(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            int sizeV = 9;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(0, 18); Vert[1] = new iVect2(0, 0);
            Vert[2] = new iVect2(6, 0); Vert[3] = new iVect2(9, 2);
            Vert[4] = new iVect2(9, 5); Vert[5] = new iVect2(6, 9);
            Vert[6] = new iVect2(0, 9); Vert[7] = new iVect2(6, 9);
            Vert[8] = new iVect2(10, 18); // "-"
            xmax = xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int T(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 4;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(6, 18); Vert[1] = new iVect2(6, 0);
            Vert[2] = new iVect2(0, 0); Vert[3] = new iVect2(10, 0); // "T"
            xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }


        private int U(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 6;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(0, 0); Vert[1] = new iVect2(0, 15);
            Vert[2] = new iVect2(3, 18); Vert[3] = new iVect2(7, 18);
            Vert[4] = new iVect2(10, 15); Vert[5] = new iVect2(10, 0);
            xmax = xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }

        private int W(int x0, int y0)
        {
            int iv, x, xmax = 0, xold, y, yold;
            const int sizeV = 5;
            iVect2[] Vert = new iVect2[sizeV];
            Vert[0] = new iVect2(0, 0); Vert[1] = new iVect2(0, 18);
            Vert[2] = new iVect2(6, 9); Vert[3] = new iVect2(12, 18);
            Vert[4] = new iVect2(12, 0);
            xmax = xold = Vert[0].X;
            yold = Vert[0].Y;
            for (iv = 1; iv < sizeV; iv++)
            {
                x = Vert[iv].X; y = Vert[iv].Y;
                if (x > xmax) xmax = x;
                g.DrawLine(myPen, marginX + x0 + (int)(zoom * xold), marginY + y0 + (int)(zoom * yold),
                  marginX + x0 + (int)(zoom * x), marginY + y0 + (int)(zoom * y));
                xold = x; yold = y;
            }
            return x0 + (int)(zoom * (xmax + 6));
        }



        public void Write(string str, int x0, int y0, CInscript inscript)
        {
            int i, len = str.Length, rv = x0;
            //MessageBox.Show("Write: Str=" + Str + " len=" + len + " x0=" + x0 + " y0=" + y0);
            for (i = 0; i < len; i++) //===============================================
            {
                switch (str[i])
                {
                    case '-': rv = inscript.Minus(rv, y0); break;
                    case '+': rv = inscript.Plus(rv, y0); break;
                    case '=': rv = inscript.Equal(rv, y0); break;
                    case '.': rv = inscript.Stop(rv, y0); break;
                    case '0': rv = inscript.Null(rv, y0); break;
                    case '1': rv = inscript.F1(rv, y0); break;
                    case '2': rv = inscript.F2(rv, y0); break;
                    case '3': rv = inscript.F3(rv, y0); break;
                    case '4': rv = inscript.F4(rv, y0); break;
                    case '5': rv = inscript.F5(rv, y0); break;
                    case '6': rv = inscript.F6(rv, y0); break;
                    case '7': rv = inscript.F7(rv, y0); break;
                    case '8': rv = inscript.F8(rv, y0); break;
                    case '9': rv = inscript.F9(rv, y0); break;
                    case 'a':
                    case 'A': rv = inscript.A(rv, y0); break;
                    case 'b':
                    case 'B': rv = inscript.B(rv, y0); break;
                    case 'c':
                    case 'C': rv = inscript.C(rv, y0); break;
                    case 'e':
                    case 'E': rv = inscript.E(rv, y0); break;
                    case 'i':
                    case 'I': rv = inscript.I(rv, y0); break;
                    case 'k':
                    case 'K': rv = inscript.K(rv, y0); break;
                    case 'l':
                    case 'L': rv = inscript.L(rv, y0); break;
                    case 'm':
                    case 'M': rv = inscript.M(rv, y0); break;
                    case 'n':
                    case 'N': rv = inscript.N(rv, y0); break;
                    case 'r':
                    case 'R': rv = inscript.R(rv, y0); break;
                    case 't':
                    case 'T': rv = inscript.T(rv, y0); break;
                    case 'u':
                    case 'U': rv = inscript.U(rv, y0); break;
                    case 'w':
                    case 'W': rv = inscript.W(rv, y0); break;
                } //::::::::::::::::::::::: end switch :::::::::::::::::::::::::::::::::::
            } //========================= end for (i ... =================================
        } //*************************** end write ****************************************
    }

}

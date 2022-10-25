namespace SimdGisTest
{
    public readonly struct TmlRectD 
    {
        private static TmlRectD defaultBoxDeg = new TmlRectD(-180, -90, 360, 180);
        public static ref TmlRectD DefaultBoxDeg => ref defaultBoxDeg;

        private static TmlRectD empty = new TmlRectD();
        public static ref TmlRectD Empty => ref empty;

        public double X { get; }
        public double Y { get; }
        public double Width { get; }
        public double Height { get; }
        public double Left => X;
        public double Top => Y;
        public double Right => (X + Width);
        public double Bottom => (Y + Height);

        public TmlRectD(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
       
       
      
        public override int GetHashCode()
        {
            return (int)(((uint)X) ^ ((((uint)Y) << 13) | (((uint)Y) >> 0x13)) ^ ((((uint)Width) << 0x1a) | (((uint)Width) >> 6)) ^ ((((uint)Height) << 7) | (((uint)Height) >> 0x19)));
        }
        //---------------------------------------------
        public bool Contains(double x, double y) => (X <= x) && (x < (X + Width)) && (Y <= y) && (y < (Y + Height));
        public bool Contains(in PtDbl pt) => (X <= pt.X) && (pt.X < (X + Width)) && (Y <= pt.Y) && (pt.Y < (Y + Height));
        public bool Contains(in TmlRectD rect) => (X <= rect.X) && ((rect.X + rect.Width) <= (X + Width)) && (Y <= rect.Y) && ((rect.Y + rect.Height) <= (Y + Height));
        
        //----------------------------------------------------------------
        public override string ToString() => $"{{Left={Left},Top={Top},Width={Width},Height={Height}}}";
        public string ToString3()
        {
            return ("LeftTop:[X=" + this.X.ToString() + ",Y=" + this.Y.ToString() + "]\r\n" +
                    "RightBottom:[X=" + this.Right.ToString() + ",Y=" + this.Bottom.ToString() + "]\r\n" +
                    "Width={" + this.Width.ToString() + "}\r\n" +
                    "Height={" + this.Height.ToString() + "}"
                    );
        }
        public string ToString2() => $"Left={Left}\nTop={Top}\nRight={Right}\nBottom={Bottom}";
        public string ToStringTwo() => $"Left={Left:f7} Top={Top:f7}\nRight={Right:f7} Bottom={Bottom:f7}";
        public string ToStringTwoS() => $"L={Left:f7} T={Top:f7}\nR={Right:f7} B={Bottom:f7}";
        public string ToStringTwoSH() => $"L={Left:f7} T={Top:f7} R={Right:f7} B={Bottom:f7}";




        public static TmlRectD GetBoundingBox(IList<PtDbl> pts)
        {
            double Left = pts[0].X;
            double Right = pts[0].X;
            double Top = pts[0].Y;
            double Bottom = pts[0].Y;

            for (int i = 1; i < pts.Count; i++)
            {
                Left = Math.Min(pts[i].X, Left);
                Right = Math.Max(pts[i].X, Right);
                Top = Math.Min(pts[i].Y, Top);
                Bottom = Math.Max(pts[i].Y, Bottom);
            }
            return new TmlRectD(Left, Top, Right - Left, Bottom - Top);
        }



        public static TmlRectD GetBoundingBox(IList<PtFlt> pts)
        {
            float Left = pts[0].X;
            float Right = pts[0].X;
            float Top = pts[0].Y;
            float Bottom = pts[0].Y;

            for (int i = 1; i < pts.Count; i++)
            {
                Left = Math.Min(pts[i].X, Left);
                Right = Math.Max(pts[i].X, Right);
                Top = Math.Min(pts[i].Y, Top);
                Bottom = Math.Max(pts[i].Y, Bottom);
            }
            return new TmlRectD(Left, Top, Right - Left, Bottom - Top);
        }

    }
}

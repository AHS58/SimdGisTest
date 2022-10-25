using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimdGisTest
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PtDbl
    {
        public double X { get; }
        public double Y { get; }
        public PtDbl(double x, double y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"X:{X} Y:{Y}";
        }

        //***
        public static PtDbl[] GetRandomPointArray(int count)
        {
            Random r = new();
            PtDbl[] result = new PtDbl[count];

            for (int i = 0; i < count; i++)
            {
                int sd = r.Next(0, 180);
                result[i] = new(r.NextDouble() * sd, r.NextDouble() * sd);
            }

            return result;
        }
    }
}

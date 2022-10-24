using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace SimdGisTest
{
    internal class GisIntrinsic
    {
        public static TmlRectD GetBoundingBoxSIMD256(PtDbl[] pts)
        {
            //if (!Avx.IsSupported || pts.Length < 2)
            //{
            //    return TmlRectD.GetBoundingBox(pts);
            //}

            int Remainder = pts.Length % 2;

            ReadOnlySpan<PtDbl> bs = pts;
            ReadOnlySpan<Vector256<double>> vecDz = MemoryMarshal.Cast<PtDbl, Vector256<double>>(bs[..(pts.Length - Remainder)]);

            var vmin = Vector256.Create(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
            var vmax = Vector256.Create(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);

            for (int i = 1; i < vecDz.Length; i++)
            {
                vmin = Avx.Min(vecDz[i], vmin);
                vmax = Avx.Max(vecDz[i], vmax);
            }

            //if (Remainder > 0)
            //{
            //    var vlast = Vector256.Create(pts[0].X, pts[0].Y, pts[^1].X, pts[^1].Y);//p[0] for fill
            //    vmin = Avx.Min(vlast, vmin);
            //    vmax = Avx.Max(vlast, vmax);
            //}

            ref PtDbl ptlast = ref pts[^1];
            double minX = Math.Min(Math.Min(vmin.GetElement(0), vmin.GetElement(2)), ptlast.X);
            double minY = Math.Min(Math.Min(vmin.GetElement(1), vmin.GetElement(3)), ptlast.Y);
            double maxX = Math.Max(Math.Max(vmax.GetElement(0), vmax.GetElement(2)), ptlast.X);
            double maxY = Math.Max(Math.Max(vmax.GetElement(1), vmax.GetElement(3)), ptlast.Y);

            return new TmlRectD(minX, minY, maxX - minX, maxY - minY);

        }

    }
}

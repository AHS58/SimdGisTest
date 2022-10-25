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

        public static TmlRectD GetBoundingBoxSIMD256(PtFlt[] pts)
        {
            //if (!Avx.IsSupported || pts.Length < 4)
            //{
            //    return TmlRectD.GetBoundingBox(pts);
            //}
            if ( pts.Length < 4) return TmlRectD.GetBoundingBox(pts);
           
            int Remainder = pts.Length % 4;//8 float 4 point

            ReadOnlySpan<PtFlt> bs = pts;
            ReadOnlySpan<Vector256<float>> vecDz = MemoryMarshal.Cast<PtFlt, Vector256<float>>(bs[..(pts.Length - Remainder)]);

            var vmin = Vector256.Create(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y, pts[2].X, pts[2].Y, pts[3].X, pts[3].Y);
            var vmax = Vector256.Create(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y, pts[2].X, pts[2].Y, pts[3].X, pts[3].Y);

            for (int i = 1; i < vecDz.Length; i++)
            {
                vmin = Avx.Min(vecDz[i], vmin);
                vmax = Avx.Max(vecDz[i], vmax);
            }
            float minX = vmin.GetElement(0), minY = vmin.GetElement(1);
            float maxX = vmax.GetElement(0), maxY = vmax.GetElement(1);
            for (int i = 2; i < 7; i += 2)//2,4,6
            {
                minX = Math.Min(vmin.GetElement(i), minX);
                minY = Math.Min(vmin.GetElement(i + 1), minY);
                maxX = Math.Max(vmax.GetElement(i), maxX);
                maxY = Math.Max(vmax.GetElement(i + 1), maxY);
            }
            for (int i = pts.Length-Remainder; i < pts.Length; i++)//Remain
            {
                minX = Math.Min(bs[i].X, minX);
                minY = Math.Min(bs[i].Y, minY);
                maxX = Math.Max(bs[i].X, maxX);
                maxY = Math.Max(bs[i].Y, maxY);
            }

            return new TmlRectD(minX, minY, maxX - minX, maxY - minY);

        }

    }
}

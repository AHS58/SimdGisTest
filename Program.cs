

using SimdGisTest;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

internal class Program
{
    private static void Main(string[] args)
    {
        PtDbl[] dz = PtDbl.GetRandomPointArray(1000);

        if (!Avx.IsSupported || dz.Length < 2)
        {
            Console.WriteLine("not supported" );
            return;
        }
        TmlRectD test = GisIntrinsic.GetBoundingBoxSIMD256(new PtDbl[] {new(1,1),new(0,0) } );

        Stopwatch sw = Stopwatch.StartNew();
        TmlRectD bboxSIMD = GisIntrinsic.GetBoundingBoxSIMD256(dz);
        sw.Stop();

        Console.WriteLine(bboxSIMD.ToStringTwoSH());
        Console.WriteLine("elapsed:" + sw.Elapsed);

        sw.Restart();
        TmlRectD bbox = TmlRectD.GetBoundingBox(dz);
        sw.Stop();

        Console.WriteLine(bbox.ToStringTwoSH());
        Console.WriteLine("elapsed:" + sw.Elapsed);
    }
}
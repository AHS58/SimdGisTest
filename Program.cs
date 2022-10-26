

using SimdGisTest;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

internal class Program
{
    private static void Main(string[] args)
    {
        if (!Avx.IsSupported )
        {
            Console.WriteLine("not supported");
            return;
        }
        TmlRectD testD = GisIntrinsic.GetBoundingBoxSIMD256(new PtDbl[] { new(1, 1), new(0, 0) });
        TmlRectD testF = GisIntrinsic.GetBoundingBoxSIMD256(new PtFlt[] { new(1, 1), new(0, 0), new(1, 1), new(0, 0) });

        tstDbl();
        tstFlt();
      
    }
    static void tstDbl()
    {
        PtDbl[] dz = PtDbl.GetRandomPointArray(100_000);
        if ( dz.Length < 2) return;
      
        Stopwatch sw = Stopwatch.StartNew();

        TmlRectD bboxSIMD = GisIntrinsic.GetBoundingBoxSIMD256(dz);
        sw.Stop();
        var swe = sw.Elapsed;

        Console.WriteLine(bboxSIMD.ToStringTwoSH() + " SimdDouble:" + sw.Elapsed);

        sw.Restart();
        TmlRectD bbox = TmlRectD.GetBoundingBox(dz);
        sw.Stop();

        Console.WriteLine(bbox.ToStringTwoSH() + " LoopDouble:" + sw.Elapsed + " s/l=" + sw.Elapsed / swe);
        Console.WriteLine("---------------------------------------------------");
    }
    static void tstFlt()
    {
        //PtFlt[] dzf = PtFlt.GetRandomPointArray(12);
        PtFlt[] dzf = PtFlt.GetRandomPointArray(100_000);
        if ( dzf.Length < 4)return;

       
        Stopwatch sw = Stopwatch.StartNew();

        TmlRectD bboxSIMDf = GisIntrinsic.GetBoundingBoxSIMD256(dzf);
        sw.Stop();
        var swe = sw.Elapsed;

        Console.WriteLine(bboxSIMDf.ToStringTwoSH() + " SimdFloat:" + sw.Elapsed);

        sw.Restart();
        TmlRectD bboxf = TmlRectD.GetBoundingBox(dzf);
        sw.Stop();

        Console.WriteLine(bboxf.ToStringTwoSH() + " LoopFloat:" + sw.Elapsed + " s/l=" + sw.Elapsed / swe);
        Console.WriteLine();
    }
}
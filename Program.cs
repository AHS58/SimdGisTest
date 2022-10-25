

using SimdGisTest;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

internal class Program
{
    private static void Main(string[] args)
    {
        PtDbl[] dz = PtDbl.GetRandomPointArray(10000);

        if (!Avx.IsSupported || dz.Length < 2)
        {
            Console.WriteLine("not supported");
            return;
        }
        TmlRectD testD = GisIntrinsic.GetBoundingBoxSIMD256(new PtDbl[] { new(1, 1), new(0, 0) });
        TmlRectD testF = GisIntrinsic.GetBoundingBoxSIMD256(new PtFlt[] { new(1, 1), new(0, 0), new(1, 1), new(0, 0) });

        Stopwatch sw = Stopwatch.StartNew();

        TmlRectD bboxSIMD = GisIntrinsic.GetBoundingBoxSIMD256(dz);
        sw.Stop();

        Console.WriteLine(bboxSIMD.ToStringTwoSH() + " SimdDouble:" + sw.Elapsed);

        sw.Restart();
        TmlRectD bbox = TmlRectD.GetBoundingBox(dz);
        sw.Stop();

        Console.WriteLine(bbox.ToStringTwoSH() + " LoopDouble:" + sw.Elapsed);
        Console.WriteLine("---------------------------------------------------");
        //float****


        PtFlt[] dzf = PtFlt.GetRandomPointArray(10000);     

        sw.Restart();
        TmlRectD bboxSIMDf = GisIntrinsic.GetBoundingBoxSIMD256(dzf);
        sw.Stop();

        Console.WriteLine(bboxSIMDf.ToStringTwoSH()+" SimdFloat:" + sw.Elapsed);

        sw.Restart();
        TmlRectD bboxf = TmlRectD.GetBoundingBox(dzf);
        sw.Stop();

        Console.WriteLine(bboxf.ToStringTwoSH()+" LoopFloat:" + sw.Elapsed);
        Console.WriteLine();

    }
}
using System.Runtime.InteropServices;

namespace SimdGisTest
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PtFlt
    {
        public float X { get; }
        public float Y { get; }
        public PtFlt(float x, float y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"X:{X:f7} Y:{Y:f7}";
        }
        //--------------------------------------------------
        public static PtFlt[] GetRandomPointArray(int count)
        {
            Random r = new();
            PtFlt[] result = new PtFlt [count];

            for (int i = 0; i < count; i++)
            {
                int sd = r.Next(0, 180);
                result[i] = new(r.NextSingle() * sd, r.NextSingle() * sd);
            }

            return result;
        }
    }
}

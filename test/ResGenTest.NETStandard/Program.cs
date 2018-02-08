using System;

namespace ResGenTest.NETCore
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(Sample.SampleKey);
        }
    }
}

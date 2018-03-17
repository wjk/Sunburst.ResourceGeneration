using System;

namespace ResGenTest.NETCore
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(Sample.SampleKey);
            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            Console.WriteLine(Sample.SampleKey);
        }
    }
}

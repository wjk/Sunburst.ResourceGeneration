using System;

namespace ResGenTest.Resx
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(Resources.HelloString);
            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            Console.WriteLine(Resources.HelloString);
        }
    }
}

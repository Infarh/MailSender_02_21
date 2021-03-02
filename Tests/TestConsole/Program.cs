using System;
// ReSharper disable AsyncConverter.AsyncWait

namespace TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            //TPLOverview.Test();
            //TaskTests.Run();
            TaskTests.RunAsync().Wait();

            Console.WriteLine("Главный поток завершил работу!");
            Console.ReadLine();
        }

      
    }
}

using System;

namespace TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            //TPLOverview.Test();
            TaskTests.Run();

            Console.WriteLine("Главный поток завершил работу!");
            Console.ReadLine();
        }

      
    }
}

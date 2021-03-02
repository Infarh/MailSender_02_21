using System;
using System.Threading.Tasks;
// ReSharper disable AsyncConverter.ConfigureAwaitHighlighting

namespace TestConsole
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            //TPLOverview.Test();
            //TaskTests.Run();
            await TaskTests.RunAsync();

            Console.WriteLine("Главный поток завершил работу!");
            Console.ReadLine();
        }

      
    }
}

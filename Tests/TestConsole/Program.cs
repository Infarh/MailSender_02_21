using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            //ExcelCreator.Create();
            //ThreadsTest.Start();
            //ThreadPoolTest.Start();
            //CriticalSection.Test();
            ThreadManagement.Test();

            Console.WriteLine("Главный поток завершил работу!");
            Console.ReadLine();
        }
    }
}

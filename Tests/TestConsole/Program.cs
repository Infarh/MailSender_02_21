using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            //ExcelCreator.Create();

            var clock_thread = new Thread(ThreadMethod)
            {
                Priority = ThreadPriority.Highest,
                Name = "Clock thread"
            };
            clock_thread.Start();

            var message = "Hello World!";

            var printer_thread = new Thread(() => Printer(message));
            printer_thread.Start();

            new Thread(PrinterObj).Start(message);

            Console.ReadLine();
        }

        private static void ThreadMethod()
        {
            while (true)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss.ffff");
                Thread.Sleep(100);
            }
        }

        private static void PrintThreadInfo()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine(
                "id:{0}; name:{1}; priority:{2}",
                thread.ManagedThreadId,
                thread.Name,
                thread.Priority);
        }


        private static void Printer(string Message)
        {
            PrintThreadInfo();

            var thread_id = Thread.CurrentThread.ManagedThreadId;
            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine("id:{0}\t{1}", thread_id, Message);
                Thread.Sleep(10);
            }
        }

        private static void PrinterObj(object obj)
        {
            var message = (string) obj;

            PrintThreadInfo();

            var thread_id = Thread.CurrentThread.ManagedThreadId;
            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine("id:{0}\t{1}", thread_id, message);
                Thread.Sleep(10);
            }
        }
    }
}

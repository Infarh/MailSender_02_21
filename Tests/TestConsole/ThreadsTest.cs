using System;
using System.Threading;

namespace TestConsole
{
    internal static class ThreadsTest
    {
        public static void Start()
        {
            var clock_thread = new Thread(ThreadMethod)
            {
                Priority = ThreadPriority.Highest,
                Name = "Clock thread"
            };
            clock_thread.Start();

            var message1 = "Lowest";
            var message2 = "Highest";

            var printer_thread = new Thread(() => Printer(message1)) { Priority = ThreadPriority.Lowest };
            printer_thread.Start();

            new Thread(PrinterObj) { Priority = ThreadPriority.Highest }.Start(message2);

            var print_task = new PrintTask(500, "123");
            print_task.Start();

            //printer_thread.Join();

            if (!printer_thread.Join(1000))
                printer_thread.Interrupt();
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
            var message = (string)obj;

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

using System;
using System.Linq;
using System.Threading;

namespace TestConsole
{
    internal static class ThreadPoolTest
    {
        public static void Start()
        {
            var messages = Enumerable.Range(1, 100).Select(i => $"Message-{i}").ToArray();

            //for (var i = 0; i < messages.Length; i++)
            //{
            //    var i0 = i;
            //    new Thread(() => PrintMessage(messages[i0])).Start();
            //}

            //ThreadPool.SetMinThreads(15,15);
            //ThreadPool.SetMaxThreads(20,20);
            for (var i = 0; i < messages.Length; i++)
            {
                var i0 = i;
                ThreadPool.QueueUserWorkItem(_ => PrintMessage(messages[i0]));
            }
        }

        private static void PrintMessage(string Message)
        {
            Thread.Sleep(100);
            Console.WriteLine("id:{0} - {1}",
                Thread.CurrentThread.ManagedThreadId,
                Message);
        }
    }
}

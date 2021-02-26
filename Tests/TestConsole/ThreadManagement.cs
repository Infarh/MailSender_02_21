using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    internal static class ThreadManagement
    {
        public static void Test()
        {
            var threads = new Thread[10];

            var manual_reset_event = new ManualResetEvent(false);
            var auto_reset_event = new AutoResetEvent(false);

            for (var i = 0; i < threads.Length; i++)
            {
                var i0 = i;
                threads[i] = new Thread(() => Printer($"Сообщение {i0}", auto_reset_event));
                threads[i].Start();
            }

            Console.WriteLine("Потоки созданы. Готовы к запуску.");
            Console.ReadLine();

            auto_reset_event.Set();

            Console.ReadLine();
            auto_reset_event.Set();

            Console.ReadLine();
            auto_reset_event.Set();

            Console.ReadLine();
            auto_reset_event.Set();

            Mutex app_mutex = new Mutex(true, "MyAppMutex", out var is_owner);
            Semaphore semaphore = new Semaphore(0, 10);

            //semaphore.WaitOne(5);
            semaphore.WaitOne();

            semaphore.Release();
            //semaphore.Release(5);
        }

        private static void Printer(string Message, EventWaitHandle Event)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Поток id:{0} начал работу", thread_id);

            Event.WaitOne();

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("id:{0}\t{1}", thread_id, Message);
                Thread.Sleep(10);
            }
            Console.WriteLine("Поток id:{0} завершил работу", thread_id);
        }
    }
}

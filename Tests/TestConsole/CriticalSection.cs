using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    internal static class CriticalSection
    {
        public static void Test()
        {
            var messages = Enumerable.Range(1, 10).Select(i => $"Message-{i}").ToArray();
            //System.Collections.Concurrent.ConcurrentDictionary<>

            for (var i = 0; i < messages.Length; i++)
            {
                var i0 = i;
                //if(i % 2 == 0)
                new Thread(() => PrintMessageWithLock(messages[i0], 50)).Start();
                //else
                //    new Thread(() => PrintMessage(messages[i0], 50)).Start();
            }

            //var lengths = new List<int>();
            //var threads = new Thread[messages.Length];
            //for (var i = 0; i < messages.Length; i++)
            //{
            //    var i0 = i;
            //    //threads[i] = new Thread(() => lengths.Add(messages[i0].Length));
            //    threads[i] = new Thread(() =>
            //    {
            //        lock (lengths) lengths.Add(messages[i0].Length);
            //    });
            //}

            //foreach (var thread in threads)
            //    thread.Start();

        }

        private static readonly object __SyncRoot = new object();

        private static void PrintMessage(string Message, int Count)
        {
            var thread = Thread.CurrentThread;

            for (var i = 0; i < Count; i++)
            {
                Console.Write("thId:{0}", thread.ManagedThreadId);
                Console.Write("\t");
                Console.Write(Message);
                Console.WriteLine();

                Thread.Sleep(100);
            }
        }

        private static void PrintMessageWithLock(string Message, int Count)
        {
            var thread = Thread.CurrentThread;

            for (var i = 0; i < Count; i++)
            {
                lock (__SyncRoot)
                {
                    Console.Write("thId:{0}", thread.ManagedThreadId);
                    Console.Write("\t");
                    Console.Write(Message);
                    Console.WriteLine();
                }

                Thread.Sleep(100);
            }
        }

        private static void PrintMessageWithLockInfo(string Message, int Count)
        {
            var thread = Thread.CurrentThread;

            for (var i = 0; i < Count; i++)
            {
                Monitor.Enter(__SyncRoot);
                try
                {
                    Console.Write("thId:{0}", thread.ManagedThreadId);
                    Console.Write("\t");
                    Console.Write(Message);
                    Console.WriteLine();
                }
                finally
                {
                    Monitor.Exit(__SyncRoot);
                }

                Thread.Sleep(100);
            }
        }
    }
}

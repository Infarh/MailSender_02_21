using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    internal static class TPLOverview
    {
        public static void Test()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Console.Title = DateTime.Now.ToLongTimeString();
                    Thread.Sleep(100);
                }
            })
            { IsBackground = true }.Start();

            Action<string> printer = str =>
            {
                Console.WriteLine(
                    "Сообщение из потока id:{0} - {1}",
                    Thread.CurrentThread.ManagedThreadId, str);
                Thread.Sleep(3000);
                Console.WriteLine("Печать в потоке {0} завершена", Thread.CurrentThread.ManagedThreadId);
            };

            //printer("Hello World!");

            //printer.BeginInvoke("111", result => { Console.WriteLine("Операция завершилась"); }, null);

            //Func<long, long> get_factorial = Factorial;

            //get_factorial.BeginInvoke(30, result =>
            //{
            //    var y = get_factorial.EndInvoke(result);
            //    Console.WriteLine("Результат: {0}", y);
            //}, null);

            //var worker = new BackgroundWorker();
            //worker.DoWork += (s, e) =>
            //{
            //    var w = (BackgroundWorker)s;
            //    //w.ReportProgress(50);
            //    e.Result = Factorial((long)e.Argument!);
            //};
            //worker.ProgressChanged += (_, e) => Console.WriteLine("Прогресс операции {0}", e.ProgressPercentage);
            //worker.RunWorkerCompleted += (_, e) => Console.WriteLine("Результат операции {0}", e.Result);
            //worker.RunWorkerAsync((long)30);

            //var value = 42;
            //var str = $"Message-{value}";
            //var str2 = string.Format("Message-{0}", value);

            ////var factorial_task = Task.Run(() => Factorial(20));

            //var print_task = new Task(() => printer("Hello World"));
            //print_task.Start();

            //print_task.Wait();

            //async/await 

            //Parallel.Invoke(
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    () => Console.WriteLine("Ещё один метод")
            //    );

            //Parallel.For(0, 100, i => ParallelInvokeMethod($"Message - {i}"));
            //Parallel.For(0, 100, 
            //    new ParallelOptions { MaxDegreeOfParallelism = 10 },
            //    i => ParallelInvokeMethod($"Message - {i}"));

            //var parallel_loop_result = Parallel.For(0, 100,
            //    new ParallelOptions { MaxDegreeOfParallelism = 10 },
            //    (i, state) =>
            //    {
            //        if(i > 10) state.Break();
            //        ParallelInvokeMethod($"Message - {i}");
            //    });
            //Console.WriteLine("Операция прервана на номере {0}", parallel_loop_result.LowestBreakIteration);

            //var messages = Enumerable.Range(1, 100).Select(i => $"Message {i}");

            //Parallel.ForEach(messages, new ParallelOptions { MaxDegreeOfParallelism = 4 }, printer);

            //var cancellation = new CancellationTokenSource();
            //var cancel = cancellation.Token;

            //var messages_with_end7_count = messages
            //   .AsParallel()
            //   .WithDegreeOfParallelism(10)
            //   .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
            //   .WithMergeOptions(ParallelMergeOptions.AutoBuffered)
            //   .WithCancellation(cancel)
            //   .Select(msg => MessageProcessing(msg))
            //   .Where(msg => msg.EndsWith("7\""))
            //   .ToArray();
        }

        private static string MessageProcessing(string Message)
        {
            Thread.Sleep(250);
            return $"Processed msg:\"{Message}\"";
        }

        private static long Factorial(long x)
        {
            var result = 1l;
            while (x > 1)
            {
                result *= x;
                x--;

                Thread.Sleep(100);
            }

            return result;
        }

        private static void ParallelInvokeMethod()
        {
            Console.WriteLine("ThrID:{0} - started", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(250);
            Console.WriteLine("ThrID:{0} - finished", Thread.CurrentThread.ManagedThreadId);
        }

        private static void ParallelInvokeMethod(string Message)
        {
            Console.WriteLine("ThrID:{0} - started - {1}", Thread.CurrentThread.ManagedThreadId, Message);
            Thread.Sleep(250);
            Console.WriteLine("ThrID:{0} - finished", Thread.CurrentThread.ManagedThreadId);
        }
    }
}

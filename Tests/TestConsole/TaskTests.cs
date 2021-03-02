using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable AsyncConverter.AsyncWait

namespace TestConsole
{
    internal static class TaskTests
    {
        public static void Run()
        {
            //var compute_value30_task = new Task<long>(() => IntSum(30));
            //compute_value30_task.Start();

            var compute_value30_task = Task.Run(() => IntSum(30));
            var compute_value45_task = Task.Run(() => IntSum(45));
            var compute_value50_task = Task.Run(() => IntSum(50));
            var compute_value80_task = Task.Factory.StartNew(() => IntSum(80));
            var x = 90;
            var compute_value_x_task = Task.Factory.StartNew(obj => IntSum((long)obj), x);
            var compute_value_100000_task = Task.Factory.StartNew(() => IntSum(100000), TaskCreationOptions.LongRunning);

            #region Плохо!
            //var sum_of_30 = compute_value30_task.Result;
            compute_value30_task.Wait();

            Task.WaitAll(
                compute_value30_task,
                compute_value45_task,
                compute_value50_task,
                compute_value80_task,
                compute_value_x_task,
                compute_value_100000_task); 

            Task.WaitAny(
                compute_value30_task,
                compute_value45_task,
                compute_value50_task,
                compute_value80_task,
                compute_value_x_task,
                compute_value_100000_task);
            #endregion
        }



        private static long IntSum(long x)
        {
            if (x < 0) return IntSum(-x);

            var result = 0l;
            while (x > 0)
            {
                result += x;
                x--;

                Thread.Sleep(10);
            }

            return result;
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

// ReSharper disable AsyncConverter.ConfigureAwaitHighlighting

namespace TestWPF
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private CancellationTokenSource _CalculationCancellation;

        private async void StartButtonClick(object sender, RoutedEventArgs e)
        {
            //var thread_id = Thread.CurrentThread.ManagedThreadId;

            var button = (Button)sender;
            button.IsEnabled = false;
            CancelButton.IsEnabled = true;

            var cancellation = new CancellationTokenSource();
            _CalculationCancellation = cancellation;
            var progress = new Progress<double>(
                value =>
                {
                    ProgressInformer.Value = value;
                    PercentProgressInfo.Text = value.ToString("p");
                });

            //var result = await Task.Run(() => IntSum(500)).ConfigureAwait(true);
            try
            {
                var result = await IntSumAsync(500, progress, cancellation.Token)
                   .ConfigureAwait(true);

                ((IProgress<double>)progress).Report(0);

                //var thread_id2 = Thread.CurrentThread.ManagedThreadId;

                ResultTextBlock.Text = result.ToString();
                
            }
            catch (OperationCanceledException)
            {
                ResultTextBlock.Text = "Операция отменена";
                ((IProgress<double>)progress).Report(0);
            }
            CancelButton.IsEnabled = false;
            button.IsEnabled = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _CalculationCancellation?.Cancel();
        }

        private static long IntSum(long x)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;

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

        private static async Task<long> IntSumAsync(
            long X,
            IProgress<double> Progress = default,
            CancellationToken Cancel = default)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;

            Cancel.ThrowIfCancellationRequested();

            if (X < 0) return await IntSumAsync(-X).ConfigureAwait(false);

            var result = 0l;
            var x = 1;
            while (x <= X)
            {
                if (Cancel.IsCancellationRequested)
                {
                    // Подготовиться к отмене операции, почистить ресурсы
                    Cancel.ThrowIfCancellationRequested();
                }

                result += x;
                x++;

                Progress?.Report((double)x / X);

                await Task.Delay(10, Cancel).ConfigureAwait(false);
                //Thread.Sleep(10);
            }

            return result;
        }
    }
}

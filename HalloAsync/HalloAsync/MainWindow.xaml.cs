using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HalloAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(1000);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = !true;
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => { pb1.Value = i; });
                    Thread.Sleep(100);
                }
                pb1.Dispatcher.Invoke(() => { ((Button)sender).IsEnabled = true; });
            });
        }

        private void StartTaskMitTS(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            cts = new CancellationTokenSource();
            TaskScheduler ts = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Factory.StartNew(() => { pb1.Value = i; }, CancellationToken.None, TaskCreationOptions.None, ts);
                    Thread.Sleep(100);
                    if (cts.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }).ContinueWith(t => { ((Button)sender).IsEnabled = true; }, CancellationToken.None, TaskContinuationOptions.None, ts);
        }

        CancellationTokenSource cts;

        private void Abort(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private async void StartAA(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Value = i;
                    await Task.Delay(100, cts.Token);
                }
            }
            catch (TaskCanceledException) { MessageBox.Show("Abort erfolgreich"); }
            catch (Exception ex) { MessageBox.Show("ERROR"); }
        }


        public long OldSlowFunction()
        {
            Thread.Sleep(5000);
            return 3298773984723;
        }

        public Task<long> OldSlowFunctionAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => OldSlowFunction(), cancellationToken);
        }


        private async void StartOldSlow(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            cts = new CancellationTokenSource();

            //MessageBox.Show($"OldSlow: {OldSlowFunction()}");
            MessageBox.Show($"OldSlow: {await OldSlowFunctionAsync(cts.Token)}");
            ((Button)sender).IsEnabled = true;

        }
    }
}
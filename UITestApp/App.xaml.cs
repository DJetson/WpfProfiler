using UITestApp.ViewModels;
using UITestApp.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UITestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        bool IsProfilingEnabled = true;
        Stopwatch timer = new Stopwatch();
        List<long> times = new List<long>();

        int testItemCount = 250000;
        int seed = 0;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (IsProfilingEnabled)
                StartupWithProfiling(25);
            else
                StartupWithoutProfiling();
        }

        private void StartupWithoutProfiling()
        {
            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
            MainWindow.Show();
        }

        private void StartupWithProfiling(int SampleSize = 5)
        {
            var mainViewModel = new MainWindowViewModel(testItemCount, false, seed);
            MainWindow = new MainWindow() { DataContext = mainViewModel };
            MainWindow.ContentRendered += Win_ContentRendered;
            times.Add(timer.ElapsedMilliseconds);
            for (int i = 0; i < SampleSize; i++)
            {
                //var win = new MainWindow() { DataContext = mainViewModel };
                //Debug.WriteLine($"Starting StopWatch on MainWindow.Show()...");
                timer.Start();
                MainWindow.ShowDialog();
                //win.Show();

                //Debug.WriteLine($"Stopping StopWatch on MainWindow.Show()...");
                //Debug.WriteLine($"Elapsed Time: {timer.ElapsedMilliseconds}ms");
            }

            LaunchVisualizationProcess();
            
            var profiler = new ProfilerCharts.Views.MainWindow() { DataContext = new ProfilerCharts.ViewModels.MainWindowViewModel(times) };
            var tmp = MainWindow;
            MainWindow = profiler;
            //StringBuilder args = new StringBuilder($"{times.First()}");

            //for(int i = 0; i < times.Count; i++)
            //{
            //    args.Append($",{times[i]}");
            //}

            //AppDomain profilerDomain = AppDomain.CreateDomain(nameof(profilerDomain));
            //profilerDomain.Load(ProfilerCharts.ViewModels.MainWindowViewModel.GetChartAssembly().FullName);
            //profilerDomain.ExecuteAssemblyByName((typeof(ProfilerCharts.App).Assembly.FullName),new string[] { args.ToString() });
            MainWindow.Show();
            tmp.Close();
            times.Remove(times.Min());
            times.Remove(times.Max());

            Debug.WriteLine($"\nAverage: {times.Average()}");
        }

        private void LaunchVisualizationProcess()
        {
        }

        private void Win_ContentRendered(object sender, EventArgs e)
        {
            timer.Stop();
            MainWindow.Hide();

            var mainViewModel = new MainWindowViewModel(testItemCount, false, seed);
            var tmpWin = new MainWindow() { DataContext = mainViewModel };
            tmpWin.ContentRendered += Win_ContentRendered;

            MainWindow.Close();

            MainWindow = tmpWin;
            times.Add(timer.ElapsedMilliseconds);
            Debug.WriteLine($"T-{times.Count - 1} = {times.Last()}ms");
            timer.Reset();
        }
    }
}

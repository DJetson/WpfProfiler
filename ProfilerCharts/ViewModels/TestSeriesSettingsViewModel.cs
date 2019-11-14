using MVVM.Common.Commands;
using MVVM.Common.ViewModels;
using ProfilerCharts.Interfaces;
using ProfilerCharts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UITestApp.ViewModels;
using UITestApp.Views;

namespace ProfilerCharts.ViewModels
{
    public class TestSeriesSettingsViewModel : ViewModelBase
    {
        public DelegateCommand GenerateRandomizedTestCommand => new DelegateCommand(CanExecuteGenerateRandomizedTest, ExecuteGenerateRandomizedTest);

        private bool CanExecuteGenerateRandomizedTest(object parameter)
        {
            return true;
        }

        private void ExecuteGenerateRandomizedTest(object parameter)
        {
            var testSettingsItem = GenerateRandomizedTest(250, false, 0);
            testSettingsItem.OrderId = TestSettingsItems.Count + 1;
            TestSettingsItems.Add(testSettingsItem);
        }

        private TestSettingsViewModel _SelectedTestItem;
        public TestSettingsViewModel SelectedTestItem
        {
            get => _SelectedTestItem;
            set { _SelectedTestItem = value; NotifyPropertyChanged(); }
        }

        public TestSettingsViewModel GenerateRandomizedTest(int MaxItemCount, bool RandomizeItemCount, int randomSeed)
        {
            var testSettings = new TestSettingsViewModel();
            Random rng;
            if (randomSeed == -1)
            {
                rng = new Random((int)DateTime.Now.Ticks);
            }
            else
            {
                rng = new Random(randomSeed);
            }
            int ItemCount = 0;

            ItemCount = RandomizeItemCount ? rng.Next(MaxItemCount) : MaxItemCount;

            testSettings.TestDataItemCount = ItemCount;

            for (int i = 0; i < ItemCount; i++)
            {
                testSettings.MockDataSet.Items.Add(new TestViewModel(rng) { Id = i + 1 });
            }

            return testSettings;
        }

        private ObservableCollection<object> _TestSettingsItems = new ObservableCollection<object>();
        public ObservableCollection<object> TestSettingsItems
        {
            get => _TestSettingsItems;
            set { _TestSettingsItems = value; NotifyPropertyChanged(); }
        }

        //public ObservableCollection<long> Times
        //{
        //    get => ((TestSettingsViewModel)TestSettingsItems[0]).IterationTimes;
        //}

        private ObservableCollection<IResultsViewModel> _TestSeriesResults = new ObservableCollection<IResultsViewModel>();
        public ObservableCollection<IResultsViewModel> TestSeriesResults
        {
            get => _TestSeriesResults;
            set { _TestSeriesResults = value; NotifyPropertyChanged(); }
        }

            //= new List<long>();
        public Stopwatch timer = new Stopwatch();
        private Window MainWindow;
        int nextTestIndex = 0;

        public void ExecuteTestSeriesAlt()
        {
            foreach(var item in TestSettingsItems)
            {
                var test = ((TestSettingsViewModel)item).BeginTest();
                var average = new AverageResultsViewModel(test.Results.Count, (long)test.Results.Average(e => e.DeltaTime));
                average.SeriesName = $"{test.SeriesName}(Average)"; 
                TestSeriesResults.Add(test);
                TestSeriesResults.Add(average);
            }
        }

        //public void ExecuteTestSeriesAlt()
        //{
        //    var mainViewModel = TestSettingsItems[nextTestIndex] as TestSettingsViewModel;

        //    if (mainViewModel == null)
        //        throw new Exception($"TestSettingsItems[{nextTestIndex}] is null or failed to successfully cast to TestSettingsViewModel type");

        //    for (int i = 0; i < TestSettingsItems.Count; i++)
        //    {
        //        for (int j = 0; j < mainViewModel.Iterations; j++)
        //        {
        //            var content = (FrameworkElement)Activator.CreateInstance(mainViewModel.Target);
        //            content.DataContext = mainViewModel.MockDataSet;
        //            content.Loaded += Content_Loaded;
        //            MainWindow = new TestFrameworkView();
        //            (MainWindow as TestFrameworkView).TargetHost.Content = content;
        //            times.Add(timer.ElapsedMilliseconds);

        //            timer.Start();
        //            MainWindow.ShowDialog();
        //        }
        //    }
        //    times.Remove(times.Min());
        //    times.Remove(times.Max());

        //    Debug.WriteLine($"\nAverage: {times.Average()}");
        //}

        //private void Content_Loaded(object sender, RoutedEventArgs e)
        //{
        //    timer.Stop();
        //    MainWindow.Hide();
        //    nextTestIndex++;
        //    if (nextTestIndex < TestSettingsItems.Count)
        //    {
        //        var mainViewModel = TestSettingsItems[nextTestIndex] as TestSettingsViewModel;
        //        var content = (FrameworkElement)Activator.CreateInstance(mainViewModel.Target);
        //        content.DataContext = mainViewModel.MockDataSet;
        //        content.Loaded += Content_Loaded;
        //        var tmpWin = new TestFrameworkView();
        //        (tmpWin as TestFrameworkView).TargetHost.Content = content;

        //        MainWindow.Close();

        //        MainWindow = tmpWin;
        //    }
        //    else
        //    {
        //        MainWindow.Close();
        //    }
        //    Times.Add(timer.ElapsedMilliseconds);
        //    Debug.WriteLine($"T-{Times.Count - 1} = {Times.Last()}ms");
        //    timer.Reset();
        //}

        public void EnqueueTests()
        {
            //Queue<TestSettingsViewModel> testQueue = new Queue<TestSettingsViewModel>();

            //foreach (var testSettingsItem in TestSettingsItems.Cast<TestSettingsViewModel>().OrderBy(e => e.OrderId))
            //{
            //    testQueue.Enqueue(testSettingsItem);
            //}

            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        var action = testQueue.Dequeue();
            //        action(resource);
            //    }
            //});

            ////Now to do some work you simply add something to the queue...
            //queue.Add((sb) => sb.Append("Hello"));
            //queue.Add((sb) => sb.Append(" World"));

            //queue.Add((sb) => Console.WriteLine("Content: {0}", sb.ToString()));

            //Console.ReadLine();
        }

        private void LaunchVisualizationProcess()
        {
        }

    }
}

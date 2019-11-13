using MVVM.Common.Commands;
using MVVM.Common.ViewModels;
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

        public List<long> times = new List<long>();
        public Stopwatch timer = new Stopwatch();
        private Window MainWindow;
        int nextTestIndex = 0;

        public void ExecuteTestSeriesAlt()
        {
            var mainViewModel = TestSettingsItems[nextTestIndex] as TestSettingsViewModel;
            var content = (FrameworkElement)Activator.CreateInstance(mainViewModel.Target);
            content.DataContext = mainViewModel.MockDataSet;
            content.Loaded += Content_Loaded;
            MainWindow = new TestFrameworkView();
            (MainWindow as TestFrameworkView).TargetHost.Content = content;
            times.Add(timer.ElapsedMilliseconds);
            for (int i = 0; i < TestSettingsItems.Count; i++)
            {

                timer.Start();
                MainWindow.ShowDialog();
            }
            times.Remove(times.Min());
            times.Remove(times.Max());

            Debug.WriteLine($"\nAverage: {times.Average()}");
        }

        private void Content_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            MainWindow.Hide();
            nextTestIndex++;
            if (nextTestIndex < TestSettingsItems.Count)
            {
                var mainViewModel = TestSettingsItems[nextTestIndex] as TestSettingsViewModel;
                var content = (FrameworkElement)Activator.CreateInstance(mainViewModel.Target);
                content.DataContext = mainViewModel.MockDataSet;
                content.Loaded += Content_Loaded;
                var tmpWin = new TestFrameworkView();
                (tmpWin as TestFrameworkView).TargetHost.Content = content;

                MainWindow.Close();

                MainWindow = tmpWin;
            }
            else
            {
                MainWindow.Close();
            }
            times.Add(timer.ElapsedMilliseconds);
            Debug.WriteLine($"T-{times.Count - 1} = {times.Last()}ms");
            timer.Reset();
        }

        private void LaunchVisualizationProcess()
        {
        }

    }
}

using MVVM.Common.Commands;
using MVVM.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using UITestApp.ViewModels;

namespace ProfilerCharts.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public DelegateCommand LaunchCommand => new DelegateCommand(CanLaunch, Launch);

        private TestSeriesSettingsViewModel _TestSeriesSettings = new TestSeriesSettingsViewModel();
        public TestSeriesSettingsViewModel TestSeriesSettings
        {
            get => _TestSeriesSettings;
            set { _TestSeriesSettings = value; NotifyPropertyChanged(); }
        }

        private TestSettingsViewModel _TestSettingsViewModel = new TestSettingsViewModel();
        public TestSettingsViewModel TestSettingsViewModel
        {
            get => _TestSettingsViewModel;
            set { _TestSettingsViewModel = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<ObservableCollection<KeyValuePair<int, long>>> AllResults
        {
            get
            {
                var allResults = new ObservableCollection<ObservableCollection<KeyValuePair<int, long>>>();


                foreach(var item in TestSeriesSettings.TestSeriesResults)
                {
                    var testSetResults = new ObservableCollection<KeyValuePair<int, long>>();
                    foreach(var iterationItem in item.Results)
                    {
                        var kvPair = new KeyValuePair<int, long>(iterationItem.Iteration, iterationItem.DeltaTime);
                        testSetResults.Add(kvPair);
                    }
                    allResults.Add(testSetResults);
                }
                return allResults;
            }
        }

        private void Launch(object parameter)
        {
            var uiTestApp = new UITestViewModel();
            //uiTestApp.BeginTests(TestSeriesSettings.TestSettingsItems.Count, 250);
            //TestSeriesSettings.ExecuteTestSeries();
            TestSeriesSettings.ExecuteTestSeriesAlt();
            NotifyPropertyChanged(nameof(AllResults));
            NotifyPropertyChanged(nameof(TestSeriesSettings));
            //var uiTestAppProcess = Process.Start(new ProcessStartInfo() { Arguments = $"-ti:{TestSettingsViewModel.TestIterations} -dn:{TestSettingsViewModel.TestDataItemCount}", FileName = $"{Environment.CurrentDirectory}\\UITestApp.exe" });
            //UpdateChartData(TestSeriesSettings.Times);
        }

        private bool CanLaunch(object parameter)
        {
            MainWindowViewModel Parameter = parameter as MainWindowViewModel;

            if (Parameter?.TestSeriesSettings?.TestSettingsItems == null)
                return false;

            if (Parameter.TestSeriesSettings.TestSettingsItems.Count <= 0)
                return false;

            //if (Parameter.TestSettingsViewModel.TestIterations <= 0)
            //    return false;

            return true;
        }

        private ObservableCollection<KeyValuePair<int, long>> _DataItems = new ObservableCollection<KeyValuePair<int, long>>();

        public ObservableCollection<KeyValuePair<int,long>> DataItems
        {
            get => _DataItems;
            set { _DataItems = value; NotifyPropertyChanged(); }
        }

        public MainWindowViewModel()
        {

        }

        public static Assembly GetChartAssembly()
        {
            return typeof(Chart).Assembly;
        }

        public MainWindowViewModel(IEnumerable<long> data)
        {
        }

        //public void UpdateChartData(IEnumerable<long> data)
        //{
        //    foreach (var item in data)
        //    {
        //        DataItems.Add(new KeyValuePair<int, long>(DataItems.Count + 1, item));
        //    }
        //}
    }
}

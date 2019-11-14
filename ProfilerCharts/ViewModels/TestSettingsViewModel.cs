using MVVM.Common.Interfaces;
using MVVM.Common.ViewModels;
using ProfilerCharts.Interfaces;
using ProfilerCharts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProfilerCharts.ViewModels
{
    public class TestSettingsViewModel : ViewModelBase, IBrowseForFile
    {
        /// <summary>
        /// TestSettings may be set to readonly whenever they are used to show settings already applied to 
        /// a previously run test, as in a compiled test report.
        /// </summary>
        private bool _IsReadOnly;
        public bool IsReadOnly
        {
            get => _IsReadOnly;
            set { _IsReadOnly = value; NotifyPropertyChanged(); }
        }

        private int _OrderId;
        public int OrderId
        {
            get => _OrderId;
            set { _OrderId = value; NotifyPropertyChanged(); }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set { _Name = value; NotifyPropertyChanged(); }
        }

        private Type _Target;
        public Type Target
        {
            get => _Target;
            set { _Target = value; NotifyPropertyChanged(); }
        }

        private MockDataSetViewModel _MockDataSet = new MockDataSetViewModel();
        public MockDataSetViewModel MockDataSet
        {
            get => _MockDataSet;
            set { _MockDataSet = value; NotifyPropertyChanged(); }
        }

        private int _Iterations = 1;
        public int Iterations
        {
            get => _Iterations;
            set { _Iterations = value; NotifyPropertyChanged(); }
        }

        //private ObservableCollection<long> _IterationTimes = new ObservableCollection<long>();
        //public ObservableCollection<long> IterationTimes
        //{
        //    get => _IterationTimes;
        //    set { _IterationTimes = value; NotifyPropertyChanged(); }
        //}

        /// <summary>
        /// The number of Mock DataItems to create and display in controls that display collections
        /// </summary>
        private int _TestDataItemCount = 1000;
        public int TestDataItemCount
        {
            get => _TestDataItemCount;
            set { _TestDataItemCount = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<Type> _TargetTypes = new ObservableCollection<Type>();
        public ObservableCollection<Type> TargetTypes
        {
            get => _TargetTypes;
            set { _TargetTypes = value; NotifyPropertyChanged(); }
        }

        public void LoadFiles(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                if (File.Exists(path))
                    PopulateControlsList(path, true);
            }
        }

        private void PopulateControlsList(string path, bool appendItems)
        {
            if (!appendItems)
            {
                TargetTypes.Clear();
            }

            var targetAssembly = Assembly.LoadFile(path);

            foreach (var type in targetAssembly.GetTypes().Where(e => e.IsClass && e.IsPublic && e.IsSubclassOf(typeof(FrameworkElement))))
            {
                TargetTypes.Add(type);
            }
        }

        public ResultsViewModel BeginTest()
        {
            Stopwatch timer = new Stopwatch();
            List<long> times = new List<long>();

            var results = new ResultsViewModel() { Name = Target.Name, SettingsApplied = this, TargetType = Target };

            results.Results = new ObservableCollection<IResultItemViewModel>();

            Queue<TestSettingsViewModel> testQueue = new Queue<TestSettingsViewModel>();

            for(int i = 0; i < Iterations; i++)
            {
                var iterationResult = new ResultItemViewModel()
                {
                    Iteration = i + 1,
                    StartTime = timer.ElapsedMilliseconds,
                    EndTime = ExecuteTestIteration(timer, times),
                };

                results.Results.Add(iterationResult);
            }

            return results;
            //IterationTimes = new ObservableCollection<long>(times);
        }

        public long ExecuteTestIteration(Stopwatch timer, List<long> times)
        {
            var content = (FrameworkElement)Activator.CreateInstance(Target);
            content.DataContext = MockDataSet;
            //content.Loaded += Content_Loaded;
            var MainWindow = new TestFrameworkView();
            (MainWindow as TestFrameworkView).TargetHost.Content = content;
            timer.Start();
            MainWindow.ContentRendered += (s, e) => {
                timer.Stop();
                MainWindow.Close();
            };
            MainWindow.ShowDialog();

            //MainWindow.ShowDialog();
            //timer.Stop();
            //MainWindow.Close();
            return timer.ElapsedMilliseconds;
            //times.Add(timer.ElapsedMilliseconds);
            //timer.Reset();
            //return MainWindow;
        }

        public void ExecuteTestSeriesAlt()
        {
            Stopwatch timer = new Stopwatch();
            List<long> times = new List<long>();
            for (int j = 0; j < Iterations; j++)
            {
                
            }

            times.Remove(times.Min());
            times.Remove(times.Max());

            Debug.WriteLine($"\nAverage: {times.Average()}");
        }

        public override object Clone()
        {
            var clone = new TestSettingsViewModel()
            {
                IsReadOnly = this.IsReadOnly,
                MockDataSet = (MockDataSetViewModel)this.MockDataSet.Clone(),
                Name = this.Name,
                OrderId = -1,
                Target = this.Target,
                TargetTypes = new ObservableCollection<Type>(this.TargetTypes),
                TestDataItemCount = this.TestDataItemCount
            };

            return clone;
        }
    }
}

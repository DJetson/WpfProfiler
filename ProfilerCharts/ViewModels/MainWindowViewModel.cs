using MVVM.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;

namespace ProfilerCharts.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
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
            foreach(var item in data)
            {
                DataItems.Add(new KeyValuePair<int,long>(DataItems.Count, item));
            }
        }
    }
}

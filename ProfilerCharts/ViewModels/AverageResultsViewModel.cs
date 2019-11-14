using MVVM.Common.ViewModels;
using ProfilerCharts.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilerCharts.ViewModels
{
    public class AverageResultsViewModel : ViewModelBase, IResultsViewModel
    {
        private string _SeriesName;
        public string SeriesName
        {
            get => _SeriesName;
            set { _SeriesName = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<IResultItemViewModel> _Results = new ObservableCollection<IResultItemViewModel>();
        public ObservableCollection<IResultItemViewModel> Results
        {
            get => _Results;
            set { _Results = value; NotifyPropertyChanged(); }
        }

        public AverageResultsViewModel()
        {

        }

        public AverageResultsViewModel(int count, long value)
        {
            for (int i = 0; i < count; i++)
            {
                Results.Add(new AverageResultItemViewModel() { Iteration = i + 1, DeltaTime = value });
            }
        }

        public Type ResultsType { get => GetType(); }

    }
}

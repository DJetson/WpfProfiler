using MVVM.Common.ViewModels;
using ProfilerCharts.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProfilerCharts.ViewModels
{
    public class ResultsViewModel : ViewModelBase, IResultsViewModel
    {
        private TestSettingsViewModel _SettingsApplied;
        public TestSettingsViewModel SettingsApplied
        {
            get => _SettingsApplied;
            set { _SettingsApplied = value; NotifyPropertyChanged(); }
        }

        private Type _TargetType;
        public Type TargetType
        {
            get => _TargetType; 
            set { _TargetType = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(SeriesName)); }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set { _Name = value; NotifyPropertyChanged(); }
        }

        private string _SeriesName;
        public string SeriesName
        {
            get => TargetType.Name;
            //set { _SeriesName = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<IResultItemViewModel> _Results;
        public ObservableCollection<IResultItemViewModel> Results
        {
            get => _Results;
            set { _Results = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(ResultsTimes)); }
        }

        public ObservableCollection<long> ResultsTimes
        {
            get => new ObservableCollection<long>(_Results.Select(e => e.DeltaTime));
        }

        public Type ResultsType { get => GetType(); }


    }
}

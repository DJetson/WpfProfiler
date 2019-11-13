using MVVM.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProfilerCharts.ViewModels
{
    public class ResultsViewModel : ViewModelBase
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
            set { _TargetType = value; NotifyPropertyChanged(); }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set { _Name = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<ResultItemViewModel> _Results;
        public ObservableCollection<ResultItemViewModel> Results
        {
            get => _Results;
            set { _Results = value; NotifyPropertyChanged(); }
        }

    }
}

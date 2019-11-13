using MVVM.Common.Interfaces;
using MVVM.Common.ViewModels;
using System.Collections.ObjectModel;
using UITestApp.ViewModels;

namespace ProfilerCharts.ViewModels
{
    public class MockDataSetViewModel : ViewModelBase
    {
        private ObservableCollection<TestViewModel> _Items = new ObservableCollection<TestViewModel>();
        public ObservableCollection<TestViewModel> Items
        {
            get => _Items;
            set { _Items = value; NotifyPropertyChanged(); }
        }
    }
}
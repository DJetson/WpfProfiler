using MVVM.Common.Interfaces;
using MVVM.Common.ViewModels;
using System;
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

        public override object Clone()
        {
            var clone = new MockDataSetViewModel()
            {
                Items = new ObservableCollection<TestViewModel>(this.Items)
            };

            return clone;
        }
    }
}
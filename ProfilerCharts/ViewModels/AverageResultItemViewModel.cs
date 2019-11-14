using MVVM.Common.ViewModels;
using ProfilerCharts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilerCharts.ViewModels
{
    public class AverageResultItemViewModel : ViewModelBase, IResultItemViewModel
    {
        private int _Iteration;
        public int Iteration
        {
            get => _Iteration;
            set { _Iteration = value; NotifyPropertyChanged(); }
        }

        private long _DeltaTime;
        public long DeltaTime
        {
            get => _DeltaTime;
            set { _DeltaTime = value; NotifyPropertyChanged(); }
        }
    }
}

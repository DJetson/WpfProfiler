using MVVM.Common.ViewModels;
using System;

namespace ProfilerCharts.ViewModels
{
    public class ResultItemViewModel : ViewModelBase
    {
        private int _Iteration = 0;
        public int Iteration
        {
            get => _Iteration;
            set { _Iteration = value; NotifyPropertyChanged(); }
        }

        private long _StartTime = 0;
        public long StartTime
        {
            get => _StartTime;
            set { _StartTime = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(DeltaTime)); }
        }

        private long _EndTime = 0;
        public long EndTime
        {
            get => _EndTime;
            set { _EndTime = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(DeltaTime)); }
        }

        public long DeltaTime
        {
            get => Math.Max(EndTime - StartTime,0);
        }
    }
}
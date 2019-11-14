using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilerCharts.Interfaces
{
    public interface IResultsViewModel
    {
        string SeriesName { get; }
        ObservableCollection<IResultItemViewModel> Results { get; set; }
        Type ResultsType { get; }
    }
}

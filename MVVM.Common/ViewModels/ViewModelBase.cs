using MVVM.Common.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.Common.ViewModels
{
    public class ViewModelBase : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName]string property = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}

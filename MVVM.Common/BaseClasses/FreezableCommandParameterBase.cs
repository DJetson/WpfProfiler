using MVVM.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.Common.BaseClasses
{
    public abstract class FreezableCommandParameterBase : Freezable
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ViewModelBase), typeof(FreezableCommandParameterBase), new UIPropertyMetadata(default(ViewModelBase)));
        public ViewModelBase ViewModel
        {
            get => (ViewModelBase)GetValue(ViewModelProperty);
            set { SetValue(ViewModelProperty, (ViewModelBase)value); }
        }

        protected abstract override Freezable CreateInstanceCore();


    }
}

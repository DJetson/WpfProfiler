using MVVM.Common.BaseClasses;
using MVVM.Common.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.Common.ViewModels
{
    public class ViewModelBase : NotifyBase, IViewModel, ICloneable
    {
        public virtual object Clone()
        {
            return new ViewModelBase();
        }
    }
}

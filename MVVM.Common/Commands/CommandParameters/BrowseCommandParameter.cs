using MVVM.Common.BaseClasses;
using MVVM.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.Common.Commands
{
    public class BrowseCommandParameter : FreezableCommandParameterBase
    {
        private static readonly DependencyProperty FilterProperty = DependencyProperty.Register(nameof(Filter), typeof(string), typeof(BrowseCommandParameter), new UIPropertyMetadata(default(string)));
        private static readonly DependencyProperty MultiselectProperty = DependencyProperty.Register(nameof(Multiselect), typeof(bool), typeof(BrowseCommandParameter), new UIPropertyMetadata(default(bool)));

        public string Filter
        {
            get => (string)GetValue(FilterProperty);
            set { SetValue(FilterProperty, value); }
        }

        public bool Multiselect
        {
            get => (bool)GetValue(MultiselectProperty);
            set { SetValue(MultiselectProperty, value); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new BrowseCommandParameter();
        }
    }
}

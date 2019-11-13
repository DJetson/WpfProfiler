using MVVM.Common.BaseClasses;
using MVVM.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.Common.Commands
{
    public class CollectionItemsCommandParameter : FreezableCommandParameterBase
    {
        private static readonly DependencyProperty TargetCollectionProperty = DependencyProperty.Register("TargetCollection", typeof(ObservableCollection<object>), typeof(CollectionItemsCommandParameter), new UIPropertyMetadata(default(ObservableCollection<object>)));
        public ObservableCollection<object> TargetCollection
        {
            get => ((ObservableCollection<object>)GetValue(TargetCollectionProperty));
            set { SetValue(TargetCollectionProperty, (ObservableCollection<object>)value); }
        }

        private static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<object>), typeof(CollectionItemsCommandParameter), new UIPropertyMetadata(default(ObservableCollection<object>)));
        public ObservableCollection<object> SelectedItems
        {
            get => (ObservableCollection<object>)GetValue(SelectedItemsProperty);
            set { SetValue(SelectedItemsProperty, (ObservableCollection<object>)value); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new CollectionItemsCommandParameter();
        }
    }
}

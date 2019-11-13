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
    public class DuplicateCollectionItemsCommandParameter : FreezableCommandParameterBase
    {
        private static readonly DependencyProperty CopyToCollectionProperty = DependencyProperty.Register("CopyToCollection", typeof(ObservableCollection<object>), typeof(DuplicateCollectionItemsCommandParameter), new UIPropertyMetadata(default(ObservableCollection<object>)));
        public ObservableCollection<object> CopyToCollection
        {
            get => ((ObservableCollection<object>)GetValue(CopyToCollectionProperty));
            set { SetValue(CopyToCollectionProperty, (ObservableCollection<object>)value); }
        }

        private static readonly DependencyProperty SourceItemsProperty = DependencyProperty.Register("SourceItems", typeof(ObservableCollection<object>), typeof(DuplicateCollectionItemsCommandParameter), new UIPropertyMetadata(default(ObservableCollection<object>)));
        public ObservableCollection<object> SourceItems
        {
            get => (ObservableCollection<object>)GetValue(SourceItemsProperty);
            set { SetValue(SourceItemsProperty, (ObservableCollection<object>)value); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new DuplicateCollectionItemsCommandParameter();
        }
    }
}

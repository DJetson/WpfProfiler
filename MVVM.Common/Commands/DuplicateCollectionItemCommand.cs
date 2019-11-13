using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.Common.Commands
{
    public class DuplicateCollectionItemCommand : RequeryBase
    {
        public override bool CanExecute(object parameter)
        {
            if (!(parameter is DuplicateCollectionItemsCommandParameter Parameter))
                return false;

            if (Parameter?.ViewModel == null)
                return false;
            if (Parameter.SourceItems == null)
                return false;
            if (Parameter.SourceItems.Count() == 0)
                return false;
            if (Parameter.CopyToCollection == null)
                return false;
            if (Parameter?.CopyToCollection.Count() == 0)
                return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            var Parameter = parameter as DuplicateCollectionItemsCommandParameter;

            foreach (var item in Parameter.SourceItems)
                Parameter.CopyToCollection.Add(item);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Common.Commands
{
    public class RemoveCollectionItemsCommand : RequeryBase
    {
        public override bool CanExecute(object parameter)
        {
            if (!(parameter is CollectionItemsCommandParameter Parameter))
                return false;

            if (Parameter?.ViewModel == null)
                return false;
            if (Parameter.SelectedItems == null)
                return false;
            if (Parameter.SelectedItems.Count() == 0)
                return false;
            if (Parameter.TargetCollection == null)
                return false;
            if (Parameter?.TargetCollection.Count() == 0)
                return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            var Parameter = parameter as CollectionItemsCommandParameter;

            foreach (var item in Parameter.SelectedItems.ToList())
                Parameter.TargetCollection.Remove(item);
        }
    }
}

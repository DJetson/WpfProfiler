using MVVM.Common.Commands;
using ProfilerCharts.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilerCharts.Commands
{
    public class DuplicateTestSettingsViewModelItemsCommand : DuplicateCollectionItemsCommand
    {
        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            var Parameter = parameter as CollectionItemsCommandParameter;

            for (int i = 0; i < Parameter.TargetCollection.Count; i++)
            {
                ((TestSettingsViewModel)Parameter.TargetCollection[i]).OrderId = i + 1;
            }
        }
    }
}

using Microsoft.Win32;
using MVVM.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Common.Commands
{
    public class BrowseCommand : RequeryBase
    {
        public override bool CanExecute(object parameter)
        {
            if (parameter is BrowseCommandParameter Parameter)
            {
                if (Parameter.ViewModel == null)
                    return false;
            }
            else if (!(parameter is IBrowseForFile))
            {
                return false;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            IBrowseForFile viewModel = null;
            string filter = String.Empty;
            bool multiSelect = false;

            if (parameter is BrowseCommandParameter Parameter)
            {
                viewModel = (IBrowseForFile)Parameter.ViewModel;
                filter = Parameter.Filter;
                multiSelect = Parameter.Multiselect;
            }

            viewModel = viewModel ?? parameter as IBrowseForFile;

            var openFileDialog = new OpenFileDialog()
            {
                Filter = filter,
                Multiselect = multiSelect,
                CheckPathExists = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                viewModel.LoadFiles(openFileDialog.FileNames);
            }
        }
    }
}

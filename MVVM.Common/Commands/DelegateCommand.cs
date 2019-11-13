using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Common.Commands
{
    public class DelegateCommand : RequeryBase
    {
        public DelegateCommand(Predicate<object> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        readonly Predicate<object> _canExecute;
        readonly Action<object> _execute;

        public override bool CanExecute(object parameter)
        {
            return (_canExecute?.Invoke(parameter)) ?? false;
        }

        public override void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}

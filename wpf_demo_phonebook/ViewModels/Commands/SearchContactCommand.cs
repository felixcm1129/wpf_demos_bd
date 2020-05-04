using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace wpf_demo_phonebook.ViewModels.Commands
{
    class SearchContactCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action<ContactModel> _execute;

        public SearchContactCommand(Action<ContactModel> action)
        {
            _execute = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter as ContactModel);
        }
    }
}

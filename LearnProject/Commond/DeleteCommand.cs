using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LearnProject.Commond
{
    class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExcute == null)
            {
                return true;
            }
            return canExcute(parameter);
        }

        public void Execute(object parameter)
        {
            excute(parameter);
        }
        public Action<object> excute;
        public Func<object, bool> canExcute;
    }
}

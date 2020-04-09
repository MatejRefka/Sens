using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace Sens
{
    /// <summary>
    /// A basic command that runs an Action
    /// </summary>
    
    public class RelayCommand : ICommand
    {
        //private action to run field
        private Action mAction;

        // Default constructor
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        // The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        public event EventHandler CanExecuteChanged = (sender, e) => { };


        // A relay command can always execute
        public bool CanExecute(object parameter)
        {
            return true;
        }

        // Executes the commands Action
        public void Execute(object parameter)
        {
            mAction();
        }

    }
}
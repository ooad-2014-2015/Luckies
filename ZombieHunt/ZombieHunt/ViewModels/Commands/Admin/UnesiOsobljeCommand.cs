using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZombieHunt.ViewModels.Commands
{
    public class UnesiOsobljeCommand: ICommand
    {
        public AdminVM avm { get; set; }

        public UnesiOsobljeCommand(AdminVM avm)
        {
            this.avm = avm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        #pragma warning disable 0067
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            avm.UnesiOsoblje();
        }
    }
}

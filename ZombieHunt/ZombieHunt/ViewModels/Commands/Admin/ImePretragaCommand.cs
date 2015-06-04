using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZombieHunt.ViewModels.Commands
{
    public class ImePretragaCommand: ICommand
    {
        public AdminVM avm { get; set; }

        public ImePretragaCommand(AdminVM avm)
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
            avm.FiltrirajPoImenu((parameter as TextBox).Text);
        }
    }
}

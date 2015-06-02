using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZombieHunt.ViewModels.Commands
{
    public class RezervisiOruzjeCommand: ICommand
    {
        public RezervacijaVM rvm { get; set; }

        public RezervisiOruzjeCommand(RezervacijaVM rvm)
        {
            this.rvm = rvm;
            rvm.UcitajOruzje();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        #pragma warning disable 0067
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            rvm.UcitajOruzje();
        }
    }
}

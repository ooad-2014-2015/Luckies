﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZombieHunt.ViewModels.Commands
{
    public class UcitajHranuCommand: ICommand
    {
        public UvodnaFormaVM ufvm { get; set; }

        public UcitajHranuCommand(UvodnaFormaVM ufvm)
        {
            this.ufvm = ufvm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        #pragma warning disable 0067
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ufvm.UcitajHranu();
        }
    }
}

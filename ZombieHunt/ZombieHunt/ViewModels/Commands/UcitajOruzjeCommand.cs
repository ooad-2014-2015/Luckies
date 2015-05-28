﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZombieHunt.ViewModels.Commands
{

    public class UcitajOruzjeCommand : ICommand
    {
        public UvodnaFormaVM ufvm { get; set; }

        public UcitajOruzjeCommand(UvodnaFormaVM ufvm)
        {
            this.ufvm = ufvm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ufvm.UcitajOruzje();
        }
    }
}

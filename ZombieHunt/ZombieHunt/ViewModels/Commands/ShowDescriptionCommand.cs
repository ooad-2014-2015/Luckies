﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZombieHunt.ViewModels.Commands
{
    public class ShowDescriptionCommand : ICommand
    {
        public  UvodnaFormaVM uvf { get; set; }

        public ShowDescriptionCommand(UvodnaFormaVM uvf)
        {
            this.uvf = uvf;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            uvf.ShowDescription();
        }
    }
}

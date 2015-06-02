﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZombieHunt.ViewModels.Commands
{
    public class ShowAdminCommand: ICommand
    {
        public LoginVM logic { get; set; }

        public ShowAdminCommand(LoginVM logic)
        {
            this.logic = logic;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            logic.ShowAdministratorPrivileges();
        }
    }
}

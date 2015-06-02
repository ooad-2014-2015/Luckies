using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZombieHunt.Views;
using ZombieHunt.Models;
using ZombieHunt.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ZombieHunt.ViewModels
{
    public class LoginVM : INotifyPropertyChanged
    {
        public ShowAdminCommand showAdminCommand { get; set; }
        public LoginVM()
        {
            showAdminCommand = new ShowAdminCommand(this);
        }

        public void ShowAdministratorPrivileges()
        {
            AdministratorForma af = new AdministratorForma();
            af.ShowDialog();
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #endregion
    }
}

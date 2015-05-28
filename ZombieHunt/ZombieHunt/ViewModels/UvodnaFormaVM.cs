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
    public class UvodnaFormaVM: INotifyPropertyChanged
    {
        private KomentariKolekcija kk;

        public ShowAdminCommand showAdminCommand { get; set; }
        public UcitajOpremuCommand ucitajOpremuCommand { get; set; }
        public UcitajOruzjeCommand ucitajOruzjeCommand { get; set; }


        private ObservableCollection<Komentar> _komentariOC;

        private ObservableCollection<Oprema> _opremaOC;


        public ObservableCollection<Komentar> komentariOC
        {
            get { return _komentariOC; }
            set
            {
                _komentariOC = value;
                RaisePropertyChanged("komentariOC");
            }
        }


        public ObservableCollection<Oprema> opremaOC
        {
            get { return _opremaOC; }
            set
            {
                _opremaOC = value;
                RaisePropertyChanged("opremaOC");
            }
        }


        public UvodnaFormaVM()
        {
            kk = new KomentariKolekcija();
            komentariOC = new ObservableCollection<Komentar>(kk.UcitajKomentare());
            ucitajOruzjeCommand = new UcitajOruzjeCommand(this);
            ucitajOpremuCommand = new UcitajOpremuCommand(this);
            showAdminCommand = new ShowAdminCommand(this);
            
        }

        public void ShowAdministratorPrivileges()
        {
            AdministratorForma af = new AdministratorForma();
            af.ShowDialog();
        }

        public void UcitajOruzje()
        {
            OpremaKolekcija opremaKol = new OpremaKolekcija();                              //trebaju nove instance, ne micati u konstruktor
            opremaOC = new ObservableCollection<Oprema>(opremaKol.UcitajOpremu("oruzje"));
        }

        public void UcitajOpremu()
        {
            OpremaKolekcija opremaKol = new OpremaKolekcija();
            opremaOC = new ObservableCollection<Oprema>(opremaKol.UcitajOpremu("oprema"));
        }

        


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler!=null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        
    }
}

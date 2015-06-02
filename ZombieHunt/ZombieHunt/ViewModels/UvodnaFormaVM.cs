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

      
        public UcitajOpremuCommand ucitajOpremuCommand { get; set; }
        public UcitajOruzjeCommand ucitajOruzjeCommand { get; set; }
        public ShowLogInCommand showLogInCommand { get; set; }
        public ShowHelpCommand showHelpCommand { get; set; }
        public ShowDescriptionCommand showDescriptionCommand { get; set; } 



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
      
            showLogInCommand = new ShowLogInCommand(this);
            showHelpCommand = new ShowHelpCommand(this);
            showDescriptionCommand = new ShowDescriptionCommand(this);
            
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

        public void ShowLogIn()
        {
            LogIn l = new LogIn();
            l.ShowDialog();
        }


        public void ShowHelp()
        {
            Help h = new Help();
            h.ShowDialog();
        }

        public void ShowDescription()
        {
            ONama on = new ONama();
            on.ShowDialog();
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

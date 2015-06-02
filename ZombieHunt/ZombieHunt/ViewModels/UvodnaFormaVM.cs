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
using System.Threading;

namespace ZombieHunt.ViewModels
{
    public class UvodnaFormaVM: INotifyPropertyChanged
    {
        private KomentariKolekcija kk;

        #region ICommand pokazivači
        public ShowAdminCommand showAdminCommand { get; set; }
        public PrikaziONamaCommand prikaziONamaCommand { get; set; }
        public UcitajOruzjeCommand ucitajOruzjeCommand { get; set; }
        public UcitajOpremuCommand ucitajOpremuCommand { get; set; }
        public PrikaziOsobljeCommand prikaziOsobljeCommand { get; set; }
        public UcitajHranuCommand ucitajHranuCommand { get; set; }
        public UcitajVozilaCommand ucitajVozilaCommand { get; set; }
        public ZapocniRezervacijuCommand zapocniRezervacijuCommand { get; set; }
        #endregion


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
        
        private ObservableCollection<Komentar> _komentariOC;
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
            prikaziONamaCommand = new PrikaziONamaCommand(this);
            ucitajOruzjeCommand = new UcitajOruzjeCommand(this);
            ucitajOpremuCommand = new UcitajOpremuCommand(this);
            prikaziOsobljeCommand = new PrikaziOsobljeCommand(this);
            ucitajHranuCommand = new UcitajHranuCommand(this);
            ucitajVozilaCommand = new UcitajVozilaCommand(this);
            showAdminCommand = new ShowAdminCommand(this);
            zapocniRezervacijuCommand = new ZapocniRezervacijuCommand(this);
            
        }

        public void ShowAdministratorPrivileges()
        {
            Thread t = new Thread(() =>
            {
                AdministratorForma af = new AdministratorForma();

                af.Show();
                af.Closed += (sender, e) => af.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });

            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            
        }

        public void PrikaziONama()
        {
            ONama on = new ONama();
            on.ShowDialog();
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

        public void PrikaziOsoblje()
        {
            OsobljeForma of = new OsobljeForma();
            of.ShowDialog();
        }

        public void UcitajHranu()
        {
            OpremaKolekcija opremaKol = new OpremaKolekcija();
            opremaOC = new ObservableCollection<Oprema>(opremaKol.UcitajOpremu("hrana"));
        }

        public void UcitajVozila()
        {
            OpremaKolekcija opremaKol = new OpremaKolekcija();
            opremaOC = new ObservableCollection<Oprema>(opremaKol.UcitajOpremu("vozila"));
        }

        public void ZapocniRezervaciju()
        {
            Rezervacija_pt1 rez1 = new Rezervacija_pt1();
            rez1.ShowDialog();
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

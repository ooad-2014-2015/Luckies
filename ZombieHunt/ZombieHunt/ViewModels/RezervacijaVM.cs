using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieHunt.Models;
using ZombieHunt.ViewModels.Commands;
using ZombieHunt.Views;

namespace ZombieHunt.ViewModels
{
    public class RezervacijaVM: INotifyPropertyChanged
    {
        #region ICommand pokazivači
        public NastaviRezervacijuCommand nastaviRezervacijuCommand { get; set; }
        public DodajKlijentaCommand dodajClanaCommand { get; set; }
        public RezervisiOruzjeCommand rezervisiOruzjeCommand { get; set; }
        public RezervisiOpremuCommand rezervisiOpremuCommand { get; set; }
        public Proba proba { get; set; }
        #endregion

        private ObservableCollection<Klijent> klijentiOC;
        public ObservableCollection<Klijent> KlijentiOC
        {
            get { return klijentiOC; }
            set
            {
                klijentiOC = value;
                RaisePropertyChanged("KlijentiOC");
            }
        }

        private ObservableCollection<Oprema> _opremaOC;
        public ObservableCollection<Oprema> opremaOC
        {
            get { return _opremaOC; }
            set
            {
                _opremaOC = value;
                RaisePropertyChanged("opremaOC");
            }
        }

        public RezervacijaVM()
        {
            proba = new Proba(this);
            rezervisiOruzjeCommand = new RezervisiOruzjeCommand(this);
            rezervisiOpremuCommand = new RezervisiOpremuCommand(this);
            dodajClanaCommand = new DodajKlijentaCommand(this);
            nastaviRezervacijuCommand = new NastaviRezervacijuCommand(this);
            KlijentiOC = new ObservableCollection<Klijent>();
        }

        public void DodajKlijenta()
        {
            KlijentiOC.Add(new Klijent("Mirela", "Peskovic", "LICNAID"));
        }

        public void NastaviRezervaciju()
        {
            Rezervacija_pt2 rez2 = new Rezervacija_pt2();
            rez2.ShowDialog();
        }

        public void UcitajOruzje()
        {
            OpremaKolekcija opremaKol = new OpremaKolekcija();
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
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}

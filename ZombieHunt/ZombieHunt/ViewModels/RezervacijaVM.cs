using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZombieHunt.Models;
using ZombieHunt.ViewModels.Commands;
using ZombieHunt.Views;

namespace ZombieHunt.ViewModels
{
    public class RezervacijaVM: INotifyPropertyChanged
    {

        #region ICommand pokazivači
        public NastaviRezervacijuCommand nastaviRezervacijuCommand { get; set; }
        public DodajKlijentaCommand dodajKlijentaCommand { get; set; }
        public UkloniKlijentaCommand ukloniKlijentaCommand { get; set; }
        public RezervisiOruzjeCommand rezervisiOruzjeCommand { get; set; }
        public RezervisiOsobljeCommand rezervisiOsobljeCommand { get; set; }
        public RezervisiHranuCommand rezervisiHranuCommand { get; set; }
        public RezervisiVozilaCommand rezervisiVozilaCommand { get; set; }
        public RezervisiOpremuCommand rezervisiOpremuCommand { get; set; }
        #endregion


        #region Bind Getters and Setters
        private List<Osoblje> listaOsoblja;

        private Osoblje osoba;
        public Osoblje Osoba
        {
            get { return osoba; }
            set
            {
                if(osobljeCancel == null)
                {
                    osoba = value;
                    if (osoba != null && UnajmljenoOsobljeOC.Count + KlijentiOC.Count < 8)
                    {
                        UnajmljenoOsobljeOC.Add(osoba);
                        if (osoba.Spec == "Hired Gun") hiredGunOC.Remove(osoba);
                        else if (osoba.Spec == "Medic") medicOC.Remove(osoba);
                        else if (osoba.Spec == "Driver") driverOC.Remove(osoba);
                        else if (osoba.Spec == "Mechanic") mechanicOC.Remove(osoba);
                    }
                }
                
            }
        }

        private Osoblje osobljeCancel;
        public Osoblje OsobljeCancel
        {
            get { return osobljeCancel; }
            set
            {
                osobljeCancel = value;
                if(osobljeCancel != null)
                {
                    if (osobljeCancel.Spec == "Hired Gun") hiredGunOC.Add(osobljeCancel);
                    else if (osobljeCancel.Spec == "Medic") medicOC.Add(osobljeCancel);
                    else if (osobljeCancel.Spec == "Doctor") driverOC.Add(osobljeCancel);
                    else if (osobljeCancel.Spec == "Mechanic") mechanicOC.Add(osobljeCancel);
                    UnajmljenoOsobljeOC.Remove(osobljeCancel);
                }
            }
        }

        public Klijent KlijentCancel { get; set; }

        private Oprema oprema;
        public Oprema Oprema
        {
            get { return oprema; }
            set
            {
                oprema = value;
                if (oprema != null) IznajmljenaOpremaOC.Add(oprema);
                oprema = null;
            }
        }

        private ObservableCollection<Osoblje> _hiredGunOC;
        public ObservableCollection<Osoblje> hiredGunOC
        {
            get { return _hiredGunOC; }
            set
            {
                _hiredGunOC = value;
                RaisePropertyChanged("hiredGunOC");
            }
        }

        private ObservableCollection<Osoblje> _medicOC;
        public ObservableCollection<Osoblje> medicOC
        {
            get { return _medicOC; }
            set
            {
                _medicOC = value;
                RaisePropertyChanged("medicOC");
            }
        }

        private ObservableCollection<Osoblje> _driverOC;
        public ObservableCollection<Osoblje> driverOC
        {
            get { return _driverOC; }
            set
            {
                _driverOC = value;
                RaisePropertyChanged("driverOC");
            }
        }

        private ObservableCollection<Osoblje> _mechanicOC;
        public ObservableCollection<Osoblje> mechanicOC
        {
            get { return _mechanicOC; }
            set
            {
                _mechanicOC = value;
                RaisePropertyChanged("mechanicOC");
            }
        }


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

        private ObservableCollection<Osoblje> unajmljenoOsobljeOC;
        public ObservableCollection<Osoblje> UnajmljenoOsobljeOC
        {
            get { return unajmljenoOsobljeOC; }
            set
            {
                unajmljenoOsobljeOC = value;
                RaisePropertyChanged("UnajmljenoOsobljeOC");
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

        private ObservableCollection<Oprema> _iznajmljenaOpremaOC;
        public ObservableCollection<Oprema> IznajmljenaOpremaOC
        {
            get { return _iznajmljenaOpremaOC; }
            set
            {
                _iznajmljenaOpremaOC = value;
                RaisePropertyChanged("IznajmljenaOpremaOC");
            }
        }

        #endregion


        public RezervacijaVM()
        {
            rezervisiOruzjeCommand = new RezervisiOruzjeCommand(this);
            rezervisiOpremuCommand = new RezervisiOpremuCommand(this);
            rezervisiHranuCommand = new RezervisiHranuCommand(this);
            rezervisiVozilaCommand = new RezervisiVozilaCommand(this);
            dodajKlijentaCommand = new DodajKlijentaCommand(this);
            ukloniKlijentaCommand = new UkloniKlijentaCommand(this);
            rezervisiOsobljeCommand = new RezervisiOsobljeCommand(this);
            nastaviRezervacijuCommand = new NastaviRezervacijuCommand(this);
            KlijentiOC = new ObservableCollection<Klijent>();
            UnajmljenoOsobljeOC = new ObservableCollection<Osoblje>();
            IznajmljenaOpremaOC = new ObservableCollection<Oprema>();
            UcitajOsoblje();
        }


        public void RezervisiOsoblje()
        {
            RezervacijaOsobljaForma rof = new RezervacijaOsobljaForma();
            rof.ShowDialog();
        }

        public void DodajKlijenta()
        {
            if (KlijentiOC.Count + UnajmljenoOsobljeOC.Count < 8) KlijentiOC.Add(new Klijent("FirstName_Here", "LastName_Here", "ID_Here (12ABC1234)"));
        }

        public void UkloniKlijenta()
        {
            if (KlijentCancel != null) KlijentiOC.Remove(KlijentCancel);
        }

        public void UcitajOsoblje()
        {
            hiredGunOC = new ObservableCollection<Osoblje>();
            medicOC = new ObservableCollection<Osoblje>();
            driverOC = new ObservableCollection<Osoblje>();
            mechanicOC = new ObservableCollection<Osoblje>();

            OsobljeKolekcija ok = new OsobljeKolekcija();
            listaOsoblja = ok.UcitajOsoblje();

            foreach (Osoblje osoba in listaOsoblja)
            {
                if (osoba.Spec == "Hired Gun") hiredGunOC.Add(osoba);
                else if (osoba.Spec == "Medic") medicOC.Add(osoba);
                else if (osoba.Spec == "Driver") driverOC.Add(osoba);
                else if (osoba.Spec == "Mechanic") mechanicOC.Add(osoba);
            }
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

        public void OpremaUListu()
        {
            if (Oprema != null) IznajmljenaOpremaOC.Add(Oprema);
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

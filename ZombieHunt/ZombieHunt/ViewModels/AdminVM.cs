using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZombieHunt.Models;
using ZombieHunt.ViewModels.Commands;

namespace ZombieHunt.ViewModels
{
    public class AdminVM: INotifyPropertyChanged
    {

        #region Binding Getters and Setters
        
        public string PolazakTemp { get; set; }
        public string DolazakTemp { get; set; }
        public string ImeTemp { get; set; }
        public string PrezimeTemp { get; set; }

        private string naziv_osoba;
        public string Naziv_osoba
        {
            get { return naziv_osoba; }
            set
            {
                naziv_osoba = value;
                RaisePropertyChanged("Naziv_osoba");
            }
        }

        private string cijena_osoba;
        public string Cijena_osoba
        {
            get { return cijena_osoba; }
            set
            {
                cijena_osoba = value;
                RaisePropertyChanged("Cijena_osoba");
            }
        }

        private string tip_osoba;
        public string Tip_osoba
        {
            get { return tip_osoba; }
            set
            {
                tip_osoba = value;
                RaisePropertyChanged("Tip_osoba");
            }
        }

        private string picturePath_osoba;
        public string PicturePath_osoba
        {
            get {return picturePath_osoba;}
            set
            {
                picturePath_osoba = value;
                RaisePropertyChanged("PicturePath_osoba");
            }
        }

        private string naziv_oprema;
        public string Naziv_oprema
        {
            get { return naziv_oprema; }
            set
            {
                naziv_oprema = value;
                RaisePropertyChanged("Naziv_oprema");
            }
        }

        private string cijena_oprema;
        public string Cijena_oprema
        {
            get { return cijena_oprema; }
            set
            {
                cijena_oprema = value;
                RaisePropertyChanged("Cijena_oprema");
            }
        }

        private int kolicina_oprema;
        public int Kolicina_oprema
        {
            get { return kolicina_oprema; }
            set
            {
                kolicina_oprema = value;
                RaisePropertyChanged("Kolicina_oprema");
            }
        }

        private string kategorija_oprema;
        public string Kategorija_oprema
        {
            get { return kategorija_oprema; }
            set
            {
                kategorija_oprema = value;
                RaisePropertyChanged("Kategorija_oprema");
            }
        }

        private string picturePath_oprema;
        public string PicturePath_oprema
        {
            get { return picturePath_oprema; }
            set
            {
                picturePath_oprema = value;
                RaisePropertyChanged("PicturePath_oprema");
            }
        }

        #endregion




        #region ICommand pokazivači
        public PolazakPretragaCommand polazakPretragaCommand { get; set; }
        public DolazakPretragaCommand dolazakPretragaCommand { get; set; }
        public ImePretragaCommand imePretragaCommand { get; set; }
        public PrezimePretragaCommand prezimePretragaCommand { get; set; }

        public OFDCommand ofdCommand { get; set; }
        public UnesiOsobljeCommand unesiOsobljeCommand { get; set; }
        public UnesiOpremuCommand unesiOpremuCommand { get; set; }
        #endregion

        private ObservableCollection<Pretraga> pretragaOC;

        public ObservableCollection<Pretraga> PretragaOC
        {
            get { return pretragaOC; }
            set
            {
                pretragaOC = value;
                RaisePropertyChanged("PretragaOC");
            }
        }

        public List<string> kategorije { get; set; }
        public List<string> tipopreme { get; set; }

        public AdminVM()
        {
            polazakPretragaCommand = new PolazakPretragaCommand(this);
            dolazakPretragaCommand = new DolazakPretragaCommand(this);
            imePretragaCommand = new ImePretragaCommand(this);
            prezimePretragaCommand = new PrezimePretragaCommand(this);
            ofdCommand = new OFDCommand(this);
            unesiOsobljeCommand = new UnesiOsobljeCommand(this);
            unesiOpremuCommand = new UnesiOpremuCommand(this);
            kategorije = new List<string>() { "Hired Gun", "Medic", "Driver", "Mechanic" };
            tipopreme = new List<string>() {"Oružje", "Oprema", "Hrana", "Vozila"};
            UcitajBazu();
        }


        #region Pretraga i filteri
        public void UcitajBazu()
        {
            PretragaKolekcija pretragaKol = new PretragaKolekcija();

            PretragaOC = new ObservableCollection<Pretraga>(pretragaKol.UcitajBazu());
        }

        public void FiltrirajPoPolasku(string polazak)
        {
            PretragaKolekcija pretragaKol = new PretragaKolekcija();

            PretragaOC = new ObservableCollection<Pretraga>(pretragaKol.FiltrirajPoPolasku(polazak, DolazakTemp, ImeTemp, PrezimeTemp));
        }

        public void FiltrirajPoDolasku(string dolazak)
        {
            PretragaKolekcija pretragaKol = new PretragaKolekcija();

            PretragaOC = new ObservableCollection<Pretraga>(pretragaKol.FiltrirajPoDolasku(PolazakTemp, dolazak, ImeTemp, PrezimeTemp));
        }

        public void FiltrirajPoImenu(string ime)
        {
            PretragaKolekcija pretragaKol = new PretragaKolekcija();

            PretragaOC = new ObservableCollection<Pretraga>(pretragaKol.FiltrirajPoImenu(PolazakTemp, DolazakTemp, ime, PrezimeTemp));
        }

        public void FiltrirajPoPrezimenu(string prezime)
        {
            PretragaKolekcija pretragaKol = new PretragaKolekcija();

            PretragaOC = new ObservableCollection<Pretraga>(pretragaKol.FiltrirajPoPrezimenu(PolazakTemp, DolazakTemp, ImeTemp, prezime));
        }
        #endregion

        public void UnesiOsoblje()
        {
            
            OsobljeKolekcija ok = new OsobljeKolekcija();
            ok.DodajOsoblje(Naziv_osoba, Convert.ToDouble(Cijena_osoba), Tip_osoba, PicturePath_osoba);
            Naziv_osoba = String.Empty;
            Cijena_osoba = String.Empty;
            PicturePath_osoba = String.Empty;
        }

        public void UnesiOpremu()
        {
            OpremaKolekcija ok = new OpremaKolekcija();
            ok.DodajOpremu(Naziv_oprema, Convert.ToDouble(Cijena_oprema), Convert.ToInt32(Kolicina_oprema), Kategorija_oprema, PicturePath_oprema);
            Naziv_oprema = String.Empty;
            Cijena_oprema = String.Empty;
            Kolicina_oprema = 0;
            Kategorija_oprema = String.Empty;
            PicturePath_oprema = String.Empty;

        }

        public void OtvoriFileDialog(string parametar)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files | *.png; *.jpg";
            if(ofd.ShowDialog()==true)
            {
                if (parametar == "osoba") PicturePath_osoba = Path.GetFullPath(ofd.FileName);
                else if (parametar == "oprema") PicturePath_oprema = Path.GetFullPath(ofd.FileName);
            }
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

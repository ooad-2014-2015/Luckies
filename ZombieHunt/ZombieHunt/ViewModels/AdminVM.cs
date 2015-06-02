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

namespace ZombieHunt.ViewModels
{
    public class AdminVM: INotifyPropertyChanged
    {
        public string ImeTemp { get; set; }
        public string PrezimeTemp { get; set; }

        public ImePretragaCommand imePretragaCommand { get; set; }
        public PrezimePretragaCommand prezimePretragaCommand { get; set; }

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

        public UnesiNovoOsobljeCommand unesiNovoOsobljeCommand { get; set; }

        public AdminVM()
        {
            imePretragaCommand = new ImePretragaCommand(this);
            prezimePretragaCommand = new PrezimePretragaCommand(this);
            kategorije = new List<string>() { "Hired Gun", "Medic", "Driver", "Mechanic" };
            unesiNovoOsobljeCommand = new UnesiNovoOsobljeCommand(this);
            UcitajBazu();
        }

        public void UcitajBazu()
        {
            PretragaKolekcija pretragaKol = new PretragaKolekcija();

            PretragaOC = new ObservableCollection<Pretraga>(pretragaKol.UcitajBazu());
        }

        public void FiltrirajPoImenu(string ime)
        {
            PretragaKolekcija pretragaKol = new PretragaKolekcija();

            PretragaOC = new ObservableCollection<Pretraga>(pretragaKol.FiltrirajPoImenu(ime, PrezimeTemp));
        }

        public void FiltrirajPoPrezimenu(string prezime)
        {
            PretragaKolekcija pretragaKol = new PretragaKolekcija();

            PretragaOC = new ObservableCollection<Pretraga>(pretragaKol.FiltrirajPoPrezimenu(ImeTemp, prezime));
        }

        public void UnesiNovoOsoblje()
        {
            
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

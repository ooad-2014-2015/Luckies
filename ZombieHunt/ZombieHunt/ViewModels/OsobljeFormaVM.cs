using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieHunt.Models;
using ZombieHunt.ViewModels.Commands;

namespace ZombieHunt.ViewModels
{
    public class OsobljeFormaVM: INotifyPropertyChanged
    {
        #region ICommand pokazivači
        public UcitajOsobljeCommand ucitajOsobljeCommand { get; set; }
        #endregion

        private List<Osoblje> listaOsoblja;

        private ObservableCollection<Osoblje> _hiredGunOC;
        private ObservableCollection<Osoblje> _medicOC;
        private ObservableCollection<Osoblje> _driverOC;
        private ObservableCollection<Osoblje> _mechanicOC;

        public OsobljeFormaVM()
        {
            ucitajOsobljeCommand = new UcitajOsobljeCommand(this);
        }

        public ObservableCollection<Osoblje> hiredGunOC
        {
            get { return _hiredGunOC; }
            set
            {
                _hiredGunOC = value;
                RaisePropertyChanged("hiredGunOC");
            }
        }

        public ObservableCollection<Osoblje> medicOC
        {
            get { return _medicOC; }
            set
            {
                _medicOC = value;
                RaisePropertyChanged("medicOC");
            }
        }

        public ObservableCollection<Osoblje> driverOC
        {
            get { return _driverOC; }
            set
            {
                _driverOC = value;
                RaisePropertyChanged("driverOC");
            }
        }

        public ObservableCollection<Osoblje> mechanicOC
        {
            get { return _mechanicOC; }
            set
            {
                _mechanicOC = value;
                RaisePropertyChanged("mechanicOC");
            }
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

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

        private void UcitajHelp()
        {
            HelpLogin = "Ovaj dio naseg sistema se odnosi na Vas samo ukoliko ste uposlenik agencije.\n" +
                             "Ovom dijelu se pristupa sa username-om i passwordom admin admin dok agencija ne odobri ostatak sistema.\n" +
                             "Nas projekat ne obuhvata rad na sigurnim bazama podataka.";

            HelpONama = "U Rubrici 'O nama' mozete vidjeti kratku historiju nase agencije,\n " +
                           "kao i mapu otoka Saccubos i jos neke zanimljivosti.";

            HelpKomentari = "U desnom dijelu glavnog menija mozete vidjeti neke komentare dosadasnjih klijenta. \n"+
                               "Ukoliko zelite ostaviti svoj komentar, morat cete instalirati nasu Windows Phone aplikaciju.";

            HelpPonuda = "U rubrikama 'Pregled...' mozete pogledati ono sto agencija trenutno nudi od dodatne opreme.";

            HelpRezervacija = "Ukoliko zelite rezervisati svoj lov na zombie-e ovo je rubrika za vas!\n " +
                                "Lider tima unosi podatke o svom timu. Tim moze imati max 8 clanova - zajedno sa eventualnim odabranim osobljem. \n" +
                                "Ukoliko se odlucite da odmah platite vasu najbolju odluku u zivotu, odmah cete dobiti svoj ugovor. \n" +
                                "Na dan polaska nemojte zaboraviti svoje ugovore i racune! Ukoliko se odlucite da ipak platite u nasoj agenciji, cekamo vas.\n" +
                                "U tom slucaju nemojte oklijevati, jer necemo dozivotno cekati na Vas!";

            ONama =     "'Dosao si ovdje\n"+
                        "Gdje je najnezahvalnije bilo doci\n " +
                        "Ovdje gdje je najlude bilo stici\n " +
                        "Ovdje gdje je ipak najjunackije bilo nici\n " +
                        "Jer ovdje se ne zivi samo da bi se umrlo\n " +
                        "Ovdje se i umire\n " +
                        "Da bi se zivjelo.'\n              " +
                         "                                   MAK DIZDAR\n\n\n " +


                        "Zombie Hunt je agencija koja omogucava ljubiteljima adrenalina da se na jedan potpuno novi nacin oslobode stresa i pokazu svoju snagu i snalazljivost u borbi za zivot.  "+
                        "Lov na Zombije predstavlja novi vid  sporta namijenjen prvenstveno za ljude koji na najbolji nacin zele da iskuse vrijednost zivota i ljepotu prirode. Zombiji su  "+
                        "agresivna i neprijateljski nastrojena bezumna bica koja bi neobuzdana predstavljala opasnost po covjecanstvo. Opasnost proizlazi iz njihove sposobnosti da zaraze  "+
                        "zive organizme putem fizickog kontakta. No, zbog njihovog nedostatka inteligencije, nisu sposobni ni za kakav oblik organizacije te zbog toga za ucesnike u nasem  "+
                        "programu ne predstavljaju nista drugo do pokretne mete i izvor neogranicene zabave.\n " +
                        "Agencija je osnovana 2007. god u Londonu, Ujedinjeno Kraljevstvo, s ciljem da se stvori jedan novi vid zabave za ljubitelje adrenalina. Osnovana je pod nazivom Zombies, "+
                        "dok je nakon godinu dana preimenovana u ZombieHunt. Na pocetku poslovanja, agencija je imala samo 15 uposlenika, dok danas broji 25.000 uposlenika u 10.348 poslovnica "+
                        "sirom svijeta, gdje je jedina agencija koja se bavi ovakvom vrstom sporta.\n  " +
                        "Od 2007. do 2008. god lov na zombije odrzavao se u Londonu, na specijalnom terenu, pri cemu su klijenti imali na raspolaganju samo 5 razlicitih vrsta pusaka. "+
                        "Od 2008. god, lov se premjesta na nenaseljeni egzoticni otok Saccubos, u sredisnjem dijelu Indijskog okeana.  "+
                        "Takoder, agencija uvodi novi vid poslovanja sa jednom od najpouzdanijih avio kompanija, AirBus, koja je zaduzena da klijenti na otok dodju sigurno I udobno. "+
                        "Uvodi I novo oruzje I opremu, kao I strucno osoblje koje brine za sigurnost klijenata.  "+
                        "Od pocetka 2009., agencija siri svoje poslovanje na druge dijelove svijeta, i otvara novih 1.000 poslovnica u Evropi. Krajem 2010.godine, agencija je brojala 5.125 "+
                        "poslovnica rasprostranjenih na veci dio Sjeverne Amerike i Evrope.\n " +
                        "Danas broji 10.348 poslovnica diljem svijeta, i taj broj svake godine raste.\n " +
                        "Broj zadovoljnih klijenata se, do 2009. godine udvostrucavao svake godine, dok danas se taj broj godisnje ucetverostrucava.\n " +
                        "Danas, agencija broji I do 2.000 stalnih klijenata, klijenata koji su korisnici ZombieCard kartice, dok je u lov do sada otislo preko 98.000 ljudi!";
        }

        public string ONama { get; set; }
        public string HelpLogin { get; set; }
        public string HelpONama { get; set;}
        public string HelpKomentari { get; set; }
        public string HelpPonuda { get; set; }
        public string HelpRezervacija { get; set; }
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
        public PrikaziHelpCommand prikaziHelpCommand { get; set; }
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
            UcitajHelp();
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
            prikaziHelpCommand = new PrikaziHelpCommand(this);
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

        public void PrikaziHelp()
        {
            Help help = new Help();
            help.ShowDialog();
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

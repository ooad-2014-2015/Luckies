using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ZombieHunt.Models;
using ZombieHunt.ViewModels.Commands;
using ZombieHunt.Views;
using TallComponents.PDF.Rasterizer;
using TallComponents.PDF.Rasterizer.Configuration;

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
        public PrikaziPlacanjeCommand prikaziPlacanjeCommand { get; set; }
        public RacunajPopustCommand racunajPopustCommand { get; set; }
        public IzvrsiUplatuCommand izvrsiUplatuCommand { get; set; }
        public PrintajUgovoreCommand printajUgovoreCommand { get; set; }
        public FinalizirajRezervacijuCommand finalizirajRezervacijuCommand { get; set; }
        #endregion


        #region Bind Getters and Setters

        private float ukupnaCijena;
        public float UkupnaCijena
        {
            get { return ukupnaCijena; }
            set
            {
                ukupnaCijena = value;
                Ukupno = Convert.ToString(ukupnaCijena);
            }
        }

        private string ukupno;
        public string Ukupno
        {
            get { return ukupno; }
            set
            {
                ukupno = "Total: " + value + "$";
                RaisePropertyChanged("Ukupno");
            }

        }

        private float popustCijena;
        public float PopustCijena
        {
            get { return popustCijena; }
            set
            {
                popustCijena = value;
                Popust = Convert.ToString(popustCijena);
            }
        }

        private string popust;
        public string Popust
        {
            get { return popust; }
            set
            {
                popust = "Total: " + Math.Round((decimal)popustCijena, 2) + "$";
                RaisePropertyChanged("Popust");
            }
        }

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
                    if (osoba != null && UnajmljenoOsobljeOC.Count + KlijentiOC.Count < 8 && UnajmljenoOsobljeOC.Count<4)
                    {
                        UkupnaCijena += (float)osoba.Cijena;
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
                    UkupnaCijena -= (float)osobljeCancel.Cijena;
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
                if(oprema!=null)
                {
                    IznajmljenaOpremaOC.Add(oprema);
                    UkupnaCijena += (float)oprema.Cijena;
                    oprema = null;
                }
                
            }
        }

        private int opremaCancel;
        public int OpremaCancel
        {
            get { return opremaCancel; }
            set
            {
                opremaCancel = value;
                if (opremaCancel >= 0)
                {
                    UkupnaCijena -= (float)IznajmljenaOpremaOC[opremaCancel].Cijena;
                    IznajmljenaOpremaOC.RemoveAt(opremaCancel);
                    opremaCancel = -1;
                }
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
            ukupnaCijena = 0;
            PopustCijena = 0;
            OpremaCancel = -1;
            Ukupno = Convert.ToString(UkupnaCijena);
            rezervisiOruzjeCommand = new RezervisiOruzjeCommand(this);
            rezervisiOpremuCommand = new RezervisiOpremuCommand(this);
            rezervisiHranuCommand = new RezervisiHranuCommand(this);
            rezervisiVozilaCommand = new RezervisiVozilaCommand(this);
            dodajKlijentaCommand = new DodajKlijentaCommand(this);
            ukloniKlijentaCommand = new UkloniKlijentaCommand(this);
            rezervisiOsobljeCommand = new RezervisiOsobljeCommand(this);
            nastaviRezervacijuCommand = new NastaviRezervacijuCommand(this);
            prikaziPlacanjeCommand = new PrikaziPlacanjeCommand(this);
            racunajPopustCommand = new RacunajPopustCommand(this);
            izvrsiUplatuCommand = new IzvrsiUplatuCommand(this);
            printajUgovoreCommand = new PrintajUgovoreCommand(this);
            finalizirajRezervacijuCommand = new FinalizirajRezervacijuCommand(this);
            KlijentiOC = new ObservableCollection<Klijent>();
            UnajmljenoOsobljeOC = new ObservableCollection<Osoblje>();
            IznajmljenaOpremaOC = new ObservableCollection<Oprema>();
            UcitajOsoblje();
        }


        #region Rezervacija prvi dio
        public void RezervisiOsoblje()
        {
            RezervacijaOsobljaForma rof = new RezervacijaOsobljaForma();
            rof.ShowDialog();
        }

        public void DodajKlijenta()
        {
            if (KlijentiOC.Count + UnajmljenoOsobljeOC.Count < 8)
            {
                KlijentiOC.Add(new Klijent("FirstName_Here", "LastName_Here", "ID_Here (12ABC1234)"));
                UkupnaCijena += 800;
            }
        }

        public void UkloniKlijenta()
        {
            if (KlijentCancel != null)
            {
                KlijentiOC.Remove(KlijentCancel);
                UkupnaCijena -= 800;
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
        #endregion


        #region Rezervacija drugi dio
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

        public void OtkaziOpremu()
        {
            IznajmljenaOpremaOC.RemoveAt(OpremaCancel);
        }
        #endregion


        #region Rezervacija treci dio

        public void PrikaziPlacanje()
        {
            if (UkupnaCijena > 10000) PopustCijena = UkupnaCijena * (float)0.9;
            else PopustCijena = UkupnaCijena;
            Rezervacija_pt3 rez3 = new Rezervacija_pt3();
            rez3.ShowDialog();
        }

        

        public void RacunajPopust(string parameter)
        {
            if (parameter=="Cener" && UkupnaCijena > 10000 || parameter == "Cache") PopustCijena = UkupnaCijena * (float)0.9;
            else if (parameter == "ZomCard" ) PopustCijena = UkupnaCijena * (float)0.7;
            else PopustCijena = UkupnaCijena;
        }

        public void IzvrsiUplatu(string creditCardNumber)
        {
            //poziv sistema za naplacivanje
        }

        public void PrintajUgovore()
        {

            foreach (Klijent klijent in KlijentiOC)
            {
                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "Individualni ugovor u sudjelovanju";
                PdfPage pdfPage = pdf.AddPage();
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

                graph.DrawString("ZombieHunt Corporation", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
                graph.DrawString("Klijent: " + klijent.Ime + " " + klijent.Prezime, font, XBrushes.Black, new XRect(0, 140, pdfPage.Width.Point, 160), XStringFormats.Center);
                graph.DrawString("Identifikacija: " + klijent.LicnaID, font, XBrushes.Black, new XRect(0, 160, pdfPage.Width.Point, 180), XStringFormats.Center);
                graph.DrawString("Datum: " + DateTime.Now, font, XBrushes.Black, new XRect(0, 180, pdfPage.Width.Point, 200), XStringFormats.Center);
                graph.DrawString("Sretno!", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.BottomCenter);
                
                string pdfFilename = klijent.Ime + klijent.Prezime + klijent.LicnaID + ".pdf";
                pdf.Save(pdfFilename);
            }

            PdfDocument pdf1 = new PdfDocument();
            pdf1.Info.Title = "Grupni ugovor u sudjelovanju";
            PdfPage pdfPage1 = pdf1.AddPage();
            XGraphics graph1 = XGraphics.FromPdfPage(pdfPage1);
            XFont font1 = new XFont("Verdana", 14, XFontStyle.Bold);

            graph1.DrawString("ZombieHunt Corporation", font1, XBrushes.Black, new XRect(0, 0, pdfPage1.Width.Point, pdfPage1.Height.Point), XStringFormats.TopCenter);

            string datum = Convert.ToString(DateTime.Now.Date);
            int y = 80;
            foreach (Klijent k in KlijentiOC)
            {
                graph1.DrawString("Klijent: " + k.Ime + " " + k.Prezime +" "+k.LicnaID, font1, XBrushes.Black, new XRect(0, y, pdfPage1.Width.Point, y+16), XStringFormats.Center);
                y += 16;
            }
            graph1.DrawString(Popust, font1, XBrushes.Black, new XRect(0, y, pdfPage1.Width.Point, y + 16), XStringFormats.Center); y += 16;
            graph1.DrawString("Datum: " + datum, font1, XBrushes.Black, new XRect(0, y, pdfPage1.Width.Point, y + 16), XStringFormats.Center); y += 16;

            graph1.DrawString("Sretno!", font1, XBrushes.Black, new XRect(0, 0, pdfPage1.Width.Point, pdfPage1.Height.Point), XStringFormats.BottomCenter);

            string pdfFilename1 = "GrupniUgovor.pdf";
            pdf1.Save(pdfFilename1);
            Process.Start(pdfFilename1);

            GoPrintDialog();
        }

        private void GoPrintDialog()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".pdf",
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
            };
            var fileOpenResult = openFileDialog.ShowDialog();
            if (fileOpenResult != true)
            {
                return;
            }


            var printDialog = new PrintDialog();
            printDialog.PageRangeSelection = PageRangeSelection.AllPages;
            printDialog.UserPageRangeEnabled = true;
            var doPrint = printDialog.ShowDialog();
            if (doPrint != true)
            {
                return;
            }

            FixedDocument fixedDocument;
            using (var pdfFile = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
            {
                var document = new Document(pdfFile);
                var renderSettings = new RenderSettings();
                var renderOptions = new ConvertToWpfOptions { ConvertToImages = false };
                renderSettings.RenderPurpose = RenderPurpose.Print;
                renderSettings.ColorSettings.TransformationMode = ColorTransformationMode.HighQuality;
                fixedDocument = document.ConvertToWpf(renderSettings, renderOptions);
            }
            printDialog.PrintDocument(fixedDocument.DocumentPaginator, "Print");
        }


        public void Finalizacija()
        {
            ukupnaCijena = 0;
            PopustCijena = 0;
            OpremaCancel = -1;
            KlijentiOC.Clear();
            unajmljenoOsobljeOC.Clear();
            IznajmljenaOpremaOC.Clear();
        }
        

        #endregion

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

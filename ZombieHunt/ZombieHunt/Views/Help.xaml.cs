using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZombieHunt.Views
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
           InitializeComponent();
           helpLogin.Text = "Ovaj dio naseg sistema se odnosi na Vas samo ukoliko ste uposlenik agencije.\n" + 
                             "Ovom dijelu se pristupa sa username-om i passwordom admin admin dok agencija ne odobri ostatak sistema.\n" +
                             "Nas projekat ne obuhvata rad na sigurnim bazama podataka.";

           helpOnama.Text="U Rubrici 'O nama' mozete vidjeti kratku historiju nase agencije,\n "+
                           "kao i mapu otoka Saccubos i jos neke zanimljivosti.";
          
           helpKomentari.Text="U desnom dijelu glavnog menija mozete vidjeti neke komentare dosadasnjih klijenta. \n"+
                               "Ukoliko zelite ostaviti svoj komentar, morat cete instalirati nasu Windows Phone aplikaciju.";

           helpPonuda.Text="U rubrikama 'Pregled...' mozete pogledati ono sto agencija trenutno nudi od dodatne opreme.";

           helpRezervacija.Text = "Ukoliko zelite rezervisati svoj lov na zombie-e ovo je rubrika za vas!\n " +
                                "Lider tima unosi podatke o svom timu. Tim moze imati max 8 clanova - zajedno sa eventualnim odabranim osobljem. \n" +
                                "Ukoliko se odlucite da odmah platite vasu najbolju odluku u zivotu, odmah cete dobiti svoj ugovor. \n" +
                                "Na dan polaska nemojte zaboraviti svoje ugovore i racune! Ukoliko se odlucite da ipak platite u nasoj agenciji, cekamo vas.\n" +
                                "U tom slucaju nemojte oklijevati, jer necemo dozivotno cekati na Vas!";

      }
    }
}

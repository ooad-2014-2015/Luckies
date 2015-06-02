using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ZombieHuntWPhone
{
    public partial class Page2 : PhoneApplicationPage
    {
        public Page2()
        {
            InitializeComponent();
            using (ZombieHuntWindowsPhoneContext sdf = new ZombieHuntWindowsPhoneContext(ZombieHuntWindowsPhoneContext.ConnectionString))
            {
                sdf.CreateIfNotExists();
                try
                {
                        Table<Komentari> komentari = sdf.GetTable<Komentari>();
                        Table<Slike> slike = sdf.GetTable<Slike>();

                        var komentariQuery = from k in komentari.ToList() select k;
                        var slikeQuery = from s in slike.ToList() select s;

                        List<BitmapImage> slikice = new List<BitmapImage>();
                        foreach (var slika in slikeQuery)
                        {
                                if (slika.Slika.ToArray() != null && slika.Slika.ToArray() is Byte[])
                                {
                                    MemoryStream stream = new MemoryStream(slika.Slika.ToArray());
                                    BitmapImage image = new BitmapImage();
                                    image.SetSource(stream);
                                    slikice.Add(image);
                                } 
                        }
                        

                       
                        foreach (var komentar in komentariQuery)
                        {

                            PivotItem p = new PivotItem();
                            MyUserControl kontrola = new MyUserControl();
                            kontrola.komentarUser.Text = komentar.Komentar;
                            kontrola.datumUser.Text = komentar.Datum.ToString();

                            Random rnd = new Random();
                            int broj_slike = rnd.Next(1,6)-1;

                            kontrola.randomSlika.Source = slikice[broj_slike];

                            
                            p.Header = "Komentar " + komentar.Id;
                            p.Content = kontrola;
                     
                            pivot.Items.Add(p);
                        }
                
                    
                }
                catch (Exception et)
                {

                }
            }

        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void pivot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }

}

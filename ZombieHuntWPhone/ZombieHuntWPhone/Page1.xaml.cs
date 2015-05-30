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
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (ZombieHuntWindowsPhoneContext sdf = new ZombieHuntWindowsPhoneContext(ZombieHuntWindowsPhoneContext.ConnectionString))
            {
                sdf.CreateIfNotExists();
                
                try
                {
                  
                    Table<Komentari> stranice = sdf.GetTable<Komentari>();
                    Komentari k= new Komentari();
                    k.Komentar = noviKomentar.Text;
                    k.Datum = DateTime.Now;
                    sdf.Komentari.InsertOnSubmit(k);
                    sdf.SubmitChanges();

                }
                catch (Exception ex)
                {

                }       
                noviKomentar.Text = "Uspjesno ste dodali komentar!";

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        }
    }

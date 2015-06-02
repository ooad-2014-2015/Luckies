using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace ZombieHunt.Views
{
    /// <summary>
    /// Interaction logic for ONama.xaml
    /// </summary>
    public partial class ONama : Window
    {
        public static string tekst_za_opis;
        public ONama()
        {
            InitializeComponent();
            
           string text = File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "onama.txt"));
           opciOpis.Text = text;
          /* using(StreamReader citaj = new StreamReader("onama.txt"))
            {
                tekst_za_opis = citaj.ReadToEnd();
                opciOpis.Text = tekst_za_opis;
            }*/
            
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ZombieHunt.Models
{
   public class Osoblje
    {
        private int id;
        private string naziv;
        private double cijena;
        private string specijalizacija;
        private BitmapImage bitmap;

       public Osoblje(int id, string naziv, double cijena, string specijalizacija, BitmapImage bitmap)
       {
           
           this.id = id;
           this.naziv = naziv;
           this.cijena = cijena;
           this.specijalizacija = specijalizacija;
           this.bitmap = bitmap;
       }

       public int ID
       {
           get { return id; }
           set { id = value; }
       }

       public string Naziv
       {
           get { return naziv; }
           set { naziv = value; }
       }

       public double Cijena
       {
           get { return cijena; }
           set { cijena = value; }
       }

       public string Spec
       {
           get { return specijalizacija; }
           set { specijalizacija = value; }
       }

       public BitmapImage Bitmap
       {
           get { return bitmap; }
           set { bitmap = value; }
       }

       public string CijenaS
       {
           get { return cijena+"$"; }
       }

    }
}

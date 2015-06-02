using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ZombieHunt.Models
{
    public class Oprema
    {
        int id;
        string naziv;
        double cijena;
        BitmapImage bitmap;

        public Oprema(int _id, string _naziv, double _cijena, BitmapImage _bitmap)
        {
            id = _id;
            naziv = _naziv;
            cijena = _cijena;
            bitmap = _bitmap;
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

        public string CijenaS
        {
            get { return cijena + "$"; }
        }

        public BitmapImage Bitmap
        {
            get { return bitmap; }
            set { bitmap = value; }
        }
    }
}

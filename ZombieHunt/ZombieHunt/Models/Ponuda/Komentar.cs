using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZombieHunt.Models
{
    public class Komentar
    {
        int id;
        string tekst;
        DateTime datum;

        public Komentar(int _id, string _tekst, DateTime _datum)
        {
            ID = _id;
            Tekst = _tekst;
            Datum = _datum;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Tekst
        {
            get { return tekst; }
            set { tekst = value; }
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }
    }
}

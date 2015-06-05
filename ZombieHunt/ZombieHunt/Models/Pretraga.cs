using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieHunt.Models
{
    public class Pretraga
    {
        int id;
        DateTime datumPolaska;
        DateTime datumDolaska;
        string ime;
        string prezime;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime DatumPolaska
        {
            get { return datumPolaska; }
            set { datumPolaska = value; }
        }

        public DateTime DatumDolaska
        {
            get { return datumDolaska; }
            set { datumDolaska = value; }
        }

        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }

        public Pretraga(int _id, DateTime _datumPolaska, DateTime _datumDolaska, string _ime, string _prezime)
        {
            ID = _id;
            DatumPolaska = _datumPolaska;
            DatumDolaska = _datumDolaska;
            Ime = _ime;
            Prezime = _prezime;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieHunt.Models
{
    public class Klijent
    {
        private string ime, prezime, licnaID;


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

        public string LicnaID
        {
            get { return licnaID; }
            set { licnaID = value; }
        }

        public Klijent(string ime, string prezime, string licnaID)
        {
            Ime = ime;
            Prezime = prezime;
            LicnaID = licnaID;
        }

        
    }
}

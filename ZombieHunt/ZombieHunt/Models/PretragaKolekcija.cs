using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieHunt.Models
{
    public class PretragaKolekcija
    {
        int id;
        DateTime datumPolaska;
        DateTime datumDolaska;
        string ime;
        string prezime;
       
        SqlConnection conn;
        List<Pretraga> listaPretrage;

        public PretragaKolekcija()
        {
            listaPretrage = new List<Pretraga>();
            conn = new SqlConnection();
            conn.ConnectionString = "Integrated Security=true;Initial Catalog=ZombieHuntDB; " + "Data Source=METH\\SQLEXPRESS";
        }

        public List<Pretraga> UcitajBazu()
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = "SELECT Rezervacije.[ID], [datumPolaska], [datumDolaska], [Ime], [Prezime]"+
                                      "FROM [ZombieHuntDB].[dbo].[Rezervacije], [ZombieHuntDB].[dbo].[Klijenti]"+
                                      "WHERE Rezervacije.[ID] = IdRezervacije";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    datumPolaska = reader.GetDateTime(1);
                    datumDolaska = reader.GetDateTime(2);
                    ime = reader.GetString(3);
                    prezime = reader.GetString(4);

                    listaPretrage.Add(new Pretraga(id, datumPolaska, datumDolaska, ime, prezime));
                }

                reader.Close();
            }
            catch (SqlException sqle)
            {
                Console.WriteLine("Error accesing the database: {0}", sqle.Message);
            }
            finally
            {
                conn.Close();
                //Console.WriteLine("Konekcija zatvorena!");
            }

            return listaPretrage;
        }

        public List<Pretraga> FiltrirajPoImenu(string imeParam, string prezimeParam)
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = "SELECT Rezervacije.[ID], [datumPolaska], [datumDolaska], [Ime], [Prezime]" +
                                           "FROM [ZombieHuntDB].[dbo].[Rezervacije], [ZombieHuntDB].[dbo].[Klijenti]" +
                                           "WHERE Rezervacije.[ID] = IdRezervacije AND [Ime] LIKE '%" + imeParam + "%'";

                if (prezimeParam != String.Empty) command.CommandText += " AND [Prezime] LIKE '%" + prezimeParam + "%'";
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    datumPolaska = reader.GetDateTime(1);
                    datumDolaska = reader.GetDateTime(2);
                    ime = reader.GetString(3);
                    prezime = reader.GetString(4);

                    listaPretrage.Add(new Pretraga(id, datumPolaska, datumDolaska, ime, prezime));
                }

                reader.Close();
            }
            catch (SqlException sqle)
            {
                Console.WriteLine("Error accesing the database: {0}", sqle.Message);
            }
            finally
            {
                conn.Close();
                //Console.WriteLine("Konekcija zatvorena!");
            }
            return listaPretrage;
        }

        public List<Pretraga> FiltrirajPoPrezimenu(string imeParam, string prezimeParam)
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = "SELECT Rezervacije.[ID], [datumPolaska], [datumDolaska], [Ime], [Prezime]" +
                                            "FROM [ZombieHuntDB].[dbo].[Rezervacije], [ZombieHuntDB].[dbo].[Klijenti]" +
                                            "WHERE Rezervacije.[ID] = IdRezervacije AND [Prezime] LIKE '%" + prezimeParam + "%'";

                if (imeParam != String.Empty) command.CommandText += " AND [Ime] LIKE '%" + imeParam + "%'";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    datumPolaska = reader.GetDateTime(1);
                    datumDolaska = reader.GetDateTime(2);
                    ime = reader.GetString(3);
                    prezime = reader.GetString(4);

                    listaPretrage.Add(new Pretraga(id, datumPolaska, datumDolaska, ime, prezime));
                }

                reader.Close();
            }
            catch (SqlException sqle)
            {
                Console.WriteLine("Error accesing the database: {0}", sqle.Message);
            }
            finally
            {
                conn.Close();
                //Console.WriteLine("Konekcija zatvorena!");
            }
            return listaPretrage;
        }
    }
}

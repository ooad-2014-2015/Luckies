using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZombieHunt.Models
{
    public class KomentariKolekcija
    {

        SqlConnection conn;
        List<Komentar> listaKomentara;

        int id;
        string tekst;
        DateTime datum;


        public KomentariKolekcija()
        {
            listaKomentara = new List<Komentar>();
            conn = new SqlConnection();
            conn.ConnectionString = "Integrated Security=true;Initial Catalog=ZombieHuntDB; " + "Data Source=METH\\SQLEXPRESS";
        }

        public List<Komentar> UcitajKomentare()
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = "SELECT [ID], [Tekst], [Datum] FROM [ZombieHuntDB].[dbo].[Komentari]";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    tekst = reader.GetString(1);
                    datum = reader.GetDateTime(2);

                    listaKomentara.Add(new Komentar(id, tekst, datum));
                }

                reader.Close();
            }
            catch (SqlException sqle)
            {
                MessageBox.Show("Error accesing the database: {0}", sqle.Message);
            }
            finally
            {
                conn.Close();
            }
            return listaKomentara;
        }

    }
}

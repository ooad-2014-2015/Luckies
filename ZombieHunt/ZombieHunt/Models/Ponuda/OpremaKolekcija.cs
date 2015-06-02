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
    public class OpremaKolekcija
    {
        private BitmapImage UcitajSliku(MemoryStream mstr)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = mstr;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        SqlConnection conn;
        List<Oprema> listaOpreme;

        int id;
        string naziv;
        double cijena;
        byte[] b;
        MemoryStream memStream;
        BitmapImage bitmap;

        public OpremaKolekcija()
        {
            listaOpreme = new List<Oprema>();
            conn = new SqlConnection();
            conn.ConnectionString = "Integrated Security=true;Initial Catalog=ZombieHuntDB; " + "Data Source=METH\\SQLEXPRESS";
        }

        public List<Oprema> UcitajOpremu(string s)
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                if (s == "oruzje") command.CommandText = "SELECT [ID], [Naziv], [Cijena], [Slika] FROM [ZombieHuntDB].[dbo].[Oruzje]";
                else if (s == "oprema") command.CommandText = "SELECT [ID], [Naziv], [Cijena], [Slika] FROM [ZombieHuntDB].[dbo].[Oprema]";
                else if (s == "hrana") command.CommandText = "SELECT [ID], [Naziv], [Cijena], [Slika] FROM [ZombieHuntDB].[dbo].[Hrana]";
                else if (s == "vozila") command.CommandText = "SELECT [ID], [Naziv], [Cijena], [Slika] FROM [ZombieHuntDB].[dbo].[Vozila]";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    memStream = new MemoryStream();

                    id = reader.GetInt32(0);
                    naziv = reader.GetString(1);
                    cijena = reader.GetDouble(2);
                    b = (byte[])reader.GetValue(3);
                    memStream.Write(b, 0, b.Length);
                    bitmap = UcitajSliku(memStream);

                    listaOpreme.Add(new Oprema(id, naziv, cijena, bitmap));
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
            return listaOpreme;
        }
    }
}

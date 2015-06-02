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
    public class OsobljeKolekcija
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
        List<Osoblje> listaOsoblja;

        int id;
        string naziv, specijalizacija;
        double cijena;
        byte[] b;
        MemoryStream memStream;
        BitmapImage bitmap;

        public OsobljeKolekcija()
        {
            listaOsoblja = new List<Osoblje>();
            conn = new SqlConnection();
            conn.ConnectionString = "Integrated Security=true;Initial Catalog=ZombieHuntDB; " + "Data Source=METH\\SQLEXPRESS";
        }

        public List<Osoblje> UcitajOsoblje()
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = "SELECT [ID], [Naziv], [Cijena], [Tip], [Slika] FROM [ZombieHuntDB].[dbo].[Osoblje]";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    memStream = new MemoryStream();

                    id = reader.GetInt32(0);
                    naziv = reader.GetString(1);
                    cijena = reader.GetDouble(2);
                    specijalizacija = reader.GetString(3);
                    b = (byte[])reader.GetValue(4);
                    memStream.Write(b, 0, b.Length);
                    bitmap = UcitajSliku(memStream);

                    listaOsoblja.Add(new Osoblje(id, naziv, cijena, specijalizacija, bitmap));
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
            return listaOsoblja;
        }
    }
}

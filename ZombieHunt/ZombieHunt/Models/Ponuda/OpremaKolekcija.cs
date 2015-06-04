using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                MessageBox.Show("Error accesing the database: "+ sqle.Message);
            }
            finally
            {
                conn.Close();
            }
            return listaOpreme;
        }


        public void DodajOpremu(string naziv, double cijena, int kolicina, string kategorija, string path)
        {
            string query = String.Empty;

            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);

                byte[] photo = reader.ReadBytes((int)fs.Length);
                reader.Close();
                fs.Close();

                if (kategorija == "Oružje") query = "INSERT INTO Oruzje (Naziv, Cijena, Slika, Zaliha) VALUES (@Naziv, @Cijena, @Slika, @Zaliha)";
                else if (kategorija == "Oprema") query = "INSERT INTO Oprema (Naziv, Cijena, Slika, Zaliha) VALUES (@Naziv, @Cijena, @Slika, @Zaliha)";
                else if (kategorija == "Hrana") query = "INSERT INTO Hrana (Naziv, Cijena, Slika, Zaliha) VALUES (@Naziv, @Cijena, @Slika, @Zaliha)";
                else if (kategorija == "Vozila") query = "INSERT INTO Zaliha (Naziv, Cijena, Slika, Zaliha) VALUES (@Naziv, @Cijena, @Slika, @Zaliha)";
                
         
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add("@Naziv", System.Data.SqlDbType.VarChar, 50).Value = naziv;
                command.Parameters.Add("@Cijena", System.Data.SqlDbType.Float).Value = cijena;
                command.Parameters.Add("@Slika", System.Data.SqlDbType.Image, photo.Length).Value = photo;
                command.Parameters.Add("@Zaliha", System.Data.SqlDbType.Int).Value = kolicina;

                conn.Open();
                command.ExecuteNonQuery();

            }
            catch (SqlException sqle)
            {
                MessageBox.Show("Error accesing the database: " + sqle.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

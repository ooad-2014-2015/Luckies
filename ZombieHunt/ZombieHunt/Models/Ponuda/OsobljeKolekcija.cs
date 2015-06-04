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

                listaOsoblja = new List<Osoblje>();

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
                MessageBox.Show("Error accesing the database: "+ sqle.Message);
            }
            finally
            {
                conn.Close();
            }
            return listaOsoblja;
        }

        public void DodajOsoblje(string naziv, double cijena, string tip, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);

                byte[] photo = reader.ReadBytes((int)fs.Length);
                reader.Close();
                fs.Close();


                string query = "INSERT INTO Osoblje (Naziv, Cijena, Tip, Slika) VALUES (@Naziv, @Cijena, @Tip, @Slika)";


                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add("@Naziv", System.Data.SqlDbType.VarChar, 50).Value = naziv;
                command.Parameters.Add("@Cijena", System.Data.SqlDbType.Float).Value = cijena;
                command.Parameters.Add("@Tip", System.Data.SqlDbType.VarChar, 50).Value = tip;
                command.Parameters.Add("@Slika", System.Data.SqlDbType.Image, photo.Length).Value = photo;


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

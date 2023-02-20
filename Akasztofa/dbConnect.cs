using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Akasztofa
{
    internal class dbConnect
    {
        private MySqlConnection con;

        public dbConnect(string host, string dbname, string ui, string pw)
        {
            con = new MySqlConnection($"Database = {dbname}; Data Source = {host}; User Id = {ui}; Password = {pw};");
        }
        private bool Connect()
        {
            try
            {
                con.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Connect_Close()
        {
            try
            {
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

       public bool FhExists(string ui)
       {
            if (Connect())
            {
                string query = "SELECT ui FROM felhasznalok WHERE ui LIKE @ui;";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ui", ui);
                if (cmd.ExecuteScalar() == null)
                {
                    Connect_Close();
                    return true;
                }
                else
                {
                    Connect_Close();
                    return false;
                }
            }
            Connect_Close();
            return false;
        }

        public bool InsertInto(string nev, string jelszo)
        {
            if (Connect())
            {
                string query = "Insert Into felhasznalok(ui, pw) Values(@ui, @pw)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ui", nev);
                cmd.Parameters.AddWithValue("@pw", jelszo);
                cmd.ExecuteNonQuery();

                Connect_Close();
                return true;
            }
            return false;
        }
        
    }
}

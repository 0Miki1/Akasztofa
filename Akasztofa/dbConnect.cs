using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public bool InsertSzo(szo sz)
        {
            if (Connect())
            {
                string query = "INSERT INTO szavak(szo, nehezseg) VALUES(@szo, @nehezseg)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@szo", sz.Word);
                cmd.Parameters.AddWithValue("@nehezseg", sz.Nehezseg);
                cmd.ExecuteNonQuery();
                Connect_Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WordExists(szo sz)
        {
            if (Connect())
            {
                string query = "SELECT szo FROM szavak WHERE szo LIKE @word";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@word", sz.Word);

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

            return false;
        }

        public List<string> KezdoBetuk()
        {
            List<string> lista = new List<string>();

            if (Connect())
            {
                string query = "SELECT SUBSTRING(szo, 1, 1) FROM szavak";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(reader.GetString(0));
                }

                Connect_Close();

            }

            return lista;
        }

        public bool DeleteWord(szo sz)
        {
            if (Connect())
            {
                string query = "DELETE FROM szavak WHERE szo LIKE @word;";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@word", sz.Word);
                cmd.ExecuteNonQuery();
                Connect_Close();

                return true;
            }

            return false;
        }

        public List<szo> SelectSzavak(string kezdobetu)
        {
            List<szo> szavak = new List<szo>();

            if (Connect())
            {
                string query = "SELECT szo FROM szavak WHERE szo LIKE '@kezd%';";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@kezd", kezdobetu);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    szavak.Add(new szo(reader.GetString(0)));
                }

                Connect_Close();
            }

            return szavak;
        }
    }
}

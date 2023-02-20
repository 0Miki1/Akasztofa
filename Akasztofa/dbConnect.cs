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
    }
}

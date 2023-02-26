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

       public bool FhExists(user u)
       {
            if (Connect())
            {
                string query = "SELECT ui FROM felhasznalok WHERE ui LIKE @ui;";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ui", u.Fid);
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

        public bool InsertInto(user u)
        {
            if (Connect())
            {
                string query = "Insert Into felhasznalok(ui, pw) Values(@ui, @pw)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ui", u.Fid);
                cmd.Parameters.AddWithValue("@pw", u.Pw);
                cmd.ExecuteNonQuery();

                Connect_Close();
                return true;
            }
            return false;
        }
        
    }
}

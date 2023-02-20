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

        public user Login(user u)
        {
            user fh = new user();
            if (Connect())
            {
                string query = "Select ui,pw From felhasznalok where ui like @ui and pw like @pw";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ui", u.Fid);
                cmd.Parameters.AddWithValue("@pw", u.Pw);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fh.Fid = reader.GetString(0);
                    fh.Pw = reader.GetString(1);
                }
            }

            return fh;
        }
    }
}

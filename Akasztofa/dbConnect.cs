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

        public user Login(user u)
        {
            user fh = new user();
            if (Connect())
            {
                string query = "SELECT ui,pw FROM felhasznalok WHERE ui LIKE @ui AND pw LIKE @pw;";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ui", u.Fid);
                cmd.Parameters.AddWithValue("@pw", u.Pw);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    fh.Fid = reader.GetString(0);
                    fh.Pw = reader.GetString(1);
                }

                Connect_Close();
            }

            return fh;
        }

        public user SelectStat(string fid)
        {
            user roman = new user(fid);
            if (Connect())
            {
                string query = "SELECT konnyuossz,konnyunyert,kozepesossz,kozepesnyert,nehezossz,neheznyert FROM jatekok WHERE fid like @fid";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@fid", roman.Fid);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    roman.Konnyuossz = Convert.ToInt32(reader.GetString(0));
                    roman.Konnyunyert = Convert.ToInt32(reader.GetString(1));
                    roman.Kozepossz = reader.GetInt32(2);
                    roman.Kozepnyert = reader.GetInt32(3);
                    roman.Nehezossz = reader.GetInt32(4);
                    roman.Neheznyert = reader.GetInt32(5);
                }
            }
            return roman;
        }

        public bool UpdateFasz(string fid, int nehezseg, int nyert)
        {
            if (Connect())
            {
                if (nyert == 1)
                {
                    if (nehezseg == 1)
                    {
                        string query = "Update jatekok set konnyuossz = konnyuossz + 1, konnyunyert = konnyunyert + 1 where @fid like fid";
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@fid", fid);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        if (nehezseg == 2)
                        {
                            string query = "Update jatekok set kozepesossz = kozepesossz + 1, kozepesnyert = kozepesnyert + 1 where @fid like fid";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@fid", fid);
                            cmd.ExecuteNonQuery();
                            return true;
                        }
                        else
                        {
                            if (nehezseg == 3)
                            {
                                string query = "Update jatekok set nehezossz = nehezossz + 1, neheznyert = neheznyert + 1 where @fid like fid";
                                MySqlCommand cmd = new MySqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@fid", fid);
                                cmd.ExecuteNonQuery();
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (nehezseg == 1)
                    {
                        string query = "Update jatekok set konnyuossz = konnyuossz + 1 where @fid like fid";
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@fid", fid);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        if (nehezseg == 2)
                        {
                            string query = "Update jatekok set kozepesossz = kozepesossz + 1 where @fid like fid";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@fid", fid);
                            cmd.ExecuteNonQuery();
                            return true;
                        }
                        else
                        {
                            if (nehezseg == 3)
                            {
                                string query = "Update jatekok set nehezossz = nehezossz + 1 where @fid like fid";
                                MySqlCommand cmd = new MySqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@fid", fid);
                                cmd.ExecuteNonQuery();
                                return true;
                            }
                        }
                    }
                }
                Connect_Close();
            }
            return false;
        }

        public szo SelectRandSzo(szo sz)
        {
            szo ujszo = new szo(sz.Nehezseg);

            if (Connect())
            {
                string query = "SELECT szo FROM szavak WHERE nehezseg LIKE @nehezseg ORDER BY RAND() LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nehezseg", sz.Nehezseg);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ujszo.Word = reader.GetString(0);
                }

                Connect_Close();
            }

            return ujszo;
        }
    }
}

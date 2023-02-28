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
                string query = "Insert Into felhasznalok(ui, pw) Values(@ui, @pw); INSERT INTO jatekok(fid, konnyuossz, konnyunyert, kozepesossz, kozepesnyert, nehezossz, neheznyert) VALUES(@fid, 0, 0, 0, 0, 0, 0)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ui", u.Fid);
                cmd.Parameters.AddWithValue("@pw", u.Pw);
                cmd.Parameters.AddWithValue("@fid", u.Fid);
                cmd.ExecuteNonQuery();

                Connect_Close();
                return true;
            }
            return false;
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
                string query = "SELECT DISTINCT SUBSTRING(szo, 1, 1) FROM szavak ORDER BY 1 ASC;";
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

        public bool DeleteWord(List<szo> lista)
        {
            if (Connect())
            {
                string query = "DELETE FROM szavak WHERE szo LIKE @word0";

                for (int i = 1; i < lista.Count; i++)
                {
                    query += $" OR szo LIKE @word{i}";
                }

                MySqlCommand cmd = new MySqlCommand(query, con);

                for (int i = 0; i < lista.Count; i++)
                {
                    cmd.Parameters.AddWithValue($"@word{i}", lista[i].Word);
                }

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
                string query = $"SELECT szo FROM szavak WHERE szo LIKE '{kezdobetu}%';";
                MySqlCommand cmd = new MySqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@kezd", kezdobetu);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    szavak.Add(new szo(reader.GetString(0)));
                }

                Connect_Close();
            }

            return szavak;
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
                    roman.Konnyuossz = reader.GetInt32(0);
                    roman.Konnyunyert = reader.GetInt32(1);
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

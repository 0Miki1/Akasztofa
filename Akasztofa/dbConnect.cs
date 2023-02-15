﻿using System;
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

        public bool Login(string nev, string jelszo)
        {
            if (Connect())
            {
                string query = "Select ui,pw From felhasznalok where ui like @ui and pw like @pw";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ui", nev);
                cmd.Parameters.AddWithValue("@pw", jelszo);
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

        public bool SelectStat(string fid)
        {
            if (Connect())
            {
                string query = "SELECT konnyuossz,konnyunyert,kozepesossz,kozepesnyert,nehezossz,neheznyert FROM jatekok WHERE fid like @fid";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@fid", fid);
                cmd.ExecuteReader();
            }
            return false;
        }
    }
}

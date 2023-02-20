﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Akasztofa
{
    /// <summary>
    /// Interaction logic for bejelentkezes.xaml
    /// </summary>
    public partial class bejelentkezes : Window
    {
        private MainWindow mw;

        public bejelentkezes(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.Hide();
            mw.Show();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            if (LogTBF.Text != string.Empty &&LogPBJ.Password != string.Empty)
            {
                dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
                user login = db.Login(new user(LogTBF.Text, LogPBJ.Password));
                if (login.Fid != string.Empty && login.Pw != string.Empty)
                {
                    //jatek statisztika
                    if (login.Fid == "admin" && login.Pw == "admin")
                    {
                        LogTBF.Text = string.Empty;
                        LogPBJ.Password = string.Empty;
                        MessageBox.Show("Sikeres admin bejelentkezés", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        //admin a = new admin(this);
                        //this.Hide();
                        //a.Show();
                    }
                    else
                    {
                        LogTBF.Text = string.Empty;
                        LogPBJ.Password = string.Empty;
                        MessageBox.Show("Sikeres bejelentkezés", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        //statisztika s = new statisztika(this, login.Fid);
                        //this.Hide();
                        //s.Show();

                    }
                }
                else
                {
                    MessageBox.Show("Sikertelen bejelentkezés", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LogTBF.Text = string.Empty;
                    LogPBJ.Password = string.Empty;
                }
            }
            else
            {
                if (LogTBF.Text == string.Empty && LogPBJ.Password == string.Empty)
                {
                    MessageBox.Show("Adjon meg egy felhasználónevet és egy jelszót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (LogTBF.Text == string.Empty && LogPBJ.Password != string.Empty)
                {
                    MessageBox.Show("Adjon meg egy felhasználónevet!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Adjon meg egy jelszót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

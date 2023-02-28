using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        //mainwindow-ba static
        private string stringToSha256(string jelsz)
        {
            SHA256 sha = new SHA256Managed();
            byte[] b = sha.ComputeHash(Encoding.UTF8.GetBytes(jelsz));

            StringBuilder strbldr = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                strbldr.Append(b[i].ToString("x2"));
            }

            return strbldr.ToString();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            if (LogTBF.Text != string.Empty &&LogPBJ.Password != string.Empty)
            {
                dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
                user login = db.Login(new user(LogTBF.Text, stringToSha256(Convert.ToString(LogPBJ.Password))));

                if (login.Fid != string.Empty && login.Pw != string.Empty)
                {
                    //jatek statisztika
                    if (login.Fid == "admin" && login.Pw == stringToSha256("admin"))
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

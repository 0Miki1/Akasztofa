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
    /// Interaction logic for regisztracio.xaml
    /// </summary>
    public partial class regisztracio : Window
    {
        protected MainWindow mw;

        public regisztracio(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.Close();
            mw.Show();
        }

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

        private void reg(object sender, RoutedEventArgs e)
        {
            if (RegTBFh.Text != string.Empty && RegPbJelsz.Password != string.Empty && RegPbJelszU.Password != string.Empty)
            {
                if (RegPbJelsz.Password == RegPbJelszU.Password)
                {
                    dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
                    if (db.FhExists(new user(RegTBFh.Text)))
                    {
                        db.InsertInto(new user(RegTBFh.Text, stringToSha256(RegPbJelsz.Password)));
                        MessageBox.Show("Sikeres regisztráció","", MessageBoxButton.OK);
                        RegTBFh.Text = string.Empty;
                        RegPbJelsz.Password = string.Empty;
                        RegPbJelszU.Password = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Van már ilyen felh név te fasz", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        RegTBFh.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("A jelszavak nem egyeznek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    RegPbJelsz.Password = string.Empty;
                    RegPbJelszU.Password = string.Empty;
                }
            }
            else
            {
                if (RegTBFh.Text == string.Empty && RegPbJelsz.Password == string.Empty && RegPbJelszU.Password == string.Empty)
                {
                    MessageBox.Show("Adjon meg egy felhasználó nevet és egy jelszót kétszer!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (RegTBFh.Text == string.Empty && RegPbJelsz.Password != string.Empty && RegPbJelszU.Password != string.Empty)
                {
                    MessageBox.Show("Adjon meg egy felhasználónevet!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

                } else if (RegTBFh.Text != string.Empty && RegPbJelsz.Password == string.Empty && RegPbJelszU.Password == string.Empty)
                {
                    MessageBox.Show("Adjon meg egy jelszót kétszer!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (RegTBFh.Text != string.Empty && RegPbJelsz.Password == string.Empty && RegPbJelszU.Password != string.Empty)
                {
                    MessageBox.Show("Adjon meg egy jelszót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (RegTBFh.Text != string.Empty && RegPbJelsz.Password != string.Empty && RegPbJelszU.Password == string.Empty)
                {
                    MessageBox.Show("Adja meg a jelszavát mégegyszer!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (RegTBFh.Text == string.Empty && RegPbJelsz.Password != string.Empty && RegPbJelszU.Password == string.Empty)
                {
                    MessageBox.Show("Adjon meg egy felhasználónevet és a jelszavát mégegyszer!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (RegTBFh.Text == string.Empty && RegPbJelsz.Password == string.Empty && RegPbJelszU.Password != string.Empty)
                {
                    MessageBox.Show("Adjon meg egy felhasználónevet és egy jelszót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

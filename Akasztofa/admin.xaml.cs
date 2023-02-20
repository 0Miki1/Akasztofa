using System;
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
using Microsoft.Win32;
using System.IO;

namespace Akasztofa
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        private MainWindow mw;

        public admin(MainWindow mw)
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

        private void betoltes(object sender, RoutedEventArgs e)
        { 
            foreach (var item in szo.Nehezsegek)
            {
                ca1.Items.Add(item);
            }
        }

        private void faszom(object sender, SelectionChangedEventArgs e)
        {
            int nehezseg = 0;
            if (ca1.SelectedItem == "Könnyű")
            {
                nehezseg = 1;
                la1.Content = "Könnyű: 1-7 karakter";
            }
            if (ca1.SelectedItem == "Közepes")
            {
                nehezseg = 2;
                la1.Content = "Közepes: 8-12 karakter";
            }
            if (ca1.SelectedItem == "Nehéz")
            {
                nehezseg = 3;
                la1.Content = "Nehéz: 12-nél több karakter";
            }
        }

        private void feltoltes(object sender, RoutedEventArgs e)
        {
            if (ta1.Text.All(char.IsLetter))
            {
                if (ca1.SelectedIndex == -1)
                {
                    MessageBox.Show("Nem választott nehézségi szintet","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
                    szo sz = new szo(ta1.Text, ca1.SelectedIndex + 1);

                    if (db.WordExists(sz))
                    {
                        if (db.InsertSzo(sz))
                        {
                            MessageBox.Show("Sikeres adatfelvétel!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            ca1.SelectedIndex = -1;
                            ta1.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Sikertelen adatfelvétel, próbálja újra!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            ca1.SelectedIndex = -1;
                            ta1.Text = string.Empty;
                        }
                    }
                    else
                    {
                        ta1.Text = string.Empty;
                        MessageBox.Show("Ez a szó már fel van véve, próbáljon meg egy másikat!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                ta1.Text = string.Empty;
                MessageBox.Show("Nem szöveget írt be", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void pirosRoman(object sender, TextChangedEventArgs e)
        {
            int karakterE = 0;
            int karakterV = 0;
            if (ca1.SelectedItem == "Könnyű")
            {
                karakterE = 1;
                karakterV = 7;
                if (ta1.Text.Length < karakterE || ta1.Text.Length > karakterV)
                {
                    la1.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    la1.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
            if (ca1.SelectedItem == "Közepes")
            {
                karakterE = 8;
                karakterV = 12;
                if (ta1.Text.Length < karakterE || ta1.Text.Length > karakterV)
                {
                    la1.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    la1.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
            if (ca1.SelectedItem == "Nehéz")
            {
                karakterE = 13;
                karakterV = 99;
                if (ta1.Text.Length < karakterE || ta1.Text.Length > karakterV)
                {
                    la1.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    la1.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            //kezdo betuk
            //torles
            //ujratoltes
        }

        private void open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text files (*.txt)|*.txt";
            open.InitialDirectory = "c:\\";
            open.FilterIndex = 2;
            open.RestoreDirectory = true;

            if (open.ShowDialog() == true)
            {
                insertSzavak(open.FileName);
                MessageBox.Show("Sikeres adatfelvétel!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void insertSzavak(string file)
        {
            //van e mar ilyen ellenorzes
            FileStream fs = new FileStream(file, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");

            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                int seged = sor.Length;

                if (seged >= 1 && seged <= 7)
                {
                    db.InsertSzo(new szo(sor, 1));
                }
                else if (seged >= 8 && seged <= 12)
                {
                    db.InsertSzo(new szo(sor, 2));
                }
                else if (seged >= 13 && seged <= 99)
                {
                    db.InsertSzo(new szo(sor, 3));
                }
            }

            sr.Close();
            fs.Close();
        }
    }
}

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
        private bool joadat;

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

        private void adatbetoltes()
        {
            ct1.Items.Clear();
            lb.Items.Clear();

            dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
            //kezdo betuk
            foreach (var item in db.KezdoBetuk())
            {
                ct1.Items.Add(item);
            }
        }

        private void betoltes(object sender, RoutedEventArgs e)
        {
            adatbetoltes();

            foreach (var item in szo.Nehezsegek)
            {
                ca1.Items.Add(item);
            }
        }

        private void nehezsegChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToString(ca1.SelectedItem) == "Könnyű")
            {
                la1.Content = "Könnyű: 1-7 karakter";
            }
            if (Convert.ToString(ca1.SelectedItem) == "Közepes")
            {
                la1.Content = "Közepes: 8-12 karakter";
            }
            if (Convert.ToString(ca1.SelectedItem) == "Nehéz")
            {
                la1.Content = "Nehéz: 12-nél több karakter";
            }
        }

        private void szinValto(object sender, TextChangedEventArgs e)
        {
            int karakterE = 0;
            int karakterV = 0;
            if (Convert.ToString(ca1.SelectedItem) == "Könnyű")
            {
                karakterE = 1;
                karakterV = 7;
                if (ta1.Text.Length < karakterE || ta1.Text.Length > karakterV)
                {
                    la1.Foreground = new SolidColorBrush(Colors.Red);
                    feltoltG.IsEnabled = false;
                    joadat = false;
                }
                else
                {
                    la1.Foreground = new SolidColorBrush(Colors.Green);
                    feltoltG.IsEnabled = true;
                    joadat = true;
                }
            }
            if (Convert.ToString(ca1.SelectedItem) == "Közepes")
            {
                karakterE = 8;
                karakterV = 12;
                if (ta1.Text.Length < karakterE || ta1.Text.Length > karakterV)
                {
                    la1.Foreground = new SolidColorBrush(Colors.Red);
                    feltoltG.IsEnabled = false;
                    joadat = false;
                }
                else
                {
                    la1.Foreground = new SolidColorBrush(Colors.Green);
                    feltoltG.IsEnabled = true;
                    joadat = true;

                }
            }
            if (Convert.ToString(ca1.SelectedItem) == "Nehéz")
            {
                karakterE = 13;
                karakterV = 99;
                if (ta1.Text.Length < karakterE || ta1.Text.Length > karakterV)
                {
                    la1.Foreground = new SolidColorBrush(Colors.Red);
                    feltoltG.IsEnabled = false;
                    joadat = false;
                }
                else
                {
                    la1.Foreground = new SolidColorBrush(Colors.Green);
                    feltoltG.IsEnabled = true;
                    joadat = true;
                }
            }
        }

        private void feltoltes(object sender, RoutedEventArgs e)
        {
            if (ta1.Text.All(char.IsLetter))
            {
                if (ta1.Text != string.Empty && ca1.SelectedIndex != -1)
                {
                    dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
                    szo sz = new szo(ta1.Text, ca1.SelectedIndex + 1);

                    if (db.WordExists(sz))
                    {
                        if (joadat)
                        {
                            if (db.InsertSzo(sz))
                            {
                                adatbetoltes();
                                MessageBox.Show("Sikeres adatfelvétel!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                                ca1.SelectedIndex = -1;
                                ta1.Text = string.Empty;
                                la1.Content = string.Empty;
                            }
                            else
                            {
                                MessageBox.Show("Sikertelen adatfelvétel, próbálja újra!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                                ca1.SelectedIndex = -1;
                                ta1.Text = string.Empty;
                                la1.Content = string.Empty;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ellenőrízze, hogy megfelelő hosszúságú adatot írt be!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        ta1.Text = string.Empty;
                        MessageBox.Show("Ez a szó már fel van véve, próbáljon meg egy másikat!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    if (ta1.Text == string.Empty && ca1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Válasszon ki egy nehzségi szintet és adjon meg egy feltölteni kívánt szót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (ta1.Text == string.Empty && ca1.SelectedIndex != -1)
                    {
                        MessageBox.Show("Adjon meg egy feltölteni kívánt szót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Nem választott nehézségi szintet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                ta1.Text = string.Empty;
                MessageBox.Show("Nem szöveget írt be", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void szavak(object sender, SelectionChangedEventArgs e)
        {
            lb.Items.Clear();

            dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");

            foreach (var item in db.SelectSzavak(Convert.ToString(ct1.SelectedItem)))
            {
                lb.Items.Add(item.Word);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
            List<szo> szavak = new List<szo>();

            foreach (var item in lb.SelectedItems)
            {
                szavak.Add(new szo(Convert.ToString(item)));
            }

            //torles
            if (ct1.Items.Count != 0)
            {
                if (ct1.SelectedIndex != -1 && lb.SelectedIndex != -1)
                {
                    if (db.DeleteWord(szavak))
                    {
                        ct1.SelectedIndex = -1;
                        lb.SelectedIndex = -1;
                        //ujratoltes
                        adatbetoltes();
                        MessageBox.Show("Sikeres adattörlés!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Sikertelen törlés, próbálja meg újra!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (ct1.SelectedIndex == -1 && lb.SelectedIndex == -1)
                    {
                        MessageBox.Show("Válasszon ki egy kezdőbetűt a szűréshez és legalább egy törölni kívánt szót a törléshez!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else if (ct1.SelectedIndex == -1 && lb.SelectedIndex != -1)
                    {
                        MessageBox.Show("Válasszon ki egy kezdőbetűt a szűréshez!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Válasszon ki legalább egy szót a törléshez!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Először vegyen fel adatot az adatbázisba, hogy törölni lehessen!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            }
        }

        private void insertSzavak(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");

            int sikeresSor = 0;
            int sikertelenSor = 0;

            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                int seged = sor.Length;

                if (seged >= 1 && seged <= 7)
                {
                    szo sz = new szo(sor, 1);
                    if (db.WordExists(sz))
                    {
                        if (db.InsertSzo(sz))
                        {
                            sikeresSor++;
                        }
                        else
                        {
                            sikertelenSor++;
                        }
                    }
                    else
                    {
                        sikertelenSor++;
                    }
                }
                else if (seged >= 8 && seged <= 12)
                {
                    szo sz = new szo(sor, 2);
                    if (db.WordExists(sz))
                    {
                        if (db.InsertSzo(sz))
                        {
                            sikeresSor++;
                        }
                        else
                        {
                            sikertelenSor++;
                        }
                    }
                    else
                    {
                        sikertelenSor++;
                    }
                }
                else if (seged >= 13 && seged <= 99)
                {
                    szo sz = new szo(sor, 3);
                    if (db.WordExists(sz))
                    {
                        if (db.InsertSzo(sz))
                        {
                            sikeresSor++;
                        }
                        else
                        {
                            sikertelenSor++;
                        }
                    }
                    else
                    {
                        sikertelenSor++;
                    }
                }
            }

            sr.Close();
            fs.Close();

            if (sikeresSor > 0)
            {
                adatbetoltes();
                MessageBox.Show($"Sikeres adatfelvétel, összes feltölteni kívánt sor: {(sikeresSor + sikertelenSor)}, ebből sikeres: {sikeresSor}, sikertelen: {sikertelenSor}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Sikertelen adatfelvétel, próbáljon meg egy másik állományt feltölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void kijeloles(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedItems.Count < lb.Items.Count)
            {
                lb.SelectAll();
            }
            else if (lb.SelectedItems.Count == lb.Items.Count)
            {
                lb.UnselectAll();
            }
        }

        private void vissza(object sender, MouseButtonEventArgs e)
        {
            //bejelentkezes b = new bejelentkezes(mw);
            //this.Close();
            //b.Show();
        }

        private void fooldal(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            mw.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for jatekter.xaml
    /// </summary>
    public partial class jatekter : Window
    {
        private user u;
        private szo sz;
        private int progress;
        private int kepsorsz;
        private int hiba;
        private List<char> rossztippek;
        private List<char> kitalaltbetuk;

        public jatekter(szo sz, user u)
        {
            InitializeComponent();
            this.u = u;
            this.sz = sz;
            progress = 0;
            kepsorsz = 13;
            hiba = 0;
            rossztippek = new List<char>();
            kitalaltbetuk = new List<char>();
            szokeres();
        }

        private void szokeres()
        {
            dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
            sz = db.SelectRandSzo(sz);

            //kitalálandó szó karakterszámának megjelenítése
            for (int i = 0; i < sz.Word.Length; i++)
            {
                L1.Text += "_";
            }
        }


        private bool statmod(bool nyert)
        {
            dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
            if (nyert)
            {
                return db.UpdateFasz(u.Fid, sz.Nehezseg, 1);
            }
            else
            {
                return db.UpdateFasz(u.Fid, sz.Nehezseg, 0);
            }
        }

        private void tipp(object sender, KeyEventArgs e)
        {
            char seged = Tipp.Text[0];

            if (Tipp.Text.All(char.IsLetter))
            {
                if (sz.Word.Contains(seged))
                {
                    if (!kitalaltbetuk.Contains(seged))
                    {
                        List<string> l1 = new List<string>();
                        int cnt = sz.Word.Count(x => x == seged);

                        //kitalálandó szó felbontása jelen állapot szerint
                        for (int i = 0; i < L1.Text.Length; i++)
                        {
                            l1.Add(Convert.ToString(L1.Text[i]));
                        }

                        L1.Text = string.Empty;

                        //kitalált karakterek felülírása
                        for (int i = 0; i < l1.Count; i++)
                        {
                            if (sz.Word[i] == seged)
                            {
                                l1[i] = Convert.ToString(seged);
                            }
                        }

                        //kitalálandó szó újrarajzolása, kitalált betűk felfedése
                        for (int i = 0; i < l1.Count; i++)
                        {
                            L1.Text += l1[i];
                        }

                        //előreheladás
                        for (int i = 0; i < cnt; i++)
                        {
                            progress++;
                        }
                        //kitalált betűk bővítése, hogy ne lehessen egy már kitalált betű újra beírásával növelni a progress-t
                        kitalaltbetuk.Add(seged);

                        if (progress == l1.Count)
                        {
                            MessageBox.Show("Gratulálunk nyertél!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            //statisztika növelés
                            if (statmod(true))
                            {
                                if (MessageBox.Show("Szeretne tovább játszani?", "Játék", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                {
                                    modvalaszto m = new modvalaszto(u);
                                    this.Close();
                                    m.Show();
                                }
                                else
                                {
                                    statisztika s = new statisztika(new user("asd"));
                                    this.Close();
                                    s.Show();
                                }
                            }
                        }
                    }

                    Tipp.Text = string.Empty;
                }
                else
                {
                    if (!rossztippek.Contains(seged))
                    {
                        RTipp.Text += $" {seged},";
                        rossztippek.Add(seged);
                        hibaLb.Content = $"{++hiba}/15";

                        if (kepsorsz > -1)
                        {
                            kep.Source = new BitmapImage(new Uri($"/akasztofa{kepsorsz--}.png", UriKind.Relative));
                        }
                        else
                        {
                            kep.Source = new BitmapImage(new Uri($"/akasztofa{kepsorsz--}.png", UriKind.Relative));
                            L1.Text = sz.Word;
                            MessageBox.Show("Vesztettél!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            //statisztika levonás
                            if (statmod(false))
                            {
                                if (MessageBox.Show("Szeretne még egyet játszani?", "Játék", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                {
                                    modvalaszto m = new modvalaszto(u);
                                    this.Close();
                                    m.Show();
                                }
                                else
                                {
                                    statisztika s = new statisztika(new user("asd"));
                                    this.Close();
                                    s.Show();
                                }
                            }
                        }

                        Tipp.Text = string.Empty;
                    }
                    else
                    {
                        Tipp.Text = string.Empty;
                    }
                }
            }
            else
            {
                Tipp.Text = string.Empty;
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Biztosan kiszeretne lépni?", "Kilépés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                statisztika a = new statisztika(new user("asd"));
                this.Close();
                a.Show();
                //adatok modositasa
                dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
                db.UpdateFasz(u.Fid, sz.Nehezseg, 0);
            }
        }
    }
}
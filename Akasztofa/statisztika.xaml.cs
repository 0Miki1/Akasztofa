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
    /// Interaction logic for statisztika.xaml
    /// </summary>
    public partial class statisztika : Window
    {
        protected user u;
        private bejelentkezes b;
        public statisztika(user u, bejelentkezes b)
        {
            InitializeComponent();
            this.u = u;
            this.b = b;
        }

        public statisztika(user u)
        {
            InitializeComponent();
            this.u = u;
        }

        private void load(object sender, RoutedEventArgs e)
        {
            dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
            user s = db.SelectStat(u.Fid);
            labelName.Content = s.Fid + " statisztikái";
            l1.Content += Convert.ToString(s.Konnyuossz);
            l2.Content += Convert.ToString($" {s.Konnyunyert} | {szamol(s.Konnyuossz, s.Konnyunyert)}%");
            l3.Content += Convert.ToString(s.Kozepossz);
            l4.Content += Convert.ToString($" {s.Kozepnyert} | {szamol(s.Kozepossz, s.Kozepnyert)}%");
            l5.Content += Convert.ToString(s.Nehezossz);
            l6.Content += Convert.ToString($" {s.Neheznyert} | {szamol(s.Nehezossz, s.Neheznyert)}%");
            lOsszes.Content += osszes(s.Konnyuossz, s.Kozepossz, s.Nehezossz);
        }

        private double szamol(double ossz, double nyert)
        {
            if (ossz > 0)
            {
                return Math.Round((nyert / ossz) * 100);
            }
            else
            {
                return 0;
            }
        }

        private string osszes(int a, int b, int c)
        {
            int fasz = a + b + c;
            return $"{fasz}";
        }

        private void Game(object sender, MouseButtonEventArgs e)
        {
            modvalaszto m = new modvalaszto(u, this);
            this.Hide();
            m.Show();
        }

        private void vissza(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            b.Show();
        }

        private void fooldal(object sender, MouseButtonEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
    }
}
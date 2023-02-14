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
            List<string> lista = new List<string>();
            lista.Add("Könnyű");
            lista.Add("Közepes");
            lista.Add("Nehéz");
            foreach (var item in lista)
            {
                ca1.Items.Add(item);
            }
            
        }

        private void fasza(object sender, DependencyPropertyChangedEventArgs e)
        {
            
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
                la1.Content = "Nehéz: 12-nél több";
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
                karakterE = 12;
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

        }
    }
}

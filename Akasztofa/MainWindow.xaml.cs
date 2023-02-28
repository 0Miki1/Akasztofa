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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Akasztofa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private void regisztracio(object sender, RoutedEventArgs e)
        {
            this.Hide();
            regisztracio r = new regisztracio(this);
            r.Show();
        }

        private void bejelentkezes(object sender, RoutedEventArgs e)
        {
            //bejelentkezes b = new bejelentkezes(this);
            //this.Hide();
            //b.Show();
            //modvalaszto j = new modvalaszto(new user("asd"));
            //this.Hide();
            //j.Show();
            //statisztika s = new statisztika(new user("a"));
            //this.Hide();
            //s.Show();
        }
    }
}

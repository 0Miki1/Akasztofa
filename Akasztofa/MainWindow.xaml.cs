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

        private void bejelentkezes(object sender, RoutedEventArgs e)
        {
            //bejelentkezes b = new bejelentkezes(this);
            //this.Hide();
            //b.Show();
            jatekter j = new jatekter();
            this.Hide();
            j.Show();
        }
    }
}

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
    /// Interaction logic for modvalaszto.xaml
    /// </summary>
    public partial class modvalaszto : Window
    {
        private user u;

        public modvalaszto(user u)
        {
            InitializeComponent();
            this.u = u;
        }

        private void konnyu(object sender, RoutedEventArgs e)
        {
            jatekter j = new jatekter(new szo(1), u);
            this.Close();
            j.Show();
        }

        private void kozepes(object sender, RoutedEventArgs e)
        {
            jatekter j = new jatekter(new szo(2), u);
            this.Close();
            j.Show();
        }

        private void nehez(object sender, RoutedEventArgs e)
        {
            jatekter j = new jatekter(new szo(3), u);
            this.Close();
            j.Show();
        }
    }
}

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
    /// Interaction logic for jatekter.xaml
    /// </summary>
    public partial class jatekter : Window
    {
        statisztika stat;
        private user u;
        private int nehezseg;
        public jatekter(int nehezseg, user u)
        {
            InitializeComponent();
            this.nehezseg= nehezseg;
            this.u = u;
        }

        private void tipp(object sender, TextChangedEventArgs e)
        {

        }

        private void exit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Biztosan kiszeretne lépni?", "Kilépés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                statisztika a = new statisztika();
                this.Close();
                a.Show();
                //adatok modositasa
                dbConnect db = new dbConnect("localhost", "akasztofa", "root", "");
                db.UpdateFasz(u.Fid, nehezseg, 0);
            }
        }
    }
}

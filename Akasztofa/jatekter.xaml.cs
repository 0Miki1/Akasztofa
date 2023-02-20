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
using System.Drawing.Bitmap;
using System.Drawing.Imaging.Metafile;

namespace Akasztofa
{
    /// <summary>
    /// Interaction logic for jatekter.xaml
    /// </summary>
    public partial class jatekter : Window
    {
        public jatekter()
        {
            InitializeComponent();
        }

        private void load(object sender, RoutedEventArgs e)
        {
            image = Image.FromFile("C:\\Users\\nagya\\source\\repos\\0Miki1\\Akasztofa\\Akasztofa\\frames");
        }
    }
}

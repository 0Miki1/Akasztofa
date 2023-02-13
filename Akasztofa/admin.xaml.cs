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
    }
}

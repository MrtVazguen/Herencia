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

namespace ReunioSocial
{
    /// <summary>
    /// Lógica de interacción para wndSimpaties.xaml
    /// </summary>
    public partial class wndSimpaties : Window
    {
       
        public wndSimpaties(TaulaPersones gent)
        {
            InitializeComponent();
            AddChild(new Simpaties(gent));
        }

        private void grdSimpaties_Loaded(object sender, RoutedEventArgs e)
        {
            wndTauler tauler = (wndTauler)this.Owner; 
            SortedDictionary<String, Int32> simpaties = tauler.simpaties;
             
        }
    }
}

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
    /// Lógica de interacción para wndTauler.xaml
    /// </summary>
    public partial class wndTauler : Window
    {
        public SortedDictionary<String, Int32> simpaties;
        private Escenari escenari;
        
        public wndTauler(Escenari escenari)
        {
            InitializeComponent();

            MainWindow owner = (MainWindow)Owner;
            this.escenari = escenari;


            dpPrincipal.Children.Add(escenari);
        }

        private void grdTauler_Loaded(object sender, RoutedEventArgs e)
        {
             
            MainWindow window = (MainWindow)this.Owner;
            
             
            //grdTauler.Files = int.Parse(window.tbFiles.Text);
            // grdTauler.Columnes = int.Parse(window.tbColumnes.Text);

        }

        private void btnSimpaties_Click(object sender, RoutedEventArgs e)
        {
            wndSimpaties simpaties = new wndSimpaties(escenari.Gent);
            simpaties.Owner = this; 
            simpaties.ShowDialog();
        }

        private void btnCicle_Click(object sender, RoutedEventArgs e)
        {
            escenari.Cicle(); 
        }

        private void btnSurt_Click(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }

        private void btnSortir_Click(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }
    }
}

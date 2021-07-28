using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ReunioSocial
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            tbInfo.Text = "";
            if (ValidarCamps())
            {
                Escenari escenari = null;
                try
                {
                    escenari = new Escenari(
                        (int)iudFiles.Value,
                        (int)iudColumnes.Value,
                        (int)iudHomes.Value,
                        (int)iudDones.Value,
                        (int)iudCambrers.Value);
                }
                catch (ArgumentException)
                {
                    tbInfo.Text = "No es pot crear l'escenari perquè hi ha més persones que files i columnes";
                }

                if (escenari != null)
                {
                    Window finestra = new wndTauler(escenari);

                    finestra.Owner = this;
                    finestra.ShowDialog();
                }
            }
            else
            {
                tbInfo.Text = "S'han d'emplenar tots els camps";
            }
        }

        private bool ValidarCamps()
        {
            if (iudFiles.Value == null)
                return false;
            if (iudColumnes.Value == null)
                return false;
            if (iudHomes.Value == null)
                return false;
            if (iudDones.Value == null)
                return false;
            if (iudCambrers.Value == null)
                return false;

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace ReunioSocial
{
    public class Posicio : UserControl
    {
        #region Atributs
        protected string nom;
        protected int fila;
        protected int columna;
        private Image imgIcona;
        private TextBlock tbNom;
        #endregion

        #region Constructors
        public Posicio() : this(0, 0) { }
        public Posicio(int fila, int columna) : this ("", fila, columna) { }
        public Posicio(string nom, int fila, int columna)
        {
            imgIcona = new Image();
            tbNom = new TextBlock();

            this.Nom = nom;
            this.Fila = fila;
            this.Columna = columna;

            Grid grid = new Grid();
            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(rd);
            rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rd);

            Grid.SetRow(imgIcona, 0);
            Grid.SetColumn(imgIcona, 0);
            imgIcona.Margin = new Thickness(2);

            grid.Children.Add(imgIcona);

            Grid.SetRow(tbNom, 1);
            Grid.SetColumn(tbNom, 0);
            tbNom.TextAlignment = TextAlignment.Center;
            tbNom.Foreground = Brushes.Black;
            tbNom.FontSize = 14;
            tbNom.Margin = new Thickness(2);
            this.Background = Brushes.Transparent;
            grid.Children.Add(tbNom);
            // Limpiar Posicion
            AddChild(grid);
          
        }
        public Posicio(string nom) : this(nom, 0, 0) { }
        #endregion

        #region Propietats
        public string Nom 
        {
            get
            {
                return this.nom;
            }
            set
            {
                this.tbNom.Text = value;
                this.nom = value;
            }
        }
        public int Columna 
        {
            get
            {
                return this.columna;
            }
            set
            {
                this.columna = value;
                Grid.SetColumn(this, columna);
            }
        }
        public int Fila 
        {
            get
            {
                return this.fila;
            }
            set
            {
                this.fila = value;
                Grid.SetRow(this, fila);
            }
        }
        public virtual bool EsBuida
        {
            // Devuelve verdadero porque en tiempo de ejecución si es una persona devolverá falso,
            // pero si no es una persona devolverá verdadero porque es un método virtual y persona lo sobreescribe.
            get
            {
                return true;
            }
        }
        public ImageSource Icona
        {
            get
            {
                return this.imgIcona.Source;
            }
            set
            {
                this.imgIcona.Source = value;
            }
        }
        #endregion

        public static double Distancia(Posicio pos1, Posicio pos2)
        {
            return Math.Sqrt(Math.Pow(pos2.columna - pos1.columna, 2) + Math.Pow(pos2.fila - pos1.fila, 2));          
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

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
        public Posicio() : this(0,0) { }
        public Posicio(int fila, int columna) : this ("", 0, 0) { }
        public Posicio(string nom, int fila, int columna)
        {
            this.nom = nom;
            this.fila = fila;
            this.columna = columna;

            // Hay que hacer un new para que no pete la propiedad Icona.
            imgIcona = new Image();
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

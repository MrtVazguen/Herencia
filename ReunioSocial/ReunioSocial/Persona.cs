using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public abstract class Persona : Posicio
    {
        protected bool tractat;

        public Persona() : base() { }
        public Persona(String nom, int fil, int col) : base(nom, fil, col) { }
        public Persona(String nom) : base(nom) { }
       
        public bool Tractat
        {
            get
            {
                return tractat;
            }
            set
            {
                tractat = value;
            }
        }

        public override bool EsBuida 
        {
            get
            {
                return false;
            }
        }

        private double Atraccio(int fil, int col, Escenari esc)
        {
            return 0.0;
        }
        public Direccio OnVaig(Escenari esc)
        {
            return 0;
        }
        public abstract int Interes(Posicio pos);
        public abstract bool EsConvidat { get; }
    }
}

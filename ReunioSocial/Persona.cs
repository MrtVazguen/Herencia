using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public abstract class Persona : Posicio
    {
        private static Random random = new Random();

        SortedDictionary<string, string> diccionariPersona ;//ContainsKey
        protected bool tractat;
        internal object icona;

        public Persona() : base() { }
        public Persona(String nom, int fil, int col) : base(nom, fil, col)
        {
            diccionariPersona = new SortedDictionary<string, string>();
        }
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
            double suma = 0;
            for (int i = 0; i < esc.Files; i++)
            {
                for (int j = 0; j < esc.Columnes; j++)
                {
                    if (!(fil == i && col == j) && !esc[i, j].EsBuida)
                    {
                        suma += Interes(esc[i, j]) / Posicio.Distancia(esc[fil, col], esc[i, j]);
                    }
                }
            }

            return suma;
        }
        public Direccio OnVaig(Escenari esc)
        {
            SortedDictionary<double, List<Direccio>> atraccions = new SortedDictionary<double, List<Direccio>>();

            double atraccio;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (esc.DestiValid(fila + i, columna + j) || (i == 0 && j == 0))
                    {
                        atraccio = Atraccio(fila + i, columna + j, esc);
                        if (atraccions.ContainsKey(atraccio))
                        {
                            atraccions[atraccio].Add(TraduirPosicioADireccio(fila + i, columna + j));
                        }
                        else
                        {
                            atraccions.Add(atraccio, new List<Direccio>());
                            atraccions[atraccio].Add(TraduirPosicioADireccio(fila + i, columna + j));
                        }
                    }
                }
            }

            double maxAtraccio = atraccions.Keys.First();
            foreach (double atrac in atraccions.Keys)
            {
                if (atrac > maxAtraccio)
                {
                    maxAtraccio = atrac;
                }
            }

            return atraccions[maxAtraccio][random.Next(atraccions[maxAtraccio].Count)];
        }

        private Direccio TraduirPosicioADireccio(int fil, int col)
        {
            Direccio direccio;

            if (fil < fila && col < columna)
            {
                direccio = Direccio.Noroest;
            }
            else if (fil < fila && col == columna)
            {
                direccio = Direccio.Nord;
            }
            else if (fil < fila && col > columna)
            {
                direccio = Direccio.Nordest;
            }
            else if (fil == fila && col < columna)
            {
                direccio = Direccio.Oest;
            }
            else if (fil == fila && col > columna)
            {
                direccio = Direccio.Est;
            }
            else if (fil > fila && col < columna)
            {
                direccio = Direccio.Sudoest;
            }
            else if (fil > fila && col == columna)
            {
                direccio = Direccio.Sud;
            }
            else if (fil > fila && col > columna)
            {
                direccio = Direccio.Sudest;
            }
            else
            {
                direccio = Direccio.Quiet;
            }

            return direccio;
        }

        public abstract int Interes(Posicio pos);
        public abstract bool EsConvidat { get; }
    }
}

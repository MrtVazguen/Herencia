using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows.Media;

namespace ReunioSocial
{
    public class Escenari : Grid
    {
        TaulaPersones gent;
        private Posicio[,] posicions;
        private static Random random = new Random();
        private const string FitxerNomsNenCurts = "../../Noms/NomsNenCurts.txt";//..\\..\\
        private const string FitxerNomsNenaCurts = "../../Noms/NomsNenaCurts.txt"; 
        private const string CarpetaImatges = "../../Imatges/";

        public Escenari() : this (5, 5, 2, 2, 2)
        {           
            //Files = 5;
            //Columnes = 5;
            //this.ShowGridLines = true;
        }

        public Escenari(int files, int columnes) : this(files, columnes, 2, 2, 2)
        { }

        public Escenari(int files, int columnes, int nHomes, int nDones, int nCambrers)
        {
            if (files * columnes < nHomes + nDones + nCambrers)
            {
                throw new ArgumentException("No es pot crear l'escenari perquè hi ha més persones que files i columnes.");
            }

            gent = new TaulaPersones();
            this.ShowGridLines  = true;
            
            int fila;
            int columna;

            RowDefinition rd;
            for (fila = 0; fila < files; fila++)
            {
                rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                RowDefinitions.Add(rd);
            }

            ColumnDefinition cd;
            for (columna = 0; columna < columnes; columna++)
            {
                cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinitions.Add(cd);
            }

            posicions = new Posicio[files, columnes];            

            for (fila = 0; fila < posicions.GetLength(0); fila++)
            {
                for (columna = 0; columna < posicions.GetLength(1); columna++)
                {
                    posicions[fila, columna] = new Posicio(fila, columna);
                    Children.Add(posicions[fila, columna]);
                }
            }

            for (int i = 0; i < nHomes; i++)
            {
                do
                {
                    fila = random.Next(files);
                    columna = random.Next(columnes);

                } while (!DestiValid(fila, columna));

                Children.Remove(posicions[fila, columna]);
                posicions[fila, columna] = new Home(ObteNom(FitxerNomsNenCurts), 0);
                posicions[fila, columna].Fila = fila;
                posicions[fila, columna].Columna = columna;
                ObteImatge((Persona)posicions[fila, columna]);
                gent.AfegeixPersona((Persona)posicions[fila, columna]);
                posicions[fila, columna].Background = Brushes.LightBlue;
                
                Children.Add(posicions[fila, columna]);
            }

            for (int i = 0; i < nDones; i++)
            {
                do
                {
                    fila = random.Next(files);
                    columna = random.Next(columnes);
                } while (!DestiValid(fila, columna));

                Children.Remove(posicions[fila, columna]);
                posicions[fila, columna] = new Dona(ObteNom(FitxerNomsNenaCurts), 0);
                posicions[fila, columna].Fila = fila;
                posicions[fila, columna].Columna = columna;
                ObteImatge((Persona)posicions[fila, columna]);
                gent.AfegeixPersona((Persona)posicions[fila, columna]);
                posicions[fila, columna].Background = Brushes.LightPink;
                Children.Add(posicions[fila, columna]);
            }

            for (int i = 0; i < nCambrers; i++)
            {
                do
                {
                    fila = random.Next(files);
                    columna = random.Next(columnes);
                } while (!DestiValid(fila, columna));

                Children.Remove(posicions[fila, columna]);
                posicions[fila, columna] = new Cambrer();
                posicions[fila, columna].Fila = fila;
                posicions[fila, columna].Columna = columna;
                ObteImatge((Persona)posicions[fila, columna]);
                gent.AfegeixPersona((Persona)posicions[fila, columna]);
                posicions[fila, columna].Background = Brushes.LightYellow;                
                Children.Add(posicions[fila, columna]);
            }          
        }

        public int Files
        {
            get
            {
                return RowDefinitions.Count;
            }
        }

        public int Columnes
        {
            get
            {
                return ColumnDefinitions.Count;
            }
        }
        public int Homes
        {
            get
            {
                return gent.NHomes;
            }
        }
        public int Dones
        {
            get
            {
                return gent.NDones;
            }
        }
        public int Cambrers
        {
            get
            {
                return gent.NCambrers;
            }
        }

        public TaulaPersones Gent
        {
            get
            {
                return gent;
            }
        }

        private void Moure(int filOrig, int colOrig, int filDesti, int colDesti)
        {
            if (DestiValid(filDesti, colDesti))
            {
                posicions[filOrig, colOrig].Fila = filDesti;
                posicions[filOrig, colOrig].Columna = colDesti;

                Children.Remove(posicions[filDesti, colDesti]);
                posicions[filDesti, colDesti] = posicions[filOrig, colOrig];
                posicions[filOrig, colOrig] = new Posicio(filOrig, colOrig);
            }
        }

        public Posicio this[int fila, int col]
        {
            get
            {
                if (fila >= Files || col >= Columnes
                    || fila < 0 || col < 0)
                {
                    throw new IndexOutOfRangeException("La posició està fora de l'escenari.");
                }

                return posicions[fila, col];
            }
            set
            {
                if (fila >= Files || col >= Columnes
                    || fila < 0 || col < 0)
                {
                    throw new IndexOutOfRangeException("La posició està fora de l'escenari.");
                }

                if (!value.EsBuida)
                {
                    int i = 0;
                    while (i < gent.Count && gent[i].Nom != value.Nom)
                    {
                        i++;
                    }

                    if (i != gent.Count)
                    {
                        throw new ArgumentException("Hi ha una persona amb el mateix nom a l'escenari.");
                    }

                    gent.AfegeixPersona((Persona)value);
                }

                posicions[fila, col] = value;
            }
        }

        public bool DestiValid(int fil, int col)
        {
            return fil < Files && col < Columnes
                && col >= 0 && fil >= 0
                && this[fil, col].EsBuida;
        }

        public void Buida(int fil, int col)
        {
            // Vaciar posicion
            this[fil, col] = new Posicio(fil, col);
        }

        public void Posa(Persona pers)
        {
            int i = 0;
            while (i < gent.Count && gent[i].Nom != pers.Nom)
            {
                i++;
            }

            if (i != gent.Count)
            {
                throw new ArgumentException("Hi ha una persona amb el mateix nom a l'escenari.");
            }

            gent.AfegeixPersona(pers);
            posicions[pers.Fila, pers.Columna] = pers;
        }

        private String ObteNom(String fitxer)
        {
            string nom;
            string linia;
            List<string> noms = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(fitxer, Encoding.UTF8))
                {
                    while ((linia = sr.ReadLine()) != null)
                    {
                        noms.Add(linia);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            int i;
            do
            {
                i = 0;
                nom = noms[random.Next(noms.Count)];
                while (i < gent.Count && nom != gent[i].Nom)
                {
                    i++;
                }
            } while (i != gent.Count);

            return nom;
        }

        private void ObteImatge(Persona p)
        {
            Uri uriImatge;
            if (p.EsConvidat)
            {
                if (((Convidat)p).EsHome)
                {
                    uriImatge = new Uri(CarpetaImatges + "Home" + random.Next(10) + ".png", UriKind.Relative);
                }
                else
                {
                    uriImatge = new Uri(CarpetaImatges + "Dona" + random.Next(10) + ".png", UriKind.Relative);
                }
            }
            else
            {
                uriImatge = new Uri(CarpetaImatges + "Cambrer.png", UriKind.Relative);
            }

            BitmapImage bitmap = new BitmapImage(uriImatge);
            p.Icona = bitmap;
        }

        public void Cicle()
        {
            int[] novaPosicio;
            foreach (Persona persona in gent)
            {
                novaPosicio = TraduirDireccioAPosicio(persona, persona.OnVaig(this));
                Moure(persona.Fila, persona.Columna, novaPosicio[0], novaPosicio[1]);
            }
        }

        private int[] TraduirDireccioAPosicio(Persona persona, Direccio direccio)
        {
            int[] posicio = new int[2];
            switch (direccio)
            {
                case Direccio.Quiet:
                    posicio[0] = persona.Fila;
                    posicio[1] = persona.Columna;
                    break;
                case Direccio.Nord:
                    posicio[0] = persona.Fila - 1;
                    posicio[1] = persona.Columna;
                    break;
                case Direccio.Nordest:
                    posicio[0] = persona.Fila - 1;
                    posicio[1] = persona.Columna + 1;
                    break;
                case Direccio.Est:
                    posicio[0] = persona.Fila;
                    posicio[1] = persona.Columna + 1;
                    break;
                case Direccio.Sudest:
                    posicio[0] = persona.Fila + 1;
                    posicio[1] = persona.Columna + 1;
                    break;
                case Direccio.Sud:
                    posicio[0] = persona.Fila + 1;
                    posicio[1] = persona.Columna;
                    break;
                case Direccio.Sudoest:
                    posicio[0] = persona.Fila + 1;
                    posicio[1] = persona.Columna - 1;
                    break;
                case Direccio.Oest:
                    posicio[0] = persona.Fila;
                    posicio[1] = persona.Columna - 1;
                    break;
                case Direccio.Noroest:
                    posicio[0] = persona.Fila - 1;
                    posicio[1] = persona.Columna - 1;
                    break;
            }

            return posicio;
        }
    }
}

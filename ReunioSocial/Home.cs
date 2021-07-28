using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class Home : Convidat
    {
        public Home(String nom, SortedDictionary<String, Int32> simpaties, int sexe) : base(nom, simpaties, sexe) { }
        public Home(String nom, int sexe) : base(nom, sexe) { }

        public override bool EsHome
        {
            get
            {
                return true;
            }
        }
        public override int Interes(Posicio pos)
        {
            int resultat;

            if (pos.EsBuida || pos == this)
            {
                resultat = 0;
            }
            else if (((Persona)pos).EsConvidat)
            {
                resultat = simpaties[pos.Nom];
                if (!((Convidat)pos).EsHome)
                {
                    resultat += plusSexe;
                }
            }
            else
            {
                resultat = 1;
            }

            return resultat;
        }
    }
}

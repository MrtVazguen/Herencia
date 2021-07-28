using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    class Dona : Convidat
    {
        public Dona(String nom, SortedDictionary<String, Int32> simpaties, int sexe) : base(nom, simpaties, sexe) { }
     
        public Dona(String nom, int sexe) : base(nom, sexe) { }

        public override bool EsHome
        {
            get
            {
                return false;
            }
        }
        public override int Interes(Posicio pos)
        {
            int resultat = 0;

            if (!(pos.EsBuida || pos == this) && ((Persona)pos).EsConvidat)
            {
                resultat = simpaties[pos.Nom];
                if (((Convidat)pos).EsHome)
                {
                    resultat += plusSexe;
                }
            }

            return resultat;
        }
    }
}

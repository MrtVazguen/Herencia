using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    class Cambrer : Persona
    {
        public Cambrer() : base("Cambrer") { }        

        public override int Interes(Posicio pos)
        {
            int resultat;

            if (pos.EsBuida || pos == this)
            {
                resultat = 0;
            }
            else if (((Persona)pos).EsConvidat)
            {
                resultat = 1;
            }
            else
            {
                resultat = -1;
            }

            return resultat;
        }
        public override bool EsConvidat { get { return false; } }
    }
}

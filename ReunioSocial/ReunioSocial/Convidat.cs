using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public abstract class Convidat : Persona
    {
    
        protected SortedDictionary<String, Int32> simpaties;
        protected int plusSexe;

        public Convidat(String nom, SortedDictionary<String, Int32> simpaties, int plusSexe) : base(nom)
        {
            this.simpaties = simpaties;
            this.plusSexe = plusSexe;
        }

        public Convidat(String nom, int plusSexe) : this(nom, null, plusSexe) { }

        public SortedDictionary<String, Int32> Simpaties
        {
            get
            {
                return simpaties;
            }
        }

        public int PlusSexe
        {
            get
            {
                return plusSexe;
            }
            set
            {
                plusSexe = value;
            }
        }

        public abstract bool EsHome { get; }
        public override bool EsConvidat { get { return true; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReunioSocial
{
    public class TaulaPersones : List<Persona>
    {
        public int NCambrers { get; set; }
        public int NHomes { get; set; }
        public int NDones { get; set; }

        private static Random r = new Random();

        private void CreaSimpaties(Convidat nou)
        {
            foreach (Persona persona in this)
            {
                if (persona.EsConvidat)
                {
                    ((Convidat)persona).Simpaties.Add(nou.Nom, r.Next(-5, 6));
                    nou.Simpaties.Add(persona.Nom, r.Next(-5, 6));
                }
            }

            nou.PlusSexe = r.Next(4);
        }

        public void AfegeixPersona(Persona persona)
        {
            if (persona.EsConvidat)
            {
                CreaSimpaties((Convidat)persona);
                if (((Convidat)persona).EsHome)
                {
                    NHomes++;
                }
                else
                {
                    NDones++;
                }
            }
            else
            {
                NCambrers++;
            }

            Add(persona);
        }

        public void EliminaPersona(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException("El argument persona no pot ser null.");
            }

            if (persona.EsConvidat)
            {
                if (((Convidat)persona).EsHome)
                {
                    NHomes--;
                }
                else
                {
                    NDones--;
                }
            }
            else
            {
                NCambrers--;
            }

            foreach(Persona p in this)
            {
                ((Convidat)p).Simpaties.Remove(persona.Nom);
            }
            
            Remove(persona);
        }
    }
}

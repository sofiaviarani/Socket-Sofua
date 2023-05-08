using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket_4I
{
    public class Rubrica
    {
        private List<Contatto> _contatti;

        public List<Contatto> Contatti
        {
            get
            {
                return _contatti;
            }
            set
            {
                _contatti = value;
            }
        }
        public Rubrica(List<Contatto> contatti)
        {
            Contatti = contatti;
        }
        public Rubrica()
        {
            Contatti = new List<Contatto>();
        }
        public void AggiungiContatto(Contatto c)
        { 
              Contatti.Add(c); 
        }


    }
}

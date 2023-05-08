using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket_4I
{
    public class Contatto
    {
        private string _username;
        private string _indirizzo;
        private string _descrizione;

        #region PROPRIETA
        public string Username
        {
            get
            {
                return _username ;
            }
            set
            {
             
                _username = value;
            }
        }
        public string Indirizzo
        {
            get
            {
                return _indirizzo;
            }
            set
            {

                _indirizzo = value;
            }
        }


        public string Descrizione
        {
            get
            {
                return _descrizione;
            }
            set
            {

                _descrizione = value;
            }
        }
        #endregion
        public Contatto(string u, string i,string d )
        {
            Username = u;
            Indirizzo = i;
            Descrizione = d;

        }
        public Contatto(string u, string i)
        {
            Username = u;
            Indirizzo = i;
        }
        public override string ToString()
        {
            return Username + " " + Descrizione ;
        }
    }
}

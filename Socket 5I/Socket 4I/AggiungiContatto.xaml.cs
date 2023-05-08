using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Socket_4I
{
    /// <summary>
    /// Logica di interazione per AggiungiContatto.xaml
    /// </summary>
    public partial class AggiungiContatto : Window
    {
        Rubrica _rubrica;
        public AggiungiContatto(ref Rubrica r)
        {
            InitializeComponent();
            _rubrica = r;
           
            AggiornaDati();
            
        }
        public void AggiornaDati()
        {
            lst_contatti.Items.Add("RUBRICA");
            lst_contatti.Items.Clear();
            foreach (Contatto c in _rubrica.Contatti)
            {
                lst_contatti.Items.Add(c);
            }
        }
        public void PulisciTxt()
        {
            txt_descrizione.Text = " ";
            txt_username.Text = " ";
        }

        private void btn_aggiungiContatto_Click(object sender, RoutedEventArgs e)
        {
            //aggiunge un conttato ad una lista 
            string indirizzo = "127.0.0.1";
            string username = txt_username.Text;
            string descrizione = txt_descrizione.Text;

            if (string.IsNullOrEmpty(txt_descrizione.Text))
            {
                Contatto c = new Contatto(username, indirizzo);
                _rubrica.AggiungiContatto(c);
                AggiornaDati();
            }
            else
            {
                Contatto c = new Contatto(username, indirizzo, descrizione);
                _rubrica.AggiungiContatto(c);
                 AggiornaDati();
            }
            MessageBox.Show("Contatto aggiunto con successo");
            PulisciTxt();
         

        }

        private void ChiudiFinestra(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Socket_4I
{
    /// <summary>
    /// 
    ///APPLICATIVO SOCKET SOFIA VIARANI 
    ///FUNZIONALITA'
    ///RUBRICA
    ///CHAT BROADCAST
    ///THREAD
    /// </summary>
    public partial class MainWindow : Window
    {

        Socket socket = null;
        DispatcherTimer dTimer = null;
        Rubrica rubrica;
        public MainWindow()
        {
            InitializeComponent();
            //inizializzo la classe rubrica che all'interno contiene un alista di contatti 
            rubrica = new Rubrica();
            DisattivaElementiIniziali();
            //creo unnuovo oggett socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //ip address any è una costante che viene utilizzata per indicare che il socket deve associarsi a tutte le interfacce di rete disponibili sulla macchina locale.
            IPAddress local_address = IPAddress.Any;
            IPEndPoint local_endpoint = new IPEndPoint(local_address, 10000);
            //associo  il socket all'endpoint locale specificato
            socket.Bind(local_endpoint);
            // con la proprietà in true il socket è abilitato a inviare pacchetti di broadcast sulla rete
            socket.EnableBroadcast = true;
            //  AVVIO LA TASK
            Task.Run(aggiornamento_dTimer);

            rdbt_chatbroadcast.Visibility = Visibility.Hidden;
            Contatto c = new Contatto("PC1", "127.0.0.1", "prova");
            rubrica.AggiungiContatto(c);
            AggiornaDati();
        }
        public void DisattivaElementiIniziali()
        {
            lbl_username_.Visibility = Visibility.Hidden;
    
        }
        public void AttivaElementi()
        {
            lbl_username_.Visibility = Visibility.Visible;
        }
        public void AggiornaDati()
        {
            foreach (Contatto c in rubrica.Contatti)
            {

                lst_rubrica.Items.Add(c);
            }
        }


        private Task aggiornamento_dTimer()
        {


            while(true)//serve ad avere un ciclo continuo di ricezione dei messsaggi 
            {
                int nBytes = 0;
                //ricezione dei caratteri in attesa
                byte[] buffer = new byte[1024];

                EndPoint remoreEndPoint = new IPEndPoint(IPAddress.Any, 0);

                nBytes = socket.ReceiveFrom(buffer, ref remoreEndPoint);

                string from = ((IPEndPoint)remoreEndPoint).Address.ToString();

                string messaggio = Encoding.UTF8.GetString(buffer, 0, nBytes);


                lstMessaggi.Items.Dispatcher.Invoke(() => lstMessaggi.Items.Add(from + ": " + messaggio));

            }
            return Task.CompletedTask;
        }

        private void btnInvia_Click(object sender, RoutedEventArgs e)
        {
            //invia il mio messaggio 
            IPAddress remote_address = IPAddress.Parse(txtTo.Text);
            //restituisce  l'endpoint remoto che ha inviato i dati
            IPEndPoint remote_endpoint = new IPEndPoint(remote_address, 10000);
            //decodifica i byte ricevuti nel messaggio di testo originale e lo memorizza nella stringa messaggio
            byte[] messaggio = Encoding.UTF8.GetBytes(txtMessaggio.Text);

            socket.SendTo(messaggio, remote_endpoint);
        }

        private void btn_aggiungiContatto_Click(object sender, RoutedEventArgs e)
        {
            AggiungiContatto ag = new AggiungiContatto(ref rubrica);
            ag.ShowDialog();
            AggiornaDati();
        }

        private void lst_rubrica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AttivaElementi();
            Contatto c = lst_rubrica.SelectedItem as Contatto;
            lbl_username_.Content = c.Username;
            txtTo.Text = c.Indirizzo;
            DateTime dataChat = DateTime.Now;

            lbl_descrizione.Content = "Ore: " + dataChat.ToString() +Environment.NewLine+ "Descrizione: "+c.Descrizione;

        }

        private void rdbt_chatbroadcast_Checked(object sender, RoutedEventArgs e)
        {
            //per ogni contatto nella lista rubrica  invio il messggio 
            foreach(Contatto c in rubrica.Contatti)
            {
                IPAddress remote_address = IPAddress.Parse(txtTo.Text);

                IPEndPoint remote_endpoint = new IPEndPoint(remote_address, 11000);

                byte[] messaggio = Encoding.UTF8.GetBytes(txtMessaggio.Text);

                socket.SendTo(messaggio, remote_endpoint);
            }
        }

        private void txtMessaggio_TextChanged(object sender, TextChangedEventArgs e)
        {

            rdbt_chatbroadcast.Visibility = Visibility.Visible;
        }
    }

}

///Anneli Idnert
///TGSYS 2013
///2015-01-13
///Threads assigment 5, ChatProgram


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace Chattprogram
{
    public partial class ChatClient : Form
    {
        private TcpClient client;            ///Making it possible to conect to a server
        private NetworkStream stream;        /// Open up to send messages
        private BinaryReader reader;         ///Reading the different messages from the server
        private BinaryWriter writer;        ///sending to different serves
        private string response;            /// String response
        private delegate void display(string u);   /// a deligate to be able to send a messages
        

        public ChatClient()
        {
            InitializeComponent();
            Task tsk = new Task(Run);         ///Starting a threadpool
            tsk.Start();
            
        }
        /// <summary>
        /// sending the response to server
        /// </summary>
        public void Run()
        {
            client = new TcpClient();        
            client.Connect("127.0.0.1", 50000);   ///Server conections
            stream = client.GetStream(); ///Open the stream
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            while (true)
            {
                try
                {
                    response = reader.ReadString();          /// the response sends throught a deligate
                    lisConversation.Invoke(new display((i) => lisConversation.Items.Add(i)), new object[] { response }); 
                }
                catch 
                {
                    
                    
                }
            }
        }
                   /// <summary>
                   /// Button to write the input to the server and other clients
                   /// </summary>
                   /// <param name="sender"></param>
                   /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            writer.Write(txt_input.Text);
            
        }


    }
}

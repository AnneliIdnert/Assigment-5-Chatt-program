///Anneli Idnert
///TGSYS 2013
///2015-01-13
///Threads assigment 5, ChatServer
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerChat
{
    public partial class Form1 : Form
    {
        private TcpListener listner;     /// listner makes the server to conect with the clients
        private List<User> users = new List<User>();      ///list of users
        private Socket socket;                        ///sockets are used to give acsses throu right port
        private delegate void display(string u);
        

        public Form1()
        {
            InitializeComponent();
            Task tskSetUp = new Task(SetUp);      ///new threadpool setup
            tskSetUp.Start();
        }
        /// <summary>
        /// Starting a connection for a chat
        /// </summary>
        public void SetUp()
        {
         listner = new TcpListener(IPAddress.Parse("127.0.0.1"), 50000);   ///making the client to connect to the server
         listner.Start();
         Displaymsg("Waiting for conection...");
         while (true)
         {
             socket = listner.AcceptSocket();     ///making the server to accept the sockets
             Displaymsg("Client conected!");
             User u = new User(socket, this );    ///user are a list of users, this is instead for Form1
             Task tsk = new Task(u.Run);          /// Starting a thread pool
             tsk.Start();
             users.Add(u);                        ///Adding the users to the chat
              
         }
        }
            /// <summary>
            /// Sending in the message   with a deligate expression
            /// </summary>
            /// <param name="msg"></param>
        public void Displaymsg(string msg)
        {
            chatDialog.Invoke(new display((i) => chatDialog.Items.Add(i)), new object[] { msg }); 
        }
           /// <summary>
           /// Button to send the message to the server
           /// </summary>
           /// <param name="sender"></param>
           /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
           foreach (User u in users)
           {
               u.SendMesage(txtSendingText.Text);     ///Sending the text to the clients
               Displaymsg(txtSendingText.Text);     ///showing the instans text in the listbox
               txtSendingText.Clear();       ///Clearing the text input
           }
        }
    }
}

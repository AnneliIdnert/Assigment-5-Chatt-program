///Anneli Idnert
///TGSYS 2013
///2015-01-13
///Threads assigment 5, ChatServer
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ServerChat
{
    class User
    {

        private NetworkStream stream;  ///Starting the Stream to make it possble to send text
        private BinaryReader reader;   ///Reading possibilities
        private BinaryWriter writer;   /// writing possibiliteis
        private Form1 frm;             /// form display to show the text in the main from
        private string response;        ///Texts strings
       public User( Socket mySocket, Form1 frm)    ///sending the socket and the form in the constructor
        {
            stream = new NetworkStream(mySocket);
            this.frm = frm;
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
        }
           /// <summary>
           /// Sending in the mesage from clients
           /// </summary>
        public void Run ()
        {
           
          

            while (true)
            {
                try
                {
                    response = reader.ReadString();      ///Reading the response from the client
                    frm.Displaymsg(response);         ///Showing the response in the client
                }
                catch 
                {
                 
                }
            }
        }
               /// <summary>
               /// Transfering the message
               /// </summary>
               /// <param name="msg"></param>
          public void SendMesage(string msg)
          {
              writer.Write(msg);              ///sending a writen message
          }

        

    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Client
{
    public class ClientConnection
    {
        public TcpClient Client { get; private set; }
        private StreamReader reader;
        private StreamWriter writer;
        public event Action<string> MessageReceived;

        // Connect to the server
        public bool Connect(string serverIp, int port)
        {
            try
            {
                Client = new TcpClient(serverIp, port);
                reader = new StreamReader(Client.GetStream());
                writer = new StreamWriter(Client.GetStream()) { AutoFlush = true };
                StartListening();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Send message to the server
        public void SendMessage(string message)
        {
            if (Client.Connected && writer != null)
            {
                writer.WriteLine(message);
            }
        }

        // Disconnect from the server
        public void Disconnect()
        {
            Client.Close();
        }

        // Start listening for server messages
        private async void StartListening()
        {
            try
            {
                while (Client.Connected)
                {
                    string message = await reader.ReadLineAsync();
                    if (message != null)
                    {
                        MessageReceived?.Invoke(message);
                    }
                }
            }
            catch
            {
                Disconnect();
            }
        }

    }
}

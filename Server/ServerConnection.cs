using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    public class ServerConnection
    {
        private TcpListener listener;
        private List<TcpClient> connectedClients;
        public event Action<string> ClientConnected;
        public event Action<string> MessageReceived;

        public ServerConnection(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            connectedClients = new List<TcpClient>();
        }

        // Start the server and listen for incoming connections
        public void Start()
        {
            listener.Start();
            AcceptClients();
        }

        // Stop the server and disconnect all clients
        public void Stop()
        {
            foreach (var client in connectedClients)
            {
                client.Close();
            }
            connectedClients.Clear();
            listener.Stop();
        }

        // Accept incoming clients asynchronously
        private async void AcceptClients()
        {
            while (true)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    connectedClients.Add(client);

                    // Notify the UI about the new connection
                    string clientInfo = ((IPEndPoint)client.Client.RemoteEndPoint).ToString();
                    ClientConnected?.Invoke(clientInfo);

                    // Start listening for messages from the client
                    ListenForMessages(client);
                }
                catch (Exception)
                {
                    // Handle client acceptance errors or shutdown
                    break;
                }
            }
        }

        // Listen for incoming messages from clients
        private async void ListenForMessages(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());

            while (client.Connected)
            {
                try
                {
                    string message = await reader.ReadLineAsync();
                    if (message != null)
                    {
                        // Broadcast the message to other clients
                        BroadcastMessage($"Client: {message}", client);
                        MessageReceived?.Invoke(message);
                    }
                }
                catch
                {
                    // Disconnect client if there is an error
                    DisconnectClient(client);
                    break;
                }
            }
        }

        // Broadcast message to all connected clients
        public void BroadcastMessage(string message, TcpClient sender)
        {
            foreach (var client in connectedClients)
            {
                if (client != sender && client.Connected)
                {
                    StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                    writer.WriteLine(message);
                }
            }
        }

        // Disconnect a specific client
        public void DisconnectClient(TcpClient client)
        {
            if (connectedClients.Contains(client))
            {
                connectedClients.Remove(client);
                client.Close();
            }
        }

        // Get the list of connected clients (for Form2)
        public List<TcpClient> GetConnectedClients()
        {
            return connectedClients;
        }
    }
}

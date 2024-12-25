using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class ServerConnection
    {
        private TcpListener tcpListener;
        private bool isRunning;
        private List<TcpClient> clients;
        private Dictionary<TcpClient, int> clientIndices; // Mapping of TcpClient to index

        public event Action<int, bool> ClientStatusChanged;
        public event Action<int, string> MessageReceived; // Event to notify Form3 about received messages

        public ServerConnection()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8080;
            tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            isRunning = false;
            clients = new List<TcpClient>();
            clientIndices = new Dictionary<TcpClient, int>();
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                tcpListener.Start();
                Thread acceptThread = new Thread(AcceptClients);
                acceptThread.Start();
                Console.WriteLine("Server started.");
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                tcpListener.Stop();
                foreach (var client in clients)
                {
                    client.Close();
                }
                clients.Clear();
                clientIndices.Clear();
                Console.WriteLine("Server stopped.");
            }
        }

        private void AcceptClients()
        {
            int clientIndex = 0;  // Initial index for clients

            while (isRunning)
            {
                try
                {
                    TcpClient client = tcpListener.AcceptTcpClient();  // Blocking call
                    clients.Add(client);
                    clientIndices.Add(client, clientIndex);  // Map client to index

                    Console.WriteLine($"Client connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

                    ClientStatusChanged?.Invoke(clientIndex, true);  // Notify Form2

                    Thread clientThread = new Thread(() => HandleClient(client, clientIndex));
                    clientThread.Start();

                    clientIndex++;
                }
                catch (SocketException ex)
                {
                    if (isRunning)
                    {
                        // Log if the server is running but still encountered an error
                        Console.WriteLine($"SocketException: {ex.Message}");
                    }
                    else
                    {
                        // If the server is not running, exit the loop (graceful shutdown)
                        Console.WriteLine("Server stopped, exiting AcceptClients loop.");
                        break;
                    }
                }
            }
        }


        private void HandleClient(TcpClient client, int clientIndex)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    if (bytesRead < 4) continue;

                    string messageType = Encoding.ASCII.GetString(buffer, 0, 4);
                    string messageContent = Encoding.ASCII.GetString(buffer, 4, bytesRead - 4);

                    if (messageType == "TEXT")
                    {
                        Console.WriteLine($"Message received from Client {clientIndex + 1}: {messageContent}");

                        // Trigger MessageReceived event to notify Form3
                        MessageReceived?.Invoke(clientIndex, messageContent);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally
            {
                clients.Remove(client);
                clientIndices.Remove(client);
                client.Close();
                Console.WriteLine("Client disconnected.");

                // Trigger client status update on Form2
                ClientStatusChanged?.Invoke(clientIndex, false);
            }
        }

        public List<string> GetConnectedClients()
        {
            List<string> clientList = new List<string>();
            foreach (var kvp in clientIndices)
            {
                clientList.Add($"Client {kvp.Value + 1}");
            }
            return clientList;
        }

        public void BroadcastMessage(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes("TEXT" + message);
            foreach (var client in clients)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending message to client: {ex.Message}");
                }
            }
        }

        public void SendMessageToClient(int clientIndex, string message)
        {
            foreach (var kvp in clientIndices)
            {
                if (kvp.Value == clientIndex)
                {
                    try
                    {
                        NetworkStream stream = kvp.Key.GetStream();
                        byte[] buffer = Encoding.ASCII.GetBytes("TEXT" + message);
                        stream.Write(buffer, 0, buffer.Length);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending message to client: {ex.Message}");
                    }
                }
            }
        }

    }
}

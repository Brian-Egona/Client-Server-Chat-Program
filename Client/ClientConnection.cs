using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    public class ClientConnection
    {
        private TcpClient client;
        private string serverAddress;
        private int port;
        private NetworkStream stream;
        private Thread receiveThread;

        public event Action<string> MessageReceived; // Event to notify the UI about received messages

        public ClientConnection(string serverAddress, int port)
        {
            this.serverAddress = serverAddress;
            this.port = port;
        }

        public bool Connect()
        {
            try
            {
                client = new TcpClient(serverAddress, port);
                stream = client.GetStream();
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.Start();
                Console.WriteLine("Connected to server.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to server: {ex.Message}");
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (client != null)
                {
                    receiveThread?.Abort();
                    client.Close();
                    Console.WriteLine("Disconnected from server.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disconnecting from server: {ex.Message}");
            }
        }

        public bool IsConnected()
        {
            try
            {
                return client != null && client.Connected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking connection status: {ex.Message}");
                return false;
            }
        }

        public void SendMessage(string message)
        {
            if (IsConnected())
            {
                try
                {
                    NetworkStream stream = client.GetStream(); // Access the client’s network stream
                    byte[] buffer = Encoding.ASCII.GetBytes("TEXT:" + message); // Prefix with "TEXT:"
                    stream.Write(buffer, 0, buffer.Length); // Write the message to the stream
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending message to server: {ex.Message}");
                }
            }
        }


        private void ReceiveMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while (IsConnected() && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string messageType = Encoding.ASCII.GetString(buffer, 0, 4);

                    if (messageType == "TEXT")
                    {
                        string message = Encoding.ASCII.GetString(buffer, 4, bytesRead - 4);
                        MessageReceived?.Invoke(message);
                    }
                    // You can handle image or other types here as well
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving message: {ex.Message}");
            }
        }
    }
}


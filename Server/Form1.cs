using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        private Thread serverThread;

        public Form1()
        {
            InitializeComponent();
        }

        // Start Server Button Click
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Start TCP server on port 8888
                server = new TcpListener(IPAddress.Any, 8888);
                server.Start();
                MessageBox.Show("Server started successfully on port 8088.");

                // Start listening for clients on a new thread
                serverThread = new Thread(ListenForClients);
                serverThread.IsBackground = true;
                serverThread.Start();

                // Transition to the next form (Devices/Clients page)
                Form2 devicesForm = new Form2();
                devicesForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handle incoming client connections
        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                MessageBox.Show("Client connected!");
                // Handle client communication (can add another thread to manage messages)
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
    }
}

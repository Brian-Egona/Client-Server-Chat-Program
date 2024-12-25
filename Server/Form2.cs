using System;
using System.Windows.Forms;

namespace Server
{
    public partial class Form2 : Form
    {
        private ServerConnection serverConnection;

        public Form2()
        {
            InitializeComponent();
            InitializePCStatus();
            serverConnection = new ServerConnection(); // Initialize server connection
            serverConnection.ClientStatusChanged += UpdatePCStatus; // Attach the event handler
            serverConnection.Start(); // Start the server when Form2 is initialized
        }

        // Initialize PC Statuses to "Disconnected"
        private void InitializePCStatus()
        {
            listBox1.Items.Add("Client 1: Disconnected");
            listBox2.Items.Add("Client 2: Disconnected");
            listBox3.Items.Add("Client 3: Disconnected");
            listBox4.Items.Add("Client 4: Disconnected");
        }

        // Update the status of the clients
        public void UpdatePCStatus(int pcIndex, bool isConnected)
        {
            ListBox listBox = null;

            switch (pcIndex)
            {
                case 0:
                    listBox = listBox1;
                    break;
                case 1:
                    listBox = listBox2;
                    break;
                case 2:
                    listBox = listBox3;
                    break;
                case 3:
                    listBox = listBox4;
                    break;
                default:
                    return;
            }

            listBox.Invoke(new Action(() =>
            {
                listBox.Items.Clear();
                listBox.Items.Add(isConnected ? $"Client {pcIndex + 1}: Connected" : $"Client {pcIndex + 1}: Disconnected");
            }));
        }

        public void RefreshClientStatus()
        {
            // Get the list of currently connected clients
            List<string> connectedClients = serverConnection.GetConnectedClients();

            // Clear all listboxes (reset to "Disconnected")
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();

            // Initialize all clients as disconnected
            InitializePCStatus();

            // Update the connected clients in the listboxes
            foreach (string client in connectedClients)
            {
                int clientIndex = int.Parse(client.Split(' ')[1]) - 1;
                UpdatePCStatus(clientIndex, true);  // Update to "Connected" for active clients
            }
        }


        // Stop Server Button (Button 1)
        private void button1_Click(object sender, EventArgs e)
        {
            serverConnection.Stop();
            MessageBox.Show("Server stopped.");
            Application.Exit();  // Exit the application after stopping the server
        }

        // Open Chat Button (Button 2)
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 chatForm = new Form3(serverConnection, this); // Pass this Form2 instance to Form3
            chatForm.Show();
            this.Hide(); // Hide Form2 when opening Form3
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

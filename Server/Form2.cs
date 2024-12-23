using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Server
{
    public partial class Form2 : Form
    {
        private ServerConnection serverConnection;
        private ListBox[] listBoxes;
        private int clientIndex = 0;

        public Form2(ServerConnection serverConn)
        {
            InitializeComponent();
            serverConnection = serverConn;

            // Initialize listboxes into an array for scalability
            listBoxes = new ListBox[] { listBox1, listBox2, listBox3, listBox4 };

            // Subscribe to events for client connection and disconnection
            serverConnection.ClientConnected += AddClientToListBox;
            serverConnection.MessageReceived += DisplayIncomingMessage;
        }

        // Method to add connected clients to listboxes in a round-robin fashion
        private void AddClientToListBox(string clientInfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AddClientToListBox(clientInfo)));
                return;
            }

            listBoxes[clientIndex].Items.Add(clientInfo);
            clientIndex = (clientIndex + 1) % listBoxes.Length;
        }

        // Remove clients from listbox when they disconnect
        public void RemoveClientFromListBox(string clientInfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => RemoveClientFromListBox(clientInfo)));
                return;
            }

            foreach (var listBox in listBoxes)
            {
                if (listBox.Items.Contains(clientInfo))
                {
                    listBox.Items.Remove(clientInfo);
                    break;
                }
            }
        }

        // Stop Server Button
        private void button1_Click(object sender, EventArgs e)
        {
            serverConnection.Stop();
            MessageBox.Show("Server stopped. All clients disconnected.");
            Application.Exit();
        }

        // Open Chat (Form3) Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (serverConnection.GetConnectedClients().Count == 0)
            {
                MessageBox.Show("No clients connected. Please wait for clients to connect.", "Info");
                return;
            }

            // Open Form3 and pass the serverConnection for chat handling
            Form3 chatForm = new Form3(serverConnection);
            chatForm.Show();
            this.Hide();  // Hide Form2
        }

        // Handle incoming messages (optional display in listbox)
        private void DisplayIncomingMessage(string message)
        {
            // Future enhancement: Display incoming messages in one of the listboxes
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Placeholder for listbox item click logic (optional)
        }
    }
}

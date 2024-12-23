using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form2 : Form
    {
        private List<TcpClient> connectedClients = new List<TcpClient>();
        private ListBox[] listBoxes;
        private int clientIndex = 0;

        public Form2()
        {
            InitializeComponent();

            // Initialize listboxes into an array for scalability
            listBoxes = new ListBox[] { listBox1, listBox2, listBox3, listBox4 };
        }

        // Method to add connected clients to listboxes in a round-robin fashion
        public void AddClient(TcpClient client)
        {
            connectedClients.Add(client);

            // Get client info (IP and port)
            string clientInfo = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).ToString();

            // Add client to the next listbox
            listBoxes[clientIndex].Items.Add(clientInfo);

            // Increment index to the next listbox
            clientIndex = (clientIndex + 1) % listBoxes.Length;
        }

        // Remove clients when they disconnect
        public void RemoveClient(TcpClient client)
        {
            string clientInfo = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).ToString();

            foreach (var listBox in listBoxes)
            {
                if (listBox.Items.Contains(clientInfo))
                {
                    listBox.Items.Remove(clientInfo);
                    break;
                }
            }
            connectedClients.Remove(client);
        }

        //stop server button
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Open chat(form 3)
        private void button2_Click(object sender, EventArgs e)
        {
            if (connectedClients.Count == 0)
            {
                MessageBox.Show("No clients connected. Please wait for clients to connect.", "Info");
                return;
            }

            // Pass the list of connected clients to Form3
            Form3 chatForm = new Form3(connectedClients);
            chatForm.Show();
            this.Hide();  // Hide Form2
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

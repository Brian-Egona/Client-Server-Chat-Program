using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class Form3 : Form
    {
        private List<TcpClient> connectedClients;  // Store connected clients
        private TcpClient selectedClient;          // Client selected from combobox
        private NetworkStream stream;              // Stream for sending messages

        public Form3(List<TcpClient> clients)
        {
            InitializeComponent();
            connectedClients = clients;
            PopulateClientsComboBox();
        }

        // Populate ComboBox with connected clients
        private void PopulateClientsComboBox()
        {
            comboBox1.Items.Clear();
            foreach (var client in connectedClients)
            {
                string clientInfo = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).ToString();
                comboBox1.Items.Add(clientInfo);
            }
        }

        // When client is selected from ComboBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedClientInfo = comboBox1.SelectedItem.ToString();

            // Find the selected client by IP and Port
            selectedClient = connectedClients.Find(client =>
                ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).ToString() == selectedClientInfo
            );

            if (selectedClient != null)
            {
                stream = selectedClient.GetStream();
                AppendText($"Chatting with: {selectedClientInfo}");
            }
        }

        // Send message to selected client
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (selectedClient != null && stream != null)
            {
                string message = textBox1.Text;
                byte[] buffer = Encoding.ASCII.GetBytes($"Server: {message}");

                stream.Write(buffer, 0, buffer.Length);
                AppendText($"Me: {message}");
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please select a client to send a message.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Append messages to richTextBox
        private void AppendText(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { AppendText(message); }));
            }
            else
            {
                richTextBox1.AppendText(message + Environment.NewLine);
            }
        }

        // Stop the server and disconnect clients
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var client in connectedClients)
            {
                client.Close();
            }
            MessageBox.Show("Server stopped. Clients disconnected.");
            Application.Exit();
        }

        // Close chatbox and return to Form2
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

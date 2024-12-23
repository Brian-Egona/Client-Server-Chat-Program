using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class Form3 : Form
    {
        private ServerConnection serverConnection;  // Use ServerConnection to manage clients
        private TcpClient selectedClient;           // Client selected from ComboBox
        private NetworkStream stream;

        public Form3(ServerConnection serverConn)
        {
            InitializeComponent();
            serverConnection = serverConn;

            // Populate ComboBox when Form3 is loaded
            PopulateClientsComboBox();

            // Subscribe to message received event
            serverConnection.MessageReceived += DisplayIncomingMessage;
            serverConnection.ClientConnected += UpdateClientList;
        }

        // Populate ComboBox with connected clients
        private void PopulateClientsComboBox()
        {
            comboBox1.Items.Clear();
            foreach (var client in serverConnection.GetConnectedClients())
            {
                string clientInfo = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).ToString();
                comboBox1.Items.Add(clientInfo);
            }
        }

        // Update ComboBox when a new client connects
        private void UpdateClientList(string clientInfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateClientList(clientInfo)));
                return;
            }

            comboBox1.Items.Add(clientInfo);
        }

        // When a client is selected from ComboBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedClientInfo = comboBox1.SelectedItem.ToString();

            selectedClient = serverConnection.GetConnectedClients().Find(client =>
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

        // Display incoming messages from clients
        private void DisplayIncomingMessage(string message)
        {
            AppendText($"Client: {message}");
        }

        // Append messages to the RichTextBox
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

        // Stop the server and disconnect all clients
        private void button1_Click(object sender, EventArgs e)
        {
            serverConnection.Stop();
            MessageBox.Show("Server stopped. Clients disconnected.");
            Application.Exit();
        }

        // Close chatbox and return to Form2
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2(serverConnection);
            form2.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

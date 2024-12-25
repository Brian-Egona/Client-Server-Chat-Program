

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Server
{
    public partial class Form3 : Form
    {
        private ServerConnection serverConnection;
        private Form2 form2; // Reference to Form2
        private string placeholderText = "Enter a Message";

        public Form3(ServerConnection serverConnection, Form2 form2)
        {
            InitializeComponent();
            this.serverConnection = serverConnection;
            this.form2 = form2;

            InitializeComboBox();

            // Subscribe to message events from clients
            this.serverConnection.MessageReceived += OnMessageReceived;

            // Set placeholder initially
            SetPlaceholder();

            // Attach Enter and Leave events for the placeholder
            textBox1.Enter += RemovePlaceholder;
            textBox1.Leave += SetPlaceholder;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Load connected clients into combobox when the form loads
            LoadConnectedClients();
        }

        // Initialize ComboBox options
        private void InitializeComboBox()
        {
            comboBox1.Items.Add("Send to");
            comboBox1.Items.Add("All");  // Broadcast option
            comboBox1.SelectedIndex = 0;
        }

        // Refresh connected clients in ComboBox
        private void LoadConnectedClients()
        {
            List<string> connectedClients = serverConnection.GetConnectedClients();
            comboBox1.Items.Clear();
            InitializeComboBox();
            comboBox1.Items.AddRange(connectedClients.ToArray());
        }

        // Append received messages from clients to RichTextBox
        private void OnMessageReceived(int clientIndex, string message)
        {
            richTextBox1.Invoke(new Action(() =>
            {
                richTextBox1.AppendText($"Client {clientIndex} {message}{Environment.NewLine}");
            }));
        }

        // Set the placeholder text
        private void SetPlaceholder(object sender = null, EventArgs e = null)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = placeholderText;
                textBox1.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // Remove placeholder text when user clicks inside the TextBox
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (textBox1.Text == placeholderText)
            {
                textBox1.Text = "";
                textBox1.ForeColor = System.Drawing.Color.Black;
            }
        }
        // Send message button (Text)
        private void button4_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text;
            string selectedClient = comboBox1.SelectedItem.ToString();

            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a client to send the message.");
                return;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter a message before sending.");
                return;
            }

            // Sending to all clients
            if (selectedClient == "All")
            {
                serverConnection.BroadcastMessage(message);
                AppendToChatHistory($"Server (to All): {message}");
            }
            else
            {
                // Sending to a specific client
                int clientIndex = int.Parse(selectedClient.Split(' ')[1]) - 1;
                serverConnection.SendMessageToClient(clientIndex, message);
                AppendToChatHistory($"Server (to {selectedClient}): {message}");
            }

            textBox1.Clear();
        }

        // Add attachment button (Send Image)
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.gif;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string selectedClient = comboBox1.SelectedItem.ToString();

                if (selectedClient == "All")
                {
                    serverConnection.BroadcastFile(fileBytes);
                    AppendToChatHistory($"Server (to All): Sent an image");
                }
                else
                {
                    int clientIndex = int.Parse(selectedClient.Split(' ')[1]) - 1;
                    serverConnection.SendMessageToClient(clientIndex, "Sending image...");
                    serverConnection.BroadcastFile(fileBytes);
                    AppendToChatHistory($"Server (to {selectedClient}): Sent an image");
                }
            }
        }

        // Append messages to RichTextBox (Chat history)
        private void AppendToChatHistory(string message)
        {
            richTextBox1.Invoke(new Action(() =>
            {
                richTextBox1.AppendText($"{message}{Environment.NewLine}");
            }));
        }

        // Stop Server Button
        private void button1_Click(object sender, EventArgs e)
        {
            serverConnection.Stop();
            MessageBox.Show("Server stopped.", "Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        // Close chat and return to Form2
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            form2.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

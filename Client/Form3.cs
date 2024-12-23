using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Form3 : Form
    {
        private ClientConnection connection;
        private string selectedClient;

        public Form3(ClientConnection clientConn, string client)
        {
            InitializeComponent();
            connection = clientConn;
            selectedClient = client;

            // Subscribe to the message received event
            connection.MessageReceived += DisplayIncomingMessage;
        }

        // Form Load
        private void Form3_Load(object sender, EventArgs e)
        {
            richTextBox1.AppendText($"Chatting with: {selectedClient}\n");
        }

        // Handle Message TextBox (Optional Validation)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional: Limit message length or validate input
        }

        // Send Button Click - Send message through ClientConnection
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Send message to the server using ClientConnection
                connection.SendMessage($"{selectedClient}: {textBox1.Text}");
                AppendMessage($"Me: {textBox1.Text}");
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Enter a message before sending.", "Warning");
            }
        }

        // Disconnect Button Click
        private void button2_Click(object sender, EventArgs e)
        {
            connection.Disconnect();
            MessageBox.Show("Disconnected from server.");
            Application.Exit();
        }

        // Close Chat Button (Return to Form2)
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2(connection);
            form2.Show();
        }

        // Display incoming messages in the chat display (RichTextBox)
        private void DisplayIncomingMessage(string message)
        {
            AppendMessage($"Server: {message}");
        }

        // Append message to RichTextBox
        private void AppendMessage(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => richTextBox1.AppendText(message + Environment.NewLine)));
            }
            else
            {
                richTextBox1.AppendText(message + Environment.NewLine);
            }
        }
    }
}

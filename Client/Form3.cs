using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Form3 : Form
    {
        private ClientConnection clientConnection;
        private Form2 form2;  // Reference to Form2 for navigation
        private string placeholderText = "Enter a Message";

        public Form3(ClientConnection clientConnection, Form2 form2)
        {
            InitializeComponent();
            this.clientConnection = clientConnection;
            this.form2 = form2;
            this.clientConnection.MessageReceived += OnMessageReceived;

            // Set placeholder initially
            SetPlaceholder();

            // Attach Enter and Leave events
            textBox1.Enter += RemovePlaceholder;
            textBox1.Leave += SetPlaceholder;
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

        // Remove placeholder text when the user starts typing
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (textBox1.Text == placeholderText)
            {
                textBox1.Text = "";
                textBox1.ForeColor = System.Drawing.Color.Black;
            }
        }

        // Event handler for receiving messages from the server
        private void OnMessageReceived(string message)
        {
            richTextBox1.Invoke(new Action(() =>
            {
                richTextBox1.AppendText("Server: " + message + Environment.NewLine);
            }));
        }

        // Send message button
        private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text;

            if (!string.IsNullOrWhiteSpace(message) && message != placeholderText)
            {
                clientConnection.SendMessage(message);
                richTextBox1.AppendText("You: " + message + Environment.NewLine);
                textBox1.Clear();
                SetPlaceholder();  // Reset placeholder after sending
            }
            else
            {
                MessageBox.Show("Enter a message before sending.", "Warning");
            }
        }

        // Disconnect from server button
        private void button2_Click(object sender, EventArgs e)
        {
            clientConnection.Disconnect();
            form2.UpdateServerStatus(false);  // Update Form2 status
            MessageBox.Show("Disconnected from server.", "Client");
            this.Hide();
            form2.Show();  // Return to Form2
        }

        // Close chat button
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            form2.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}

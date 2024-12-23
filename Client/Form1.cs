using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private ClientConnection client;

        public Form1()
        {
            InitializeComponent();
            client = new ClientConnection();  // Instantiate ClientConnection
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Optional: Placeholder if label is clicked (no need for logic here)
        }

        // Start Client Button Click
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Connect to the server (localhost:8888)
                bool connected = client.Connect("127.0.0.1", 8888); // Ensure port matches server
                if (connected)
                {
                    MessageBox.Show("Connected to server successfully!");

                    // Transition to Form2 (Connected Users)
                    Form2 connectedUsersForm = new Form2(client);
                    connectedUsersForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Failed to connect to server. Please check if the server is running.", 
                        "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

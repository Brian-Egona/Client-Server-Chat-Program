using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private ServerConnection serverConnection;

        public Form1()
        {
            InitializeComponent();
            serverConnection = new ServerConnection(8088);
        }

        // Start Server Button Click
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serverConnection.Start();
                MessageBox.Show("Server started on port 8088!");

                // Transition to Form2 to display connected clients
                Form2 connectedClientsForm = new Form2(serverConnection);
                connectedClientsForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
    }
}

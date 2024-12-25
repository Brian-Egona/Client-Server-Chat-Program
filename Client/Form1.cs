using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private ClientConnection clientConnection;

        public Form1()
        {
            InitializeComponent();
        }

        // Start Client Button Click
        private void button1_Click(object sender, EventArgs e)
        {
            clientConnection = new ClientConnection("127.0.0.1", 8080); // Connect to server at localhost:8080

            if (clientConnection.Connect())
            {
                MessageBox.Show("Connected to server.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Form2 and pass the clientConnection object
                Form2 form2 = new Form2(clientConnection);
                form2.Show();
                this.Hide();  // Hide Form1 after successful connection
            }
            else
            {
                MessageBox.Show("Failed to connect to server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}

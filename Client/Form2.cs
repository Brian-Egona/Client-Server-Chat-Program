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

namespace Client
{
    public partial class Form2 : Form
    {
        private ClientConnection connection;

        public Form2(ClientConnection client)
        {
            InitializeComponent();
            connection = client;
            connection.MessageReceived += DisplayConnectedUsers;

            // Enable double-click on listbox
            listBox1.DoubleClick += ListBox1_DoubleClick;
        }

        // Display connected users in the listbox
        private void DisplayConnectedUsers(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => listBox1.Items.Add(message)));
            }
            else
            {
                listBox1.Items.Add(message);
            }
        }

        // Transition to Form3 when double-clicking a user
        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedClient = listBox1.SelectedItem.ToString();

                // Transition to Form3
                Form3 chatForm = new Form3(connection, selectedClient);
                chatForm.Show();
                this.Hide();
            }
        }


        // "Select User" button fallback option
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedClient = listBox1.SelectedItem.ToString();

                // Transition to Form3
                Form3 chatForm = new Form3(connection, selectedClient);
                chatForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a user from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}

using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Form
    {
        private ClientConnection clientConnection;
        private Form3 form3;

        public Form2(ClientConnection clientConnection)
        {
            InitializeComponent();
            this.clientConnection = clientConnection;
            InitializeStatusListBox();
            this.Load += Form2_Load;
        }

        // Initialize ListBox with default status
        private void InitializeStatusListBox()
        {
            listBox1.Items.Add("Server: Disconnected");
        }

        // Load event for refreshing the list
        private void Form2_Load(object sender, EventArgs e)
        {
            UpdateServerStatus(clientConnection.IsConnected());
        }

        // Update server connection status
        public void UpdateServerStatus(bool isConnected)
        {
            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(isConnected ? "Server: Connected" : "Server: Disconnected");
            }));
        }

        // Disconnect button - Disconnect from server and return to Form1
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                MessageBox.Show($"Selected User: {listBox1.SelectedItem}", "User Selected");

                form3 = new Form3(clientConnection, this);
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a user from the list.", "Selection Required");
            }
        }

        // Open chat - Navigate to Form3 (Chat Interface)
        private void button2_Click(object sender, EventArgs e)
        {
            clientConnection.Disconnect();
            UpdateServerStatus(clientConnection.IsConnected());
            MessageBox.Show("Disconnected from server.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        //// Select User - Perform action based on selected user from the list
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    if (listBox1.SelectedItem != null)
        //    {
        //        MessageBox.Show($"Selected User: {listBox1.SelectedItem}", "User Selected");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select a user from the list.", "Selection Required");
        //    }
        //}

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}





//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Client
//{
//    public partial class Form2 : Form
//    {
//        private ClientConnection connection;

//        public Form2(ClientConnection client)
//        {
//            InitializeComponent();
//            connection = client;
//            connection.MessageReceived += DisplayConnectedUsers;

//            // Enable double-click on listbox
//            listBox1.DoubleClick += ListBox1_DoubleClick;
//        }

//        // Display connected users in the listbox
//        private void DisplayConnectedUsers(string message)
//        {
//            if (InvokeRequired)
//            {
//                this.Invoke(new Action(() => listBox1.Items.Add(message)));
//            }
//            else
//            {
//                listBox1.Items.Add(message);
//            }
//        }

//        // Transition to Form3 when double-clicking a user
//        private void ListBox1_DoubleClick(object sender, EventArgs e)
//        {
//            if (listBox1.SelectedItem != null)
//            {
//                string selectedClient = listBox1.SelectedItem.ToString();

//                // Transition to Form3
//                Form3 chatForm = new Form3(connection, selectedClient);
//                chatForm.Show();
//                this.Hide();
//            }
//        }


//        // "Select User" button fallback option
//        private void button1_Click(object sender, EventArgs e)
//        {
//            if (listBox1.SelectedItem != null)
//            {
//                string selectedClient = listBox1.SelectedItem.ToString();

//                // Transition to Form3
//                Form3 chatForm = new Form3(connection, selectedClient);
//                chatForm.Show();
//                this.Hide();
//            }
//            else
//            {
//                MessageBox.Show("Please select a user from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }
//        private void button2_Click(object sender, EventArgs e)
//        {

//        }

//        private void button3_Click(object sender, EventArgs e)
//        {

//        }

//        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
//        {

//        }


//    }
//}

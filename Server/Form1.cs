using System;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Transition to Form2 when the button is clicked
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();  // Hide Form1
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
}
}

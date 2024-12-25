namespace Client
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(134, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(654, 359);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.Gray;
            textBox1.Location = new Point(134, 393);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(340, 27);
            textBox1.TabIndex = 1;
            textBox1.Text = "Enter a Message";
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(128, 128, 255);
            button1.Location = new Point(497, 391);
            button1.Name = "button1";
            button1.Size = new Size(75, 29);
            button1.TabIndex = 2;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(192, 0, 0);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(12, 46);
            button2.Name = "button2";
            button2.Size = new Size(117, 56);
            button2.TabIndex = 3;
            button2.Text = "Disconnect from Server";
            button2.UseMnemonic = false;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(255, 128, 0);
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Location = new Point(13, 126);
            button3.Name = "button3";
            button3.Size = new Size(116, 50);
            button3.TabIndex = 4;
            button3.Text = "Close Chat";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(richTextBox1);
            Name = "Form3";
            Text = "Form3";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}
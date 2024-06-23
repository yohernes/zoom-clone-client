using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zoom_client
{
    public partial class ChatUI : Form
    {
        NetworkClient networkClient;
        public ChatUI(NetworkClient networkclient)
        {
            InitializeComponent();
            networkClient = networkclient;
        }

        private void ChatUI_Load(object sender, EventArgs e)
        {
            foreach ((string message, string name) mesdata in networkClient.chatHistory)
            {
                AddMessage(mesdata.message, mesdata.name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = richTextBox1.Text;
            if (message != "")
            {
                SendMessage(message);
                AddMessage(message, networkClient.username);
                networkClient.chatHistory.Add((message, networkClient.username));
                richTextBox1.Text = "";

            }
        }
        private void SendMessage(string message)
        {
            networkClient.SendMessage($"%%%Chat%%%{networkClient.username}#{message}");
        }
        public void AddMessage(string message, string username)
        {
            Label messageLabel = new Label();
            messageLabel.AutoSize = true;
            messageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            //messageLabel.Location = new System.Drawing.Point(15, 10);
            messageLabel.Name = "label1";
            messageLabel.Size = new System.Drawing.Size(70, 29);
            messageLabel.TabIndex = 2;
            messageLabel.Text = $"<{username}>: {message}";
            flowLayoutPanel1.Controls.Add(messageLabel);
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            meetingUI.chatopen = false;
        }
    }
}

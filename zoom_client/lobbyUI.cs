using System;
using System.Windows.Forms;

namespace zoom_client
{
    public partial class lobbyUI : Form
    {
        NetworkClient networkClient;
        string username;
        Control UI;
        public lobbyUI(NetworkClient networkClient, Control ui)
        {
            InitializeComponent();
            this.networkClient = networkClient;
            username = networkClient.username;
            textBox1.Text = "Hello, " + username;
            UI = ui;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            networkClient.SendMessage($"%%%NewMeeting%%%{username}#{networkClient.udport}");
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            if(!this.textBox2.Visible) 
            {
                textBox2.Visible = true;
                textBox2.Enabled = true;
                button4.Visible = true;
                button4.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string code = "";
            if(textBox2.Text.Length == 0)
            {
                MessageBox.Show("please enter number");
                return;
            }
            try
            {
                code = textBox2.Text;
            }
            catch
            {
                MessageBox.Show("code should be numbers only");
            }
            networkClient.SendMessage($"%%%JoinMeeting%%%{code}#{username}#{networkClient.udport}");
        }

        private void lobbyUI_Load(object sender, EventArgs e)
        {
            this.FormClosing += On_FormClosing;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UI.Show();
            this.Hide();
        }


        private void On_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close all open forms.
            Application.Exit();
        }
    }
}

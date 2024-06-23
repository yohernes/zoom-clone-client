using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;

namespace zoom_client
{
    public partial class loginUI : Form
    {
        NetworkClient NetworkClient;
        private lobbyUI lobbyUI;
        private meetingUI meetingUI;
        private ChatUI chatUI;
        Captcha captcha;
        private string username;
        private string password;
        private string email;
        private string fname;
        private string lname;
        private string country;

        public loginUI()
        {
            InitializeComponent();
         
            
            try
            {
                NetworkClient = new NetworkClient(this);
                chatUI = new ChatUI(NetworkClient);
            }
            catch
            {
                MessageBox.Show("There has been a problem with the server, please try again later");
                this.Close();
            }
            
            //data = new byte[client.ReceiveBufferSize];
            //rsa = new RSA();
            //aes = new AesEncryption();
        }
    


        private void Form1_Load_1(object sender, EventArgs e)
        {
            //NetworkClient.SendMessage("%%%clientPublicKey%%%" + rsa.GetPublicKey());
            NetworkClient.Start();
            comboBox1.DataSource = DataProcessor.countries;
        }
















        private void Registbtn_Click(object sender, EventArgs e)
        {
            username = txbusername.Text;
            password = txbpassword.Text;
            fname = txbfname.Text;
            lname = txbfname.Text;
            email = txbemail.Text;
            country = comboBox1.SelectedItem.ToString();

            if (  DataProcessor.IsUsername(username)&& DataProcessor.IsPassword(password)&&DataProcessor.IsFirstOLastName(fname)&& DataProcessor.IsFirstOLastName(lname)&& DataProcessor.IsEmail(email))
            {
               
                this.RegistToCaptcha();

            }
            //relevant mbox messages are sent through the validators



        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {

            NetworkClient.SendLogin(txbuserlogin.Text, txbpasslogin.Text);

          
        }


        public void LobbyToMeeting(string newport, string newusername, AesEncryption aes,UdpClient udpClient, string code)
        {
            meetingUI = new meetingUI(newusername, int.Parse(newport), aes, udpClient,this, code);
            meetingUI.Show();
            lobbyUI.Hide();
        }

        public void OpenChat()
        {
            chatUI = new ChatUI(NetworkClient);
            chatUI.Show();

        }
        public void ReloadChat(string message,string user) 
        {
            
            chatUI.AddMessage(message, user);
        }
        public void RegistToCaptcha()
        {
            captcha = new Captcha(this);
            captcha.ShowDialog();
        }

        public void CaptchaOK()
        {
            NetworkClient.SendRegist(username, password, fname, lname, email, country);
            captcha.Close();
        }
        public void LoginToLobby()
        {
            lobbyUI = new lobbyUI(NetworkClient, this);
            this.Hide();
            lobbyUI.Show();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}

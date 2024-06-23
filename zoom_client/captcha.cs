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
    public partial class Captcha : Form
    {
        loginUI loginUI;
        public Captcha(loginUI loginUI)
        {
            InitializeComponent();
            loadCaptchaImage();
            this.loginUI = loginUI;
        }
        string captcha = "";
        private void loadCaptchaImage()
        {
            
             captcha = GenerateNewCaptcha(6);
            var image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            var font = new Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(captcha, font, Brushes.Green, new Point(0, 0));
            pictureBox1.Image = image;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadCaptchaImage();
        }
       

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == captcha.ToString())
            {
                loginUI.CaptchaOK();
            }
            else
            {
                MessageBox.Show("Suspicious...");
            }
        }
        public string GenerateNewCaptcha(int length)
        {
            List<int> asc = new List<int>();
            for(int i = 65; i < 91; i++)//add upcase chars
            {
                asc.Add(i);
            }
            for (int i = 97; i < 123; i++)//add lowcase chars
            {
                asc.Add(i);
            }
            for (int i = 48; i < 58; i++)//add numbers 3 times because there are less numbers than chars
            {
                asc.Add(i);
                asc.Add(i);
                asc.Add(i);
            }
            string rand = "";
            Random rand2 = new Random();
            int x;
            for(int i = 0; i < length; i++)
            {
                x = rand2.Next(0, asc.Count);
                rand += (char)asc[x];
            }
            return rand;
        }
    }
}

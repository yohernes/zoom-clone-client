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
    public partial class Members : Form
    {
        public List<string> members = new List<string>();
        Font LargeFont = new Font("Arial", 15);

        public Members()
        {
            InitializeComponent();
            
        }

        private void Members_Load(object sender, EventArgs e)
        {
           
        }
        public void AddMember(string member) 
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(3, 0);
            label.Name = member;
            label.Size = new System.Drawing.Size(60, 20);
            label.Font = LargeFont;
            label.TabIndex = 1;
            label.Text = member;
            this.flowLayoutPanel1.Controls.Add(label);
            members.Add(member);
        }
        public void RemoveMember(string member)
        {
            members.Remove(member);

            flowLayoutPanel1.Controls.RemoveByKey(member);
        }
    }
}

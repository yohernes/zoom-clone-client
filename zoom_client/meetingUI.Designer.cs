namespace zoom_client
{
    partial class meetingUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(meetingUI));
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.CboCamera = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Pic = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chat = new System.Windows.Forms.Button();
            this.sharescreen = new System.Windows.Forms.Button();
            this.members = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.CamButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox2.Location = new System.Drawing.Point(595, 80);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(159, 22);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "meeting code: ";
            // 
            // CboCamera
            // 
            this.CboCamera.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CboCamera.FormattingEnabled = true;
            this.CboCamera.Location = new System.Drawing.Point(450, 18);
            this.CboCamera.Margin = new System.Windows.Forms.Padding(4);
            this.CboCamera.Name = "CboCamera";
            this.CboCamera.Size = new System.Drawing.Size(21, 24);
            this.CboCamera.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.Pic);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(190, 107);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(981, 167);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // Pic
            // 
            this.Pic.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Pic.Image = global::zoom_client.Properties.Resources.profile;
            this.Pic.Location = new System.Drawing.Point(4, 4);
            this.Pic.Margin = new System.Windows.Forms.Padding(4);
            this.Pic.Name = "Pic";
            this.Pic.Size = new System.Drawing.Size(549, 357);
            this.Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic.TabIndex = 1;
            this.Pic.TabStop = false;
            this.Pic.Click += new System.EventHandler(this.Pic_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chat);
            this.panel1.Controls.Add(this.sharescreen);
            this.panel1.Controls.Add(this.members);
            this.panel1.Controls.Add(this.ExitButton);
            this.panel1.Controls.Add(this.CboCamera);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.CamButton);
            this.panel1.Location = new System.Drawing.Point(87, 295);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1155, 100);
            this.panel1.TabIndex = 13;
            // 
            // chat
            // 
            this.chat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chat.BackColor = System.Drawing.SystemColors.ControlText;
            this.chat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chat.Image = global::zoom_client.Properties.Resources._4213404_bubble_chat_communication_conversation_launcher_icon;
            this.chat.Location = new System.Drawing.Point(782, 16);
            this.chat.Margin = new System.Windows.Forms.Padding(4);
            this.chat.Name = "chat";
            this.chat.Size = new System.Drawing.Size(97, 80);
            this.chat.TabIndex = 12;
            this.chat.Text = "chat";
            this.chat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chat.UseVisualStyleBackColor = false;
            this.chat.Click += new System.EventHandler(this.chat_Click);
            // 
            // sharescreen
            // 
            this.sharescreen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.sharescreen.BackColor = System.Drawing.SystemColors.WindowText;
            this.sharescreen.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sharescreen.Image = global::zoom_client.Properties.Resources._8687765_ic_fluent_share_screen_start_icon;
            this.sharescreen.Location = new System.Drawing.Point(479, 16);
            this.sharescreen.Margin = new System.Windows.Forms.Padding(4);
            this.sharescreen.Name = "sharescreen";
            this.sharescreen.Size = new System.Drawing.Size(190, 80);
            this.sharescreen.TabIndex = 5;
            this.sharescreen.Text = "share screen";
            this.sharescreen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.sharescreen.UseVisualStyleBackColor = false;
            this.sharescreen.Click += new System.EventHandler(this.sharescreen_Click);
            // 
            // members
            // 
            this.members.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.members.BackColor = System.Drawing.SystemColors.ControlText;
            this.members.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.members.Image = global::zoom_client.Properties.Resources.icons8_people_50;
            this.members.Location = new System.Drawing.Point(677, 17);
            this.members.Margin = new System.Windows.Forms.Padding(4);
            this.members.Name = "members";
            this.members.Size = new System.Drawing.Size(97, 80);
            this.members.TabIndex = 6;
            this.members.Text = "members";
            this.members.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.members.UseVisualStyleBackColor = false;
            this.members.Click += new System.EventHandler(this.members_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ExitButton.BackColor = System.Drawing.SystemColors.ControlText;
            this.ExitButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ExitButton.Image = global::zoom_client.Properties.Resources._352328_app_exit_to_icon__1_;
            this.ExitButton.Location = new System.Drawing.Point(887, 16);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(117, 80);
            this.ExitButton.TabIndex = 8;
            this.ExitButton.Text = "exit";
            this.ExitButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Image = global::zoom_client.Properties.Resources.icons8_no_microphone_70;
            this.button1.Location = new System.Drawing.Point(150, 16);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 79);
            this.button1.TabIndex = 3;
            this.button1.Text = "unmute";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // CamButton
            // 
            this.CamButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CamButton.BackColor = System.Drawing.Color.Black;
            this.CamButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CamButton.Image = global::zoom_client.Properties.Resources.no_camera;
            this.CamButton.Location = new System.Drawing.Point(329, 16);
            this.CamButton.Margin = new System.Windows.Forms.Padding(4);
            this.CamButton.Name = "CamButton";
            this.CamButton.Size = new System.Drawing.Size(142, 80);
            this.CamButton.TabIndex = 4;
            this.CamButton.Text = "Show camera";
            this.CamButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CamButton.UseVisualStyleBackColor = false;
            this.CamButton.Click += new System.EventHandler(this.CamButton_Click);
            // 
            // meetingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1353, 436);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.textBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "meetingUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Syncro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox Pic;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button CamButton;
        private System.Windows.Forms.Button sharescreen;
        private System.Windows.Forms.Button members;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ComboBox CboCamera;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button chat;
    }
}
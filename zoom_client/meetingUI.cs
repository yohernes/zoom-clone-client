using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Net;

using System.Drawing.Imaging;
using NAudio.Wave;
using zoom_client.Properties;

namespace zoom_client
{
    public partial class meetingUI : Form
    {
        readonly string serverIp = NetworkClient.ipAddress;
        AesEncryption aes;
        string username;
        Task frames;
        Task receive;
        Task screen;
        UdpClient udpClient;
        IPEndPoint serverEndPoint;
        public static bool sendFrames = false;
        public static bool sendScreen = false;
        public static bool chatopen = false;
        loginUI UI;
        Members membersUI;
        private bool keepReceive = true;
        
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        FilterInfoCollection filterInfocollection;
        VideoCaptureDevice videoCaptureDevice;
        public static Bitmap currentFrame;


        private WaveInEvent waveIn;
        private WaveOutEvent waveOut;
        private BufferedWaveProvider bufferedWaveProvider;
        //UdpNetwork udpNetwork;
        //private WaveOutEvent waveOut;
        public meetingUI(string username, int port, AesEncryption aes,UdpClient udpClient, loginUI ui, string meetingcode)
        {

            InitializeComponent();
            InitializeAudioCapture();
            InitializeAudioPlayback();            //udpNetwork = new UdpNetwork(username,port,aes);
            this.username = username;
            this.aes = aes;
            this.udpClient = udpClient;
            UI = ui;
            membersUI = new Members();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), port);
            udpClient.Connect(serverEndPoint);

            textBox2.Text = $"meeting code: {meetingcode}";
           
            filterInfocollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterinfo in filterInfocollection)
            {
                CboCamera.Items.Add(filterinfo.Name);
                CboCamera.SelectedIndex = 0;
            }
            videoCaptureDevice = new VideoCaptureDevice();
            
        }

        //Thread stream1 = new Thread(ImageSender.StreamFr);

       
        private void Form2_Load(object sender, EventArgs e)
        {
            frames = new Task(() => SendFrames());
            frames.Start();
            receive = new Task(() => Startreceive());
            receive.Start();
            screen = new Task(() => SendScreen());
            screen.Start();

            //this.flowLayoutPanel1.Controls.Add(Pic);
            //this.FormClosing += On_FormClosing;

            membersUI.AddMember(username);
            WriteOnPicture(username, Pic);
        }




        #region UI

        private void chat_Click(object sender, EventArgs e)
        {
            if (!chatopen)
            {
                UI.OpenChat();

                chatopen = true;
            }

        }

        private void sharescreen_Click(object sender, EventArgs e)
        {
            if (sharescreen.Text == "stop share")
            {

                sharescreen.Text = "share screen";


                sendScreen = false;
                sharescreen.Image = Resources._8687765_ic_fluent_share_screen_start_icon;
                Pic.Image = Resources.profile;
                WriteOnPicture(username, Pic);
                SendMessage("%%%StopCamera%%%|");



            }
            else
            {
                try
                {
                    if (sendFrames)
                    {
                        CamButton_Click(sender, e);

                    }
                    sendScreen = true;

                    sharescreen.Image = Resources.share_screen_stop_regular_icon_203292;
                    sharescreen.Text = "stop share";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //stream1.Start();
                //t2.Start();


            }



        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "mute")
            {
                button1.Text = "unmute";
                waveIn.StopRecording();
                waveIn.Dispose();
                button1.Image = Properties.Resources.icons8_no_microphone_70;

            }
            else
            {
                button1.Text = "mute";
                button1.Image = Properties.Resources.microphone;
                waveIn.StartRecording();


            }
        }


        private void CamButton_Click(object sender, EventArgs e)
        {
            if (CamButton.Text == "hide camera")
            {

                CamButton.Text = "Show camera";



                CamButton.Image = Properties.Resources.no_camera;
                SendMessage("%%%StopCamera%%%|");

                StopFrames();

            }
            else
            {
                try
                {
                    CamButton.Text = "hide camera";
                    videoCaptureDevice = new VideoCaptureDevice(filterInfocollection[CboCamera.SelectedIndex].MonikerString);
                    videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                    videoCaptureDevice.Start();
                    sendFrames = true;
                    CamButton.Image = Properties.Resources.camera;
                    if (sendScreen)
                    {
                        sharescreen_Click(sender, e);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //stream1.Start();
                //t2.Start();

            }
        }

        private void Pic_Click(object sender, EventArgs e)
        {

        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            //keepReceive = false;
            //sendFrames = false;
            //StopFrames();
            //UI.MeetingToLobby();
            this.Close();
        }
        private void StopFrames()
        {
            sendFrames = false;
            currentFrame = null;
            videoCaptureDevice.Stop();
            Pic.Image = Properties.Resources.profile;
            WriteOnPicture(username, Pic);
            CamButton.Text = "Show camera";
        }
        public void AddGuest(string username)
        {
            System.Windows.Forms.PictureBox NewPicture = new System.Windows.Forms.PictureBox();

            NewPicture.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            NewPicture.Image = global::zoom_client.Properties.Resources.profile;
            NewPicture.Location = new System.Drawing.Point(300, 64);
            NewPicture.Margin = new System.Windows.Forms.Padding(4);
            NewPicture.Name = "Pic";
            NewPicture.Size = new System.Drawing.Size(400, 300);
            NewPicture.TabIndex = 1;
            NewPicture.TabStop = false;
            NewPicture.Click += new System.EventHandler(this.Pic_Click);
            NewPicture.Tag = username;
            NewPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            WriteOnPicture(username, NewPicture);
            pictureBoxes.Add(NewPicture);

            this.flowLayoutPanel1.Controls.Add(NewPicture);

        }
       
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Send close notification to the server
            /*    SendCloseNotification()*/
            ;
            SendMessage("%%%Closing%%%|");

            // Ensure proper disposal
            udpClient.Close();
            Task.Delay(100).Wait();
            Application.Exit();
        }
        private void members_Click(object sender, EventArgs e)
        {
            try
            {
                membersUI.ShowDialog();
            }
            catch (Exception ex) { }
        }
        private void WriteOnPicture(string name, PictureBox pic)
        {
            Image image = pic.Image;
            var font = new Font("Arial", 15, FontStyle.Bold);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(name, font, Brushes.White, new Point(100, 200));
            pic.Image = image;
        }

        #endregion





        #region Camera



        private void SendFrames()
        {
            bool sendFrames = true;
            while (true)
            {
                Image capturedImage = null;

                capturedImage = currentFrame;

                //capturedImage = Pic.Image;

                byte[] imageData = null;
                if (capturedImage != null && sendFrames)
                {
                    // Resize to very small dimensions and compress with high compression
                    imageData = DataProcessor.ResizeAndCompressImage(capturedImage, 250, 250, 250L); // Adjust dimensions and quality as needed

                    if (imageData.Length <= 64000)
                    {
                        try
                        {
                            imageData = DataProcessor.AddMetadata(imageData, "Frame");
                            byte[] encryptedframe = aes.EncryptByte(imageData);
                            udpClient.Send(encryptedframe, encryptedframe.Length);
                        }
                        catch (Exception e) { }
                    }

                }

                string encryptedImageData = null;
                if (imageData != null)
                {
                    encryptedImageData = aes.EncryptData(imageData, aes.Key, aes.IV); // Encrypt image data
                }



                if (CamButton.Text == "Show camera")
                {
                    sendFrames = false;
                }
                else
                {
                    sendFrames = true;
                }

                Thread.Sleep(100); // Control frame sending frequency


            }
        }




        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            currentFrame = (Bitmap)eventArgs.Frame.Clone();
            Pic.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void SendMessage(string message) 
        {
            
            byte[] data = Encoding.ASCII.GetBytes(message);
            data = aes.EncryptDatafr(data);
            try
            {
                udpClient.Send(data, data.Length);

            }
            catch { }
        }

        #endregion




        #region receiving

        public void Startreceive()
        {
            IPEndPoint clientEndPoint = serverEndPoint;

            while (keepReceive)
            {


                try
                {
                    byte[] receivedBytes = udpClient.Receive(ref clientEndPoint);
                    receivedBytes = aes.DecryptByte(receivedBytes);
                    var (metadata, rawData) = GetMetadataAndData(receivedBytes, "|");
                    string[] metaarray = metadata.Split('#');
                    string datatype = metaarray[0];
                    string usernamio = metaarray[1];
                    switch (datatype)
                    {

                        case "%%%Frame%%%":

                            foreach (PictureBox pictureBox in pictureBoxes)
                            {
                                if (pictureBox.Tag.ToString() == usernamio)
                                {
                                    pictureBox.Image = DataProcessor.ByteArrayToImage(rawData);
                                }
                            }
                            break;
                        case "%%%Audio%%%":
                            bufferedWaveProvider.AddSamples(rawData, 0, rawData.Length);;
                            break;
                        case "%%%NewUser%%%":

                            flowLayoutPanel1.BeginInvoke((Action)(() => AddGuest(usernamio)));
                            membersUI.AddMember(usernamio);
                            break;
                        case "%%%ExistingUsers%%%":
                            for (int i = 1; i < metaarray.Length; i++)
                            {
                                try
                                {
                                    string name = metaarray[i];
                                    if (name != username)
                                    {
                                        flowLayoutPanel1.BeginInvoke((Action)(() => AddGuest(name)));
                                       membersUI.AddMember(name); 

                                    }

                                }
                                catch (Exception ex) { }
                            }
                            break;
                        case "%%%Left%%%":
                            
                            foreach (PictureBox pictureBox in pictureBoxes)
                            {
                                if (pictureBox.Tag.ToString() == usernamio)
                                {
                                    flowLayoutPanel1.BeginInvoke((Action)(() => flowLayoutPanel1.Controls.Remove(pictureBox)));
                                    membersUI.RemoveMember(usernamio);
                                }
                            }
                            break;
                        case "%%%StopCamera%%%":
                            foreach (PictureBox pictureBox in pictureBoxes)
                            {
                                if (pictureBox.Tag.ToString() == usernamio)
                                {
                                    pictureBox.BeginInvoke((Action)(() =>
                                    {
                                        pictureBox.Image = Resources.profile;
                                        WriteOnPicture(usernamio, pictureBox);
                                    }));

                                }
                            }
                            break;
                    }
                }
                catch {  }












            }
            
        }
      
        // Prepare and send message


        public static (string Metadata, byte[] ImageData) GetMetadataAndData(byte[] data, string delimiter)
        {
            string receivedString = Encoding.ASCII.GetString(data);
            int delimiterIndex = receivedString.IndexOf(delimiter, StringComparison.Ordinal);

            if (delimiterIndex == -1)
            {
                throw new ArgumentException("Delimiter not found in data.");
            }

            string metadata = receivedString.Substring(0, delimiterIndex);
            byte[] imageData = new byte[data.Length - (delimiterIndex + delimiter.Length)];
            Buffer.BlockCopy(data, delimiterIndex + delimiter.Length, imageData, 0, imageData.Length);

            return (metadata, imageData);
        }

        

        #endregion



        #region Audio



       

        private void InitializeAudioCapture()
        {
            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(44100, 1) // 44.1kHz, Mono
            };
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;

            //udpClientSend = new UdpClient();
            //remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000); // Replace with server IP and port
        }

        private void InitializeAudioPlayback()
        {
            waveOut = new WaveOutEvent();
            waveOut.PlaybackStopped += OnPlaybackStopped;

            WaveFormat waveFormat = new WaveFormat(44100, 1);
            bufferedWaveProvider = new BufferedWaveProvider(waveFormat)
            {
                BufferDuration = TimeSpan.FromSeconds(5),
                DiscardOnBufferOverflow = true
            };

            waveOut.Init(bufferedWaveProvider);
            waveOut.Play();
        }

        
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] audioData = e.Buffer.Take(e.BytesRecorded).ToArray();
            byte[] fullData = DataProcessor.AddMetadata(audioData, "Audio");
            byte[] encData = aes.EncryptByte(fullData);
            try
            {
                udpClient.SendAsync(encData, encData.Length);
            }catch (Exception ex) { }
            
        }

        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            waveIn.Dispose();
        }

       

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
        }


        #endregion



        #region screenshare
        public void SendScreen()
        {
            sendScreen = false;
            byte[] imageData = null;
            while (true)
            {
                
                Image capturedImage = null;
                if(sendScreen)
                {
                    capturedImage = CaptureScreenshot();
                    imageData = DataProcessor.ResizeAndCompressImage(capturedImage, 250, 250, 250L); // Adjust dimensions and quality as needed

                    if (imageData.Length <= 64000)
                    {
                        try
                        {
                            imageData = DataProcessor.AddMetadata(imageData, "Frame");
                            byte[] encryptedframe = aes.EncryptByte(imageData);
                            udpClient.Send(encryptedframe, encryptedframe.Length);
                        }
                        catch (Exception e) { }
                    }
                    Pic.Image = capturedImage;
                }










                Thread.Sleep(100); // Control frame sending frequency

            }
        }
        private Image CaptureScreenshot()
        {
            // Get the size of the primary screen
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            // Create a bitmap with the same size as the screen
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                // Copy the screen to the bitmap
                g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            }

            return screenshot;
        }

        #endregion

    }
}

using System;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;
using System.Collections.Generic;

namespace zoom_client
{
    public class NetworkClient
    {
        int portNo = 1500;
        public static readonly string ipAddress = "127.0.0.1";
        TcpClient client;//client Socket
        byte[] data;//stor the data that send to & from the server
        int count = 0;
        string serverPublicKey;
        RSA rsa;
        AesEncryption aes;
        public bool handshake = false;
        public string username;
        meetingUI meetingUI;
        public bool lobbyEnabled = false;
        
        public UdpClient udpClient;
        public int udport = 0;
        private loginUI loginUI;
        public List<(string, string)> chatHistory;
        public NetworkClient(loginUI control)
        {
            client = new TcpClient();
            chatHistory = new List<(string, string)>();
            loginUI = control;
            client.Connect(ipAddress, portNo);
           
            
            data = new byte[client.ReceiveBufferSize];
            rsa = new RSA();
            aes = new AesEncryption();
            
            udpClient = new UdpClient();
            udpClient.Connect(ipAddress, portNo);
            udport = int.Parse(((IPEndPoint)udpClient.Client.LocalEndPoint).Port.ToString());
        }
        public void Start()
        {
            if(count == 0)
            {
                SendMessage("%%%clientPublicKey%%%" + rsa.GetPublicKey());
                client.GetStream().BeginRead(data,
                                             0,
                                             System.Convert.ToInt32(client.ReceiveBufferSize),
                                             ReceiveMessage,
                                             null);
            }
            count++;
           
        }
      
        public void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                int bytesRead;

                // read the data from the server
                bytesRead = client.GetStream().EndRead(ar);

              

                // invoke the delegate to display the recived data
                string incommingData = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);


                // continue reading
                client.GetStream().BeginRead(data,
                                         0,
                                         System.Convert.ToInt32(client.ReceiveBufferSize),
                                         ReceiveMessage,
                                         null);
                if (incommingData.StartsWith("%%%ServerPublicKey%%%"))
                {

                    this.serverPublicKey = incommingData.Remove(0, 21);
                    string strkey = Convert.ToBase64String(aes.Key);

                    string strIV = Convert.ToBase64String(aes.IV);
                    string aesAndIVString = strkey + "|" + strIV;
                    string encryptedaesKeyIV = rsa.Encrypt(aesAndIVString, serverPublicKey);
                    SendMessage("%%%ClientAesKey%%%" + encryptedaesKeyIV);

                }
                else
                {
                   
                    incommingData = aes.Decrypt(incommingData);
                    if (incommingData == "%%%Handshake%%%")
                    {
                        handshake = true;
                    }
                    else if (incommingData.StartsWith("%%%LoginOK%%%")&&!lobbyEnabled)
                    {
                        string[] data = incommingData.Substring(13).Split('#');
                        username = data[0];


                        loginUI.BeginInvoke((Action)(() => loginUI.LoginToLobby()));


                    }
                    else if( incommingData == "LoginNOTOK")
                    {
                        MessageBox.Show("incorrect login details");
                    }
                    else if (incommingData == "registOK")
                    {
                        MessageBox.Show("thank you for signing up, please log in");
                    }
                    else if (incommingData == "registNotOK")
                    {
                        MessageBox.Show("please try to register again, make sure to follow instructions");
                    }
                    else if (incommingData == "WrongCode")
                    {
                        MessageBox.Show("It seems the code you have entered is incorrect. Please make sure you have entered a real meeting code");
                    }
                    else if (incommingData.StartsWith("%%%NewMeeting%%%"))
                    {
                        string[] newdata = incommingData.Substring(16).Split('#');
                        string newport = newdata[0];
                        string newusername = newdata[1];
                        string newcode = newdata[2];

                        loginUI.BeginInvoke((Action)(() => loginUI.LobbyToMeeting(newport, newusername, aes, udpClient,newcode)));
                     
                        


                    }
                    else if (incommingData.StartsWith("%%%JoinPort%%%"))
                    {
                        string[] newdata = incommingData.Substring(14).Split('#');
                        string joinport = newdata[0];
                        string joinusername = newdata[1];
                        string joincode = newdata[2];
                        loginUI.BeginInvoke((Action)(() => loginUI.LobbyToMeeting(joinport, joinusername, aes, udpClient,joincode)));
                    }
                    else if (incommingData.StartsWith("%%%NewUser%%%"))
                    {
                        string[] newdata = incommingData.Substring(13).Split('#');//אין ממש צורך בספליט
                        string username = newdata[0];
                        meetingUI.BeginInvoke((Action)(() => meetingUI.AddGuest(username)));
                    }
                    else if (incommingData.StartsWith("%%%Chat%%%"))
                    {
                        string[] newdata = incommingData.Substring(10).Split('#');
                        string username = newdata[0];
                        string message = newdata[1];
                        if(username!=this.username)
                        {
                            chatHistory.Add((message, username));
                            loginUI.BeginInvoke((Action)(() => loginUI.ReloadChat(message,username)));
                        }
                            


                    }

                }


            }
            catch
            {
                // ignore the error... fired when the user loggs off
            }
        }

        
        public void SendMessage(string message)
        {
            if (message != null && handshake)
            {
                message = aes.Encrypt(message);
            }

            try
            {
                // send message to the server
                NetworkStream ns = client.GetStream();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // send the text
                ns.Write(data, 0, data.Length);
                //ns.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//end SendMessage

        public void SendRegist(string username, string password, string name, string lname, string email, string city)
        {
            if (!handshake)
            {
                MessageBox.Show("please wait, try again in a few seconds");
                return;
            }
            SendMessage($"%%%regist%%% {username}#{password}#{name}#{lname}#{email}#{city}");
            Start();
        }

        public void SendLogin(string username, string password)
        {
            if (!handshake)
            {
                MessageBox.Show("an error occured, please try again");
                return;
            }
           

            SendMessage($"%%%login%%% {username}#{password}");
            Start();
        }





        
    }
}

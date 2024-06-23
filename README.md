winforms desktop app for online conferencing. *LAN only*. no hole punching.
  communications - 
    audio, video streaming uses UDP protocol
    login, regist, create a new meeting, join meeting, chat uses TCP protocol.
  encryption - 
    RSA -> AES:
      exchanging rsa keys to set aes key, which is used for all further communication.
  notes- 
    change the server IP to the right one on "NetworkClient.cs", the communication class.

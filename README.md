WinForms desktop app for online conferencing. LAN only (no hole punching)
# architecture
![image](https://github.com/user-attachments/assets/f75c807e-c455-4cdb-898c-ee51de2a08c5)


## Program Flow
![image](https://github.com/user-attachments/assets/ae70750c-6ded-4634-8304-04a547b9db72)


## communications  

audio, video streaming uses UDP protocol.
    
login, registration, creating a new meeting, joining meeting, and chat use TCP protocol.
  
## encryption  
 
RSA -> AES:
    
exchanging RSA keys to set AES key, which is used for all further communication.


  
# notes 
  
change the server IP to the right one on "NetworkClient.cs", the communication class.

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace zoom_client
{
    class RSA
    {
        private string _privateKey;
        private string _publicKey;
        private UnicodeEncoding _encoder;
        private RSACryptoServiceProvider _rsa;

        public RSA()
        {
            _encoder = new UnicodeEncoding();
            _rsa = new RSACryptoServiceProvider();

            _privateKey = _rsa.ToXmlString(true);
            _publicKey = _rsa.ToXmlString(false);
        }

        public string GetPrivateKey()
        {
            return this._privateKey;
        }
        public string GetPublicKey()
        {
            return this._publicKey;
        }

        public string Decrypt(string data)
        {

            var dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            _rsa.FromXmlString(_privateKey);
            var decryptedByte = _rsa.Decrypt(dataByte, false);
            return _encoder.GetString(decryptedByte);
        }

        public string Encrypt(string data, string _publicKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            var dataToEncrypt = _encoder.GetBytes(data);
            try
            {
                var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false);
                var length = encryptedByteArray.Length;
                var item = 0;
                var sb = new StringBuilder();
                foreach (var x in encryptedByteArray)
                {
                    item++;
                    sb.Append(x);

                    if (item < length)
                        sb.Append(",");
                }
                return sb.ToString();
            }
            catch(Exception ex) 
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            
            return null;
            
        }


    }
}

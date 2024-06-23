using System;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using AForge.Video;
using System.Windows.Forms;

public class AesEncryption
{
     public byte[] Key { get;private set; }
     public byte[] IV { get;private set; }

    public AesEncryption()
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 128;
            aes.GenerateKey();
            aes.GenerateIV();
            Key = aes.Key;
            IV = aes.IV;
        }
    }

    public AesEncryption(byte[] key, byte[] iv)
    {
        if (key == null || key.Length != 16)
            throw new ArgumentException("Key must be 128 bits (16 bytes) long.", nameof(key));
        if (iv == null || iv.Length != 16)
            throw new ArgumentException("IV must be 128 bits (16 bytes) long.", nameof(iv));

        this.Key = key;
        this.IV = iv;
    }
    public byte[] EncryptByte(byte[] Data)//ליודיפי
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(Data, 0, Data.Length);
                    csEncrypt.FlushFinalBlock();
                }

                return msEncrypt.ToArray();
            }
        }
    }

    public string EncryptData(byte[] data, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                    csEncrypt.FlushFinalBlock();
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
    public byte[] DecryptByte(byte[] encryptedData)//for udp
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (MemoryStream msOutput = new MemoryStream())
                    {
                        try
                        {
                            csDecrypt.CopyTo(msOutput);
                        }
                        catch (CryptographicException e)
                        {
                            MessageBox.Show($"Decryption failed during crypto stream operation: {e.Message}", "Decryption Error");
                            return null;
                        }
                        return msOutput.ToArray();
                    }
                }
            }
        }
    }
    public string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 128;
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] encrypted;

            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return Convert.ToBase64String(encrypted);
        }
    }
    public byte[] EncryptDatafr(byte[] plainBytes)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV =IV;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }







    public string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 128;
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (var msDecrypt = new System.IO.MemoryStream(cipherBytes))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

}

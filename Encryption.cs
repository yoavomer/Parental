using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    public class Encryption
    {
        Aes aes;
        /// <summary>
        ///  create symmetric encryption
        /// encryption constructor
        /// </summary>
        /// <param name="clientsStream"></param>
        public Encryption(NetworkStream clientsStream)
        {
            
            this.aes = Aes.Create();
            aes.GenerateKey();
            aes.GenerateIV();

            
            RecvEncryption(clientsStream);

        }

        /// <summary>
        /// receive asymmetric encryption from the client
        /// the first 10 chars of receving and sending are dedicated to getting the length
        /// </summary>
        /// <param name="clientsStream"></param>
        public void RecvEncryption(NetworkStream clientsStream)
        {
            byte[] recvedMod = new byte[10];
            clientsStream.Read(recvedMod, 0, 10);
            string strMod = System.Text.Encoding.Default.GetString(recvedMod);
            int lengthMod = int.Parse(strMod);
            byte[] Mod;
            byte[] fullMod = new byte[lengthMod];
            for (int i = 0; i < lengthMod; lengthMod = lengthMod - 16)
            {
                Mod = new byte[16];
                if (lengthMod < 16)
                {
                    Mod = new byte[lengthMod];
                }
                clientsStream.Read(Mod, 0, Mod.Length);
                Mod.CopyTo(fullMod, fullMod.Length - lengthMod);
            }

            byte[] recvedEx = new byte[10];
            clientsStream.Read(recvedEx, 0, 10);
            string strEx = System.Text.Encoding.Default.GetString(recvedEx);
            int lengthEx = int.Parse(strEx);
            byte[] Ex;
            byte[] fullEx = new byte[lengthEx];
            for (int i = 0; i < lengthEx; lengthEx = lengthEx - 16)
            {
                Ex = new byte[16];
                if (lengthEx < 16)
                {
                    Ex = new byte[lengthEx];
                }
                clientsStream.Read(Ex, 0, Ex.Length);
                Ex.CopyTo(fullEx, fullEx.Length - lengthEx);
            }



            SendSymetricEncryp(clientsStream, fullMod, fullEx);



        }
        public void SendSymetricEncryp(NetworkStream clientsStream, byte[] mod, byte[] ex)
        {
            //sending symmetric key and iv encrypt by the asymetric key recived from client
            RSA rsa = RSA.Create();
            RSAParameters rsaKeyInfo = new RSAParameters();
            rsaKeyInfo.Modulus = mod;
            rsaKeyInfo.Exponent = ex;

            rsa.ImportParameters(rsaKeyInfo);

            byte[] encryptKey = rsa.Encrypt(this.aes.Key, RSAEncryptionPadding.Pkcs1);
            byte[] encryptIv = rsa.Encrypt(this.aes.IV, RSAEncryptionPadding.Pkcs1);


            SendNotencrypt(clientsStream, encryptKey);
            SendNotencrypt(clientsStream, encryptIv);

        }
        /// <summary>
        /// the only place sending not symmetric encrypt
        /// </summary>
        /// <param name="clientsStream"></param>
        /// <param name="data"></param>
        public void SendNotencrypt(NetworkStream clientsStream, byte[] data)
        {
            string s = data.Length.ToString();
            string send = s.PadLeft(10, '0');
            byte[] byteDataLen = Encoding.ASCII.GetBytes(send);
            byte[] byteData = new byte[data.Length + byteDataLen.Length];
            byteDataLen.CopyTo(byteData, 0);
            data.CopyTo(byteData, byteDataLen.Length);
            clientsStream.Write(byteData, 0, byteData.Length);
        }
        /// <summary>
        /// symmetric kry encrypt
        /// recive in byte array
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encrypte(byte [] data)
        {
            MemoryStream msEncrypt = new MemoryStream(data);
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write);
            StreamWriter swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.WriteLine(data);
            byte[] byteData = msEncrypt.ToArray();
            return byteData;
        }
        /// <summary>
        /// decrypting string recived
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Decrypte(string data)
        {
            byte[] byteMassage = Encoding.ASCII.GetBytes(data);
            MemoryStream msDecrypt = new MemoryStream(byteMassage);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader sDecrypt = new StreamReader(csDecrypt);
            byteMassage = msDecrypt.ToArray();
            string dec = System.Text.Encoding.Default.GetString(byteMassage);
            return dec;
        }

    }
}

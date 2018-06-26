using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ThreeDes
{
    public class Cyber3Des
    {

        private readonly TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        // TripleDES des = TripleDES.Create();

        private byte[] iv;
        private byte[] key;

        public byte[] Key { get => key; set => key = value; }
        public byte[] Iv { get => iv; set => iv = value; }


        public Cyber3Des(bool GenerateKeyAndIV = false)
        {
            if(GenerateKeyAndIV)
            {
                this.Key = GenerateKey();
                this.Iv = GenerateIV();
            }
        }

        public byte[] Encrypt(byte[] bytesToEncrypt)
        {
            return Transform(bytesToEncrypt, des.CreateEncryptor(key, iv));
        }

        public byte[] Decrypt(byte[] bytesToDecrypt)
        {
            return Transform(bytesToDecrypt, des.CreateDecryptor(key, iv));
        }


        public string Encrypt(string textToEncrypt)
        {

            byte[] bytes = Encoding.UTF8.GetBytes(textToEncrypt);
            byte[] bResult = Transform(bytes, des.CreateEncryptor(key, iv));
            string result = Convert.ToBase64String(bResult);



            return result;
        }


        public string Decrypt(string textToDecrypt)
        {

            byte[] bytes = Convert.FromBase64String(textToDecrypt);
            byte[] bResult = Transform(bytes, des.CreateDecryptor(key, iv));
            string result = Encoding.UTF8.GetString(bResult);

            return result;
        }



        public byte[] Transform(byte[] bytes, ICryptoTransform transform)
        {
            using (var mstream = new MemoryStream())
            {
                using (var cstream = new CryptoStream(mstream, transform, CryptoStreamMode.Write))
                {
                    /////////////////////////////////
                    cstream.Write(bytes, 0, bytes.Length);

                    cstream.FlushFinalBlock();


                    /////////////////////////////////////
                    ////convert from 
                    mstream.Position = 0;
                     
                    
                    return  mstream.ToArray();//result;

                }

            }
        }

        public static byte[] GenerateKey()
        {
            using (var desa = new TripleDESCryptoServiceProvider())
            {
                desa.GenerateKey();

                return desa.Key;

            }
        }


        public static byte[] GenerateIV()
        {
             using (var desa = new TripleDESCryptoServiceProvider())
            {
                desa.GenerateIV();

                return desa.IV;

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            EncryptAes();

            DecryptAes();
            byte[] data = { 5, 6, 7, 8, 9 };



            byte[] key = GenerateKey();
            byte[] iv = GenerateKey();

            //or
            byte[] kiv = new byte[16];
            RandomNumberGenerator.Create().GetBytes(kiv);




            byte[] encryptedResult = Encrypt(data, key, iv);
            byte[] decryptedResult = Decrypt(encryptedResult, key, iv);


            DisplayBytes(decryptedResult);


            Console.ReadLine();
        }

        public static byte[] GenerateKey()
        {
            byte[] key = new byte[16];

            RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(key);
            

            return key;

        }

        public static void DisplayBytes(byte[] byteArray)
        {
            StringBuilder sb = new StringBuilder();
            //string result = "";
            for(int i=0; i < byteArray.Length; i++)
            {
                sb.Append(byteArray[i] + " ");
            }

            Console.WriteLine(sb.ToString());
        }

        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes algorithm = Aes.Create())
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
                return Crypt(data, key, iv, encryptor);

        }
        public static byte[] Crypt(byte[] data, byte[] key, byte[] iv, ICryptoTransform cryptor)
        {
            MemoryStream m = new MemoryStream();
            using (Stream c = new CryptoStream(m, cryptor, CryptoStreamMode.Write))
                c.Write(data, 0, data.Length);
            return m.ToArray();
        }

       


        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes algorithm = Aes.Create())
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
                return Crypt(data, key, iv, decryptor);

        }



        public static void EncryptAes()
        {

            byte[] key = { 23, 45, 65, 78, 88, 76, 54, 73, 43, 56, 66, 23, 12, 78, 55, 88};
            byte[] data = {5, 6, 7, 8, 9 };
            byte[] iv = { 56, 89, 45, 76, 23, 45, 99, 23, 61, 72, 54, 44, 34, 35, 36, 87 };

            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
            using (Stream f = File.Create("encryptedFile.txt"))
            using (Stream c = new CryptoStream(f, encryptor, CryptoStreamMode.Write))
            {
                c.Write(data, 0, data.Length);
            }

           
        }

        public static void DecryptAes()
        {

            byte[] key = { 23, 45, 65, 78, 88, 76, 54, 73, 43, 56, 66, 23, 12, 78, 55, 88 };
            
            byte[] iv = { 56, 89, 45, 76, 23, 45, 99, 23, 61, 72, 54, 44, 34, 35, 36, 87 };

            StringBuilder sb = new StringBuilder();

            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
            using (Stream f = File.OpenRead("encryptedFile.txt"))
            using (Stream c = new CryptoStream(f, decryptor, CryptoStreamMode.Read))
            {
              for(int i; (i = c.ReadByte()) > -1;)
                {
                    Console.Write(i + " ");

                    //sb.Append(i + " ");
                }
                
            }
            string res = sb.ToString();
            File.WriteAllText("decryptedFile.txt", res);

        }

    }
}

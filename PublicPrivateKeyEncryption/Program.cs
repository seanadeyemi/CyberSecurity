using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PublicPrivateKeyEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte[] publickey;
            //byte[] publicPrivatekey;

            //Gaby expects a message so he prepares a public and private key
            //he sends the public key to lola
            using (var rsa = new RSACryptoServiceProvider())
            {
                //publickey = rsa.ExportCspBlob(false);
                //publicPrivatekey = rsa.ExportCspBlob(true);


                File.WriteAllText("PublicKey.xml", rsa.ToXmlString(false));
                File.WriteAllText("PublicAndPrivateKey.xml", rsa.ToXmlString(true));

            }



            //Lola is about to send the message, She has received Gaby's public key
            //She uses it to encrypt her message to Gaby
            byte[] data = Encoding.UTF8.GetBytes("This is the super secret message");
            byte[] encryptedData;


            using (var rsaSender = new RSACryptoServiceProvider())
            {
                string publicKey = File.ReadAllText("PublicKey.xml");

                //rsaSender.ImportCspBlob(publickey);

                rsaSender.FromXmlString(publicKey);
                encryptedData = rsaSender.Encrypt(data, true);                

            }

            //Gaby has received the encrypted message
            //He uses his public private key which he had stored to decrypt Lolas message
            byte[] decryptedMessage;
            using (var rsaReceiver = new RSACryptoServiceProvider())
            {
                string publicPrivateKey = File.ReadAllText("PublicAndPrivateKey.xml");

                rsaReceiver.FromXmlString(publicPrivateKey);

                decryptedMessage = rsaReceiver.Decrypt(encryptedData, true);

                string message = Encoding.UTF8.GetString(decryptedMessage);

            }

            Console.ReadLine();

        }



    }
}

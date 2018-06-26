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

            RSAParameters _publicKey;
            RSAParameters _privateKey;


            //Gaby expects a message from lola so he prepares a public and private key
            //he sends the public key to lola
            using (var rsa = new RSACryptoServiceProvider())
            {

                //we'll use these if we want to save the keys to byte arrays instead
                //publickey = rsa.ExportCspBlob(false);
                //publicPrivatekey = rsa.ExportCspBlob(true);

                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);


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

                //we'll use this if we are bringing in  the public key from a byte array
                //rsaSender.ImportCspBlob(publickey);


                //rsaSender.ImportParameters(_publicKey);

                rsaSender.FromXmlString(publicKey);
                encryptedData = rsaSender.Encrypt(data, true);                

            }

            //Gaby has received the encrypted message
            //He uses his public private key which he had stored to decrypt Lolas message
            byte[] decryptedMessage;
            string message;
            using (var rsaReceiver = new RSACryptoServiceProvider())
            {

                //rsaReceiver.ImportParameters(_privateKey);

                string publicPrivateKey = File.ReadAllText("PublicAndPrivateKey.xml");

                rsaReceiver.FromXmlString(publicPrivateKey);

                decryptedMessage = rsaReceiver.Decrypt(encryptedData, true);

                 message = Encoding.UTF8.GetString(decryptedMessage);

            }
            Console.WriteLine(message);
            Console.ReadLine();

        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cyber3Des crypto = new Cyber3Des();
            //crypto.Key = Cyber3Des.GenerateKey();
            //crypto.Iv = Cyber3Des.GenerateIV();


            Cyber3Des crypto = new Cyber3Des(true);


            string Message = "The very very super secret Message";

           string encryptedMessage = crypto.Encrypt(Message);

            Console.WriteLine(encryptedMessage);
            /////////////////////////////


            string decryptedMessage = crypto.Decrypt(encryptedMessage);

            Console.WriteLine(decryptedMessage);

            Console.ReadLine();


           
        }
    }
}

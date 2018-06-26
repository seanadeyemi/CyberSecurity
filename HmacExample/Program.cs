using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmacExample
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] key = CyberHMAC.CreateKey();
            byte[] key2 = CyberHMAC.CreateKey();

            string message = "This is our message";
            string message2 = "This is our message";


            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            string hmacHash = CyberHMAC.GetHMAC256(key, messageBytes);


            byte[] message2Bytes = Encoding.UTF8.GetBytes(message2);
            string hmacHash2 = CyberHMAC.GetHMAC256(key, message2Bytes);


            Console.WriteLine($"Hashed message is {hmacHash}");

            Console.WriteLine($"Hashed message 2 is {hmacHash2}");

            Console.ReadLine();

        }
    }
}

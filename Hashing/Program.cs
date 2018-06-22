using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "mY%p@55word";



            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);

            //string result = System.Text.Encoding.UTF8.GetString(data);





            // byte[] data2 = Convert.FromBase64String(password);

            // string result2 = Convert.ToBase64String(data2);


            byte[] hash = SHA256.Create().ComputeHash(data);
            SHA256 x = SHA256.Create();
            byte[] v = x.ComputeHash(hash);
            
            //byte[] hash = MD5.Create().ComputeHash(data);

            //for(int i = 0; i < hash.Length; i++)
            //{
            //    Console.Write(hash[i] + " ");
            //}

            Guid hashGuid = StringToGUID(password);

            Console.WriteLine("Your hash is "+hashGuid);

            string password2 = "mY%p@55word";

            Guid hashGuid2 = StringToGUID(password2);

            Console.WriteLine("Your second hash is "+hashGuid2);


            Console.WriteLine(SHA256Creator(password));

            Console.ReadLine();
        }

        public static Guid StringToGUID(string value)
        {
            MD5 md5hasher = MD5.Create();
            byte[] data = md5hasher.ComputeHash(Encoding.Default.GetBytes(value));
            
           

            return new Guid(data);
        }
        public static string SHA256Creator(string value)
        {
            SHA256 sha256hasher = SHA256.Create();
            byte[] data = sha256hasher.ComputeHash(Encoding.Default.GetBytes(value));
            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                result += (data[i] + " ");
            }

            return result;
        }

    }
}

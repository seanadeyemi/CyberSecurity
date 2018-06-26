using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HmacExample
{
    public class CyberHMAC
    {
        static int size = 32;
        public static byte[] CreateKey()
        {
            using (var random = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[size];

                random.GetBytes(key);

                return key;
            }

        }


        public static string GetHMAC256(byte[] key, byte[] messageToHash)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] result;
                result = hmac.ComputeHash(messageToHash);
                return Convert.ToBase64String(result);
            }
        }
        public static string GetHMAC1(byte[] key, byte[] messageToHash)
        {
            using (var hmac = new HMACSHA1(key))
            {
                byte[] result;
                result = hmac.ComputeHash(messageToHash);
                return Convert.ToBase64String(result);
            }
        }
        public static string GetHMACMD5(byte[] key, byte[] messageToHash)
        {
            using (var hmac = new HMACMD5(key))
            {
                byte[] result;
                result = hmac.ComputeHash(messageToHash);
                return Convert.ToBase64String(result);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSigning
{
    class Program
    {
        static void Main(string[] args)
        {

            string message = "Our message";
            byte[] data = Encoding.UTF8.GetBytes(message);

            CyberSignature sig = new CyberSignature();
            SignatureSet sigSet = sig.GetSignature(data);

            //data[0] = 222;

            // var result = sig.Verify(sigSet.publicKey, sigSet.signature, data);

            var result = sig.Verify(sigSet, data);

            Console.WriteLine(result);

            Console.ReadLine();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSigning
{
    public class CyberSignature
    {
        SHA256 hasher;

        public CyberSignature()
        {
            hasher = SHA256.Create();
        }
        public SignatureSet GetSignature(byte[] data)
        {

            using (var rsa = new RSACryptoServiceProvider())
            {


                var sigSet = new SignatureSet
                {
                    signature = rsa.SignData(data, hasher),
                    publicKey = rsa.ExportCspBlob(false)
                };

                return sigSet;

            }
        }


        public bool Verify(byte[] publicKey, byte[] signature, byte[] dataToCheck)
        {

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportCspBlob(publicKey);
                return rsa.VerifyData(dataToCheck, hasher, signature);
            }


        }

        public bool Verify(SignatureSet set, byte[] dataToCheck)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportCspBlob(set.publicKey);
                return rsa.VerifyData(dataToCheck, hasher, set.signature);
            }
        }
    }


    public class SignatureSet
    {
        public byte[] publicKey;
        public byte[] signature;
    }
}

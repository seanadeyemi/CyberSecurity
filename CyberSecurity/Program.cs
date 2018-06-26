using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace CyberSecurity
{
    
    [SecurityPermission(SecurityAction.InheritanceDemand)] 
    public class Document
    {
        [PrincipalPermission(SecurityAction.Demand, Name = "Oriahi")]
        public string GetFile()
        {
            return "The main file";
        }

        //Declarative
        [PrincipalPermission(SecurityAction.Demand, Role = "Manager")]
        public string GetName()
        {
            return "The main name";
        }

        [RegistryPermission(SecurityAction.Demand)]
        public string RegistryValues()
        {
            return "The main value";
        }

        [SecurityPermission(SecurityAction.LinkDemand)]
        public int GetNumber()
        {
            return 0;
        }


    }



    class Program
    {




        static void Main(string[] args)
        {
            GenericIdentity id = new GenericIdentity("Chuks");
            GenericPrincipal p = new GenericPrincipal(id, new string[] { "Manager" });


            //WindowsIdentity wid = WindowsIdentity.GetCurrent();
            //WindowsPrincipal wp = new WindowsPrincipal(wid);
            //string name = wp.Identity.Name;
            //string type = wp.Identity.AuthenticationType;
            //var auth = wp.Identity.IsAuthenticated;

            //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            //WindowsPrincipal WinPrinc = (WindowsPrincipal)Thread.CurrentPrincipal;

            //Console.WriteLine(Thread.CurrentPrincipal.Identity.Name);
            //Console.WriteLine(name);
            //Console.WriteLine(type);
            //Console.WriteLine(auth);


            //alternative (imperative)
            //PrincipalPermission pp = new PrincipalPermission("Gaby", null);
            //pp.Demand();


            //FileIOPermission fp = new FileIOPermission(FileIOPermissionAccess.Read| FileIOPermissionAccess.Write, "picture.jpg");
            //fp.Demand();

            //RegistryPermission rp = new RegistryPermission(RegistryPermissionAccess.Read, "");
            //rp.Demand();


            Thread.CurrentPrincipal = p;


            Document doc = new Document();
            //var str = doc.GetFile();

            //var str2 = doc.GetName();

            /////////////////////////////////
            //File Encrypt and Decrypt


            File.WriteAllText("ourText.txt", "This is a secret message from Chuks to the world");
            File.Encrypt("ourText.txt");

            string txt = File.ReadAllText("ourText.txt");
            File.Decrypt("ourText.txt");




            /////////////////////////////////////
            /*Protected Data*/

            byte[] stuff = {15, 17, 22, 45, 76 };

            DataProtectionScope scope = DataProtectionScope.CurrentUser;

            byte[] encryptedData = ProtectedData.Protect(stuff, null, scope);

            byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, scope);

            /****************************************************/



            Console.WriteLine(txt);

            //Console.WriteLine(str);
            //Console.WriteLine(str2);
            Console.ReadLine();





        }
    }
}

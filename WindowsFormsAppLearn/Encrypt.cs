using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppLearn
{
    internal class Encrypt
    {
        public string encryptData(string pass)
        {
            using (MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider()) {

                byte[] data = mD5.ComputeHash(new UTF8Encoding().GetBytes(pass));
                return (Convert.ToBase64String(data));
            } 
        
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.DbConnect
{
    class MD5Encrypt
    {

        private static MD5Encrypt md5Encrypt = null;

        public static MD5Encrypt getMD5Encrypt()
        {
            return md5Encrypt ?? (md5Encrypt = new MD5Encrypt());
        }

        private MD5Encrypt()
        {

        }

        public string MD5(string word)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(word));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Data
{
    public class PasswordHash
    {
        public static string HashWithMD5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            md5.Dispose();
            return strBuilder.ToString();
        }
    }
}

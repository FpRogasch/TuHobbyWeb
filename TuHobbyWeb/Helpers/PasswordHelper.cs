using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TuHobbyWeb.Helpers
{
    public class PasswordHelper
    {
        public static void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using ( var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;

                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool CheckPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computePassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computePassword.Length; i++)
                    if (passwordHash[i] != computePassword[i])
                        return false;
            }
            return true;
        }

        internal static void CheckPassword(string password, byte[] passwordHash, object passwordSalt)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Security.Cryptography;
using System.Text;


namespace Infrastructure
{
    public static class SecurityMethods
    {
        public static string HashPassword(string password)
        {
            using (XSystem.Security.Cryptography.MD5CryptoServiceProvider md5 = new XSystem.Security.Cryptography.MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                return Convert.ToBase64String(data);
            }
        }

        public static bool CheckPassword(string password, string passwordHash)
        {
            if (passwordHash == HashPassword(password))
            {
                return true;
            }
            return false;
        }

        public static string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public static string CreateRandomOTP()
        {
            var random = new Random();
            return (random.Next(999999)).ToString();
        }
    }
}

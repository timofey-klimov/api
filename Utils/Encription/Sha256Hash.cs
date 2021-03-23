using System.Security.Cryptography;
using System.Text;

namespace Utils.Encription
{
    public static class Sha256Hash
    {
        public static byte[] ComputeSha256Hash(string rawData)
        {
            using(var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            }
        }
    }
}

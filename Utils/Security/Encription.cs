using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utils.Encription
{
    public static class Encription
    {
        public static byte[] ComputeSha256Hash(string rawData)
        {
            using(var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            }
        }

        public static byte[] ComputeSha256Hash(Stream stream)
        {
            if (!stream.CanRead)
                throw new ArgumentException("Stream is not readble");

            var buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);

            using(var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(buffer);
            }
        }
    }
}

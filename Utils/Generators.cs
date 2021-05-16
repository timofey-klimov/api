using System;

namespace Utils
{
    public class Generators
    {
        public static string GenerateRandomCode(int codeLength)
        {
            var code = string.Empty;
            var random = new Random();

            for(int i = 0; i < codeLength; i++)
            {
                code += random.Next(0, 9).ToString();
            }

            return code;
        }
    }
}

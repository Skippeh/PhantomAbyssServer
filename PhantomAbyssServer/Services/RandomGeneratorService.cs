using System;
using System.Text;

namespace PhantomAbyssServer.Services
{
    public class RandomGeneratorService
    {
        private readonly char[] sharerIdChars = "abcdefghjkmnpqrstyvz".ToCharArray();

        private readonly Random random = new();
        
        public string GenerateSharerId()
        {
            return GenerateString(sharerIdChars, 8);
        }

        private string GenerateString(char[] chars, uint length)
        {
            StringBuilder builder = new();
            
            for (int i = 0; i < length; ++i)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }
    }
}
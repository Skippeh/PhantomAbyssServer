using System.Security.Cryptography;
using System.Text;

namespace PhantomAbyssServer.Services
{
    public class CryptoService
    {
        public string GetMD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input);
                byte[] hashedBytes = md5.ComputeHash(bytes);

                var builder = new StringBuilder();

                foreach (var hashedByte in hashedBytes)
                {
                    builder.Append(hashedByte.ToString("X2"));
                }
                
                return builder.ToString();
            }
        }
    }
}
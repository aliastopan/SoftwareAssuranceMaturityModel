using System.Security.Cryptography;
using System.Text;

namespace SoftwareAssuranceMaturityModel.Application.Common.Helpers
{
    public class Cryptograph
    {
        public static string GetHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder hash = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                    hash.Append(data[i].ToString("x2"));

                return hash.ToString();
            }
        }

        public static string GetHash(string input, string salt)
        {
            var saltyInput = string.Concat(input, salt);
            return GetHash(saltyInput);

        }
    }
}
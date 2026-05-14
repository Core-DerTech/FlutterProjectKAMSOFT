using System.Security.Cryptography;
using System.Text;

namespace FlutterProjectKAMSOFT.Encryption.Ciphers
{
    public class EncriptionSHA
    {
        public string Encrypt<T>(T input)
        {
            if (input == null)
                throw new ArgumentException(nameof(input));

            string text = input.ToString()!;

            byte[] hash = SHA256.HashData(
                Encoding.UTF8.GetBytes(text)
            );

            return Convert.ToHexString(hash);
        }
    }
}

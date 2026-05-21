using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.CipherFactory;
using FlutterProjectKAMSOFT.Encryption.Models;
using System.Text;

namespace FlutterProjectKAMSOFT.Encryption.Ciphers
{
    public class CaesarCipher : ICipher<CipherRequestCaesar>
    {
        IValidator<CipherRequestCaesar> _validator;
        private const int DEFAULT_SHIFT = 3;

        public CaesarCipher(IValidator<CipherRequestCaesar> validator)
        {
            _validator = validator;
        }

        public string Encrypt(CipherRequestCaesar request)
        {
            _validator.ValidateAndThrow(request);
            int shift = request.Shift ?? DEFAULT_SHIFT;
            return Process(request.Text, shift, request.Alphabet);
        }
        public string Decrypt(CipherRequestCaesar request)
        {
            _validator.ValidateAndThrow(request);
            int shift = request.Shift ?? DEFAULT_SHIFT;
            return Process(request.Text, -shift, request.Alphabet);
        }
        private string Process(string text, int shift, string alphabet)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in text)
            {
                bool isLower = char.IsLower(c);
                char upperChar = char.ToUpper(c);

                int index = alphabet.IndexOf(upperChar);

                if (index >= 0)
                {
                    int newIndex = (index + shift) % alphabet.Length;

                    if (newIndex < 0)
                        newIndex += alphabet.Length;

                    char newChar = alphabet[newIndex];

                    result.Append(isLower ? char.ToLower(newChar) : newChar);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}

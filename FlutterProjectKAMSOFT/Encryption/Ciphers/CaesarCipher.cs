using FluentValidation;
using FlutterProjectKAMSOFT.Ciphers.CipherValidation;
using System.Text;

namespace FlutterProjectKAMSOFT.Encryption.Ciphers
{
    public class CaesarCipher
    {
        private const int DEFAULT_SHIFT = 3;
        private readonly string _alphabet;

        public CaesarCipher(CipherDataModel model, IValidator<CipherDataModel> validator)
        {
            validator.ValidateAndThrow(model);
            _alphabet = model.Alphabet;
        }

        public string Encrypt(string text, int shift = DEFAULT_SHIFT)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("No text to encrypt was provided");
            }

            return Process(text, shift);
        }
        public string Decrypt(string text, int shift = DEFAULT_SHIFT)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("No text tp decrypt was provided");
            }
            return Process(text, -shift);
        }
        private string Process(string text, int shift)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in text)
            {
                bool isLower = char.IsLower(c);
                char upperChar = char.ToUpper(c);

                int index = _alphabet.IndexOf(upperChar);

                if (index >= 0)
                {
                    int newIndex = (index + shift) % _alphabet.Length;

                    if (newIndex < 0)
                        newIndex += _alphabet.Length;

                    char newChar = _alphabet[newIndex];

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

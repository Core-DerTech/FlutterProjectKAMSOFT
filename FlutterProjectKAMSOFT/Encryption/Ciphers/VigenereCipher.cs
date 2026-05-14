using FluentValidation;
using FlutterProjectKAMSOFT.Ciphers.CipherValidation;
using System.Text;

namespace FlutterProjectKAMSOFT.Encryption.Ciphers
{
    public class VigenereCipher
    {
        private readonly string _alphabet;
        private readonly string _password;

        public VigenereCipher(CipherDataModel model, IValidator<CipherDataModel> validator)
        {
            validator.ValidateAndThrow(model);

            _alphabet = model.Alphabet.ToUpper();
            _password = model.Password.ToUpper();
        }

        public string Encrypt(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("No text to encrypt was provided");

            return Process(text, true);
        }

        public string Decrypt(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("No text to decrypt was provided");

            return Process(text, false);
        }

        private string Process(string text, bool encrypt)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;

            foreach (char c in text)
            {
                bool isLower = char.IsLower(c);
                char upperChar = char.ToUpper(c);

                int textIndex = _alphabet.IndexOf(upperChar);

                if (textIndex >= 0)
                {
                    char keyChar = _password[keyIndex % _password.Length];

                    int keyShift = _alphabet.IndexOf(keyChar);

                    if (keyShift < 0)
                        throw new ArgumentException("Password contains invalid characters for alphabet");

                    int newIndex = encrypt
                        ? (textIndex + keyShift) % _alphabet.Length
                        : (textIndex - keyShift + _alphabet.Length) % _alphabet.Length;

                    char newChar = _alphabet[newIndex];

                    result.Append(isLower ? char.ToLower(newChar) : newChar);

                    keyIndex++;
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
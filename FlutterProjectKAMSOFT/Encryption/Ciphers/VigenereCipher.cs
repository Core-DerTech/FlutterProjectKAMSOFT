using FluentValidation;
using FlutterProjectKAMSOFT.Ciphers.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.CipherFactory;
using FlutterProjectKAMSOFT.Encryption.Models;
using System.Text;

namespace FlutterProjectKAMSOFT.Encryption.Ciphers
{
    public class VigenereCipher : ICipher<CipherRequestVigenere>
    {
        private IValidator<CipherRequestVigenere> _validator;

        public VigenereCipher(IValidator<CipherRequestVigenere> validator)
        {
            _validator = validator;
        }

        public string Encrypt(CipherRequestVigenere request)
        {
            _validator.Validate(request);

            return Process(request.Text, request.Key, true, request.Alphabet);
        }

        public string Decrypt(CipherRequestVigenere request)
        {
            _validator.Validate(request);
            return Process(request.Text, request.Key, false, request.Alphabet);
        }

        private string Process(string text, string insertedPassword, bool encrypt, string alphabet)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;
            string password = insertedPassword.ToUpper();
            foreach (char c in text)
            {
                bool isLower = char.IsLower(c);
                char upperChar = char.ToUpper(c);

                int textIndex = alphabet.IndexOf(upperChar);

                if (textIndex >= 0)
                {
                    char keyChar = password[keyIndex % password.Length];

                    int keyShift = alphabet.IndexOf(keyChar);

                    if (keyShift < 0)
                        throw new ArgumentException("Password contains invalid characters for alphabet");

                    int newIndex = encrypt
                        ? (textIndex + keyShift) % alphabet.Length
                        : (textIndex - keyShift + alphabet.Length) % alphabet.Length;

                    char newChar = alphabet[newIndex];

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
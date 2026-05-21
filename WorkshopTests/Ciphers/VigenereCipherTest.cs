using FluentAssertions;
using FlutterProjectKAMSOFT.Encryption.Ciphers;
using FlutterProjectKAMSOFT.Encryption.Models;
using FlutterProjectKAMSOFT.Encryption.CipherValidation;

namespace WorkshopTests.Ciphers
{
    public class VigenereCipherTest
    {
        [Fact]
        public void VigenereCipherShouldReturnTrueForCorrectPassword()
        {
            VigenereRequestValidator validator = new VigenereRequestValidator();
            CipherRequestVigenere model = new CipherRequestVigenere()
            {
                Text = "Text to encrypt",
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Key = "qwer",
            };
            string textToEnctyp = "Mamma mia";

            VigenereCipher cipher = new(validator);

            string encryptedText = cipher.Encrypt(model);
            string decryptedText = cipher.Decrypt(model);

            decryptedText.Should().Be(textToEnctyp);
        }

        [Fact]
        public void VigenereCipherShouldNotReturnTrueForCorrectPassword()
        {
            VigenereRequestValidator validator = new VigenereRequestValidator();
            CipherRequestVigenere model = new CipherRequestVigenere()
            {
                Text = "Text to encrypt",
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Key = "qwer",
            };
            string textToEnctyp = "Mamma mia";

            VigenereCipher cipher = new(validator);

            string encryptedText = cipher.Encrypt(model);
            string decryptedText = cipher.Decrypt(model);

            decryptedText.Should().NotBe(textToEnctyp);
        }
    }
}

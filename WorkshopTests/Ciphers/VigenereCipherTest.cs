using FluentAssertions;
using FlutterProjectKAMSOFT.Ciphers.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.Ciphers;
using FlutterProjectKAMSOFT.Encryption.Models;
using FlutterProjectKAMSOFT.Encryption.CipherFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopTests.Ciphers
{
    public class VigenereCipherTest
    {
        [Fact]
        public void VigenereCipherShouldReturnTrueForCorrectPassword()
        {
            CipherValidator validator = new CipherValidator();
            CipherRequest model = new CipherRequest()
            {
                Text = "Text to encrypt",
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Key = "qwer",
                Shift = 4,
                CipherType = CipherType.Vigenere
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
            CipherValidator validator = new CipherValidator();
            CipherRequest model = new CipherRequest()
            {
                Text = "Text to encrypt",
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Key = "qwer",
                Shift = 4,
                CipherType =  CipherType.Vigenere

            };
            string textToEnctyp = "Mamma mia";

            VigenereCipher cipher = new(validator);

            string encryptedText = cipher.Encrypt(model);
            string decryptedText = cipher.Decrypt(model);

            decryptedText.Should().NotBe(textToEnctyp);
        }
    }
}

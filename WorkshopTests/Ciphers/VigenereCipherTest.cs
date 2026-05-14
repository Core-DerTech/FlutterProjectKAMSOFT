using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FlutterProjectKAMSOFT.Ciphers.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.Ciphers;

namespace WorkshopTests.Ciphers
{
    public class VigenereCipherTest
    {
        [Fact]

        public void VigenereCipherShouldReturnTrueForCorrectPassword()
        {
            CipherValidator validator = new CipherValidator();
            CipherDataModel model = new CipherDataModel()
            { 
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Password = "password",
            };
            string textToEnctyp = "Mamma mia kurwa";

            VigenereCipher cipher = new(model, validator);

            string encryptedText = cipher.Encrypt(textToEnctyp);
            string decryptedText = cipher.Decrypt(encryptedText, "password");

            decryptedText.Should().Be(textToEnctyp);
        }
        [Fact]

        public void VigenereCipherShouldNotReturnTrueForCorrectPassword()
        {
            CipherValidator validator = new CipherValidator();
            CipherDataModel model = new CipherDataModel()
            { 
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Password = "password",
            };
            string textToEnctyp = "Mamma mia kurwa";

            VigenereCipher cipher = new(model, validator);

            string encryptedText = cipher.Encrypt(textToEnctyp);
            string decryptedText = cipher.Decrypt(encryptedText, "asd");

            decryptedText.Should().NotBe(textToEnctyp);
        }
    }
}

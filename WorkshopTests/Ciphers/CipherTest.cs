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
    public class CipherTest
    {
        [Fact]

        public void CeasarCipherTest()
        {
            CipherValidator validator = new CipherValidator();
            CipherDataModel model = new CipherDataModel()
            { 
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            };
            string textToEnctyp = "Chuj";

            CaesarCipher cipher = new(model, validator);

            string encryptedText = cipher.Encrypt(textToEnctyp);
            string decryptedText = cipher.Decrypt(encryptedText);

            decryptedText.Should().Be(textToEnctyp);
        }
        [Fact]

        public void VigenereCipherTest()
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
            string decryptedText = cipher.Decrypt(encryptedText);

            decryptedText.Should().Be(textToEnctyp);
        }
    }
}

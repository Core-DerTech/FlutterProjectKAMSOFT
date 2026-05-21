using FluentAssertions;
using FlutterProjectKAMSOFT.Ciphers.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.Ciphers;
using FlutterProjectKAMSOFT.Encryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopTests.Ciphers
{
    public class CaesarCipherTest
    {
        [Fact]

        public void CeasarCipherShouldBeCorrect()
        {
            CipherValidator validator = new CipherValidator();
            CipherRequest model = new CipherRequest()
            {
                Text = "Text to encrypt",
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Key = "qwer",
                Shift = 4

            };
            string textToEnctyp = "Chuj";

            CaesarCipher cipher = new(validator);

            string encryptedText = cipher.Encrypt(model);
            string decryptedText = cipher.Decrypt(model);

            decryptedText.Should().Be(textToEnctyp);
        }
    }
}

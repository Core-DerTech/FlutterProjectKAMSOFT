using FluentAssertions;
using FlutterProjectKAMSOFT.Ciphers.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.Ciphers;
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
    }
}

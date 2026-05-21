using FluentAssertions;
using FlutterProjectKAMSOFT.Encryption.Ciphers;
using FlutterProjectKAMSOFT.Encryption.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.Models;

namespace WorkshopTests.Ciphers
{
    public class CaesarCipherTest
    {
        [Fact]
        public void CeasarCipherShouldBeCorrect()
        {
            CaesarRequestValidator validator = new CaesarRequestValidator();
            CipherRequestCaesar model = new CipherRequestCaesar()
            {
                Text = "Text to encrypt",
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Shift = 4
            };
            string textToEnctyp = "Cebularzozerca";

            CaesarCipher cipher = new(validator);

            string encryptedText = cipher.Encrypt(model);
            string decryptedText = cipher.Decrypt(model);

            decryptedText.Should().Be(textToEnctyp);
        }

        [Fact]
        public void CeasarCipherShouldNotBeCorrect()
        {
            CaesarRequestValidator validator = new CaesarRequestValidator();
            CipherRequestCaesar model = new CipherRequestCaesar()
            {
                Text = "Text to encrypt",
                Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                Shift = 4
            };
            string textToEnctyp = "Cebularzozerca";

            CaesarCipher cipher = new(validator);

            string encryptedText = cipher.Encrypt(model);

            encryptedText.Should().NotBe(textToEnctyp);
        }
    }
}

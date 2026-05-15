using FluentAssertions;
using FlutterProjectKAMSOFT.Encryption.Ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopTests.Ciphers
{
    public class RSAEncryptionTest
    {
        [Fact]
        public void EncryptAndDecryptShouldeturn_OriginalText()
        {
            RSAEncryption rsa = new();

            string text = "Hello Word";

            string encrypted = rsa.Encrypt(text);

            string decrypted = rsa.Decrypt(encrypted);

            decrypted.Should().Be(text);
        }

        [Fact]
        public void EncryptShouldReturnDifferentValueThanInput()
        {
            RSAEncryption rsa = new();

            string text = "JohnSmith";

            string encrypted = rsa.Encrypt(text);

            encrypted.Should().NotBe(text);
        }

        [Fact]
        public void GenerateHashShouldReturnValidHash()
        {
            RSAEncryption rsa = new();

            string text = "Hello Word";

            string hash = rsa.GenerateHash(text);

            hash.Should().NotBeNullOrWhiteSpace();
            hash.Length.Should().Be(64);
        }

        [Fact]
        public void VerifyHashShouldReturnTrueForValidData()
        {
            RSAEncryption rsa = new();

            string text = "JohnSmith";

            string hash = rsa.GenerateHash(text);

            bool result = rsa.VerifyHash(text, hash);

            result.Should().BeTrue();
        }

        [Fact]
        public void VerifyHashShouldReturnFalseFo_InvalidData()
        {
            RSAEncryption rsa = new();

            string text = "JohnSmith";

            string hash = rsa.GenerateHash(text);

            bool result = rsa.VerifyHash("DifferentText", hash);

            result.Should().BeFalse();
        }

        [Fact]
        public void SignDataAndVerifySignatureShouldReturnTrue()
        {
            RSAEncryption rsa = new();

            string text = "Hello Word";

            string signature = rsa.SignData(text);

            bool result = rsa.VerifySignature(text, signature);

            result.Should().BeTrue();
        }

        [Fact]
        public void VerifySignatureShouldReturnFalseForModifiedText()
        {
            RSAEncryption rsa = new();

            string text = "JohnSmith";

            string signature = rsa.SignData(text);

            bool result = rsa.VerifySignature("ModifiedText", signature);

            result.Should().BeFalse();
        }
    }
}

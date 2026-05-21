using FluentAssertions;
using FlutterProjectKAMSOFT.Encryption.Ciphers;
using FlutterProjectKAMSOFT.Encryption.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.Models;
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
            EncryptionRequestValidator validator = new EncryptionRequestValidator();
            ChipherTextRequest model = new ChipherTextRequest()
            {
                Text = "Text to encrypt"
            };
            string text = model.Text;
            RSAEncryption rsa = new(validator);
            string encrypted = rsa.Encrypt(model);
            string decrypted = rsa.Decrypt(model);
            decrypted.Should().Be(text);
        }

        [Fact]
        public void EncryptShouldReturnDifferentValueThanInput()
        {
            EncryptionRequestValidator validator = new EncryptionRequestValidator();
            ChipherTextRequest model = new ChipherTextRequest()
            {
                Text = "Text to encrypt"
            };
            RSAEncryption rsa = new(validator);
            string encrypted = rsa.Encrypt(model);
            encrypted.Should().NotBe(model.Text);
        }

        [Fact]
        public void GenerateHashShouldReturnValidHash()
        {
            EncryptionRequestValidator validator = new EncryptionRequestValidator();
            ChipherTextRequest model = new ChipherTextRequest()
            {
                Text = "Text to encrypt"
            };
            RSAEncryption rsa = new(validator);

            string hash = rsa.GenerateHash(model);
            hash.Should().NotBeNullOrWhiteSpace();
            hash.Length.Should().Be(64);
        }

        [Fact]
        public void VerifyHashShouldReturnTrueForValidData()
        {
            EncryptionRequestValidator validator = new EncryptionRequestValidator();
            ChipherTextRequest model = new ChipherTextRequest()
            {
                Text = "Text to encrypt"
            };
            RSAEncryption rsa = new(validator);
            string hash = rsa.GenerateHash(model);
            bool result = rsa.VerifyHash(model, hash);
            result.Should().BeTrue();
        }

        [Fact]
        public void VerifyHashShouldReturnFalseFo_InvalidData()
        {
            EncryptionRequestValidator validator = new EncryptionRequestValidator();
            ChipherTextRequest model = new ChipherTextRequest()
            {
                Text = "Text to encrypt"
            };
            RSAEncryption rsa = new(validator);
            string hash = rsa.GenerateHash(model);
            bool result = rsa.VerifyHash(model, hash);
            result.Should().BeFalse();
        }

        [Fact]
        public void SignDataAndVerifySignatureShouldReturnTrue()
        {
            EncryptionRequestValidator validator = new EncryptionRequestValidator();
            ChipherTextRequest model = new ChipherTextRequest()
            {
                Text = "Text to encrypt"
            };
            RSAEncryption rsa = new(validator);

            string signature = rsa.SignData(model);
            bool result = rsa.VerifySignature(model, signature);
            result.Should().BeTrue();
        }

        [Fact]
        public void VerifySignatureShouldReturnFalseForModifiedText()
        {
            EncryptionRequestValidator validator = new EncryptionRequestValidator();
            ChipherTextRequest model = new ChipherTextRequest()
            {
                Text = "Text to encrypt"
            };
            RSAEncryption rsa = new(validator);

            string signature = rsa.SignData(model);
            bool result = rsa.VerifySignature(model, signature);
            result.Should().BeFalse();
        }
    }
}

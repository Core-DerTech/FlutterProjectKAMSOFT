using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.CipherFactory;
using FlutterProjectKAMSOFT.Encryption.Models;
using System.Security.Cryptography;
using System.Text;

namespace FlutterProjectKAMSOFT.Encryption.Ciphers
{
    public class RSAEncryption : ICipher<ChipherTextRequest>
    {
        private readonly RSA _rsa;
        private const int KEY_PAIR = 2048;
        public string PublicKey { get; }
        public string PrivateKey { get; }
        private IValidator<ChipherTextRequest> _validator;

        public RSAEncryption(IValidator<ChipherTextRequest> validator)
        {
            _validator = validator;
            _rsa = RSA.Create(KEY_PAIR);

            PublicKey = Convert.ToBase64String(
                _rsa.ExportRSAPublicKey()
            );

            PrivateKey = Convert.ToBase64String(
                _rsa.ExportRSAPrivateKey()
            );
        }

        public string Encrypt(ChipherTextRequest request)
        {
            _validator.ValidateAndThrow(request);
            byte[] data = Encoding.UTF8.GetBytes(request.Text);

            byte[] encryptedData = _rsa.Encrypt(
                data,
                RSAEncryptionPadding.OaepSHA256
            );

            return Convert.ToBase64String(encryptedData);
        }

        public string Decrypt(ChipherTextRequest request)
        {
            _validator.ValidateAndThrow(request);

            byte[] encryptedData;

            try
            {
                encryptedData = Convert.FromBase64String(request.Text);
            }
            catch
            {
                throw new ArgumentException("Encrypted text is not valid Base64");
            }

            byte[] decryptedData = _rsa.Decrypt(
                encryptedData,
                RSAEncryptionPadding.OaepSHA256
            );

            return Encoding.UTF8.GetString(decryptedData);
        }

        public string GenerateHash(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be null or empty");

            byte[] hash = SHA256.HashData(
                Encoding.UTF8.GetBytes(text)
            );

            return Convert.ToHexString(hash);
        }

        public bool VerifyHash(string text, string expectedHash)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be null or empty");

            if (string.IsNullOrWhiteSpace(expectedHash))
                throw new ArgumentException("Hash cannot be null or empty");

            string currentHash = GenerateHash(text);

            return currentHash.Equals(
                expectedHash,
                StringComparison.OrdinalIgnoreCase
            );
        }

        public string SignData(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be null or empty");

            byte[] data = Encoding.UTF8.GetBytes(text);

            byte[] signature = _rsa.SignData(
                data,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1
            );

            return Convert.ToBase64String(signature);
        }

        public bool VerifySignature(string text, string signature)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be null or empty");

            if (string.IsNullOrWhiteSpace(signature))
                throw new ArgumentException("Signature cannot be null or empty");

            byte[] data = Encoding.UTF8.GetBytes(text);

            byte[] signatureBytes;

            try
            {
                signatureBytes = Convert.FromBase64String(signature);
            }
            catch
            {
                throw new ArgumentException("Signature is not valid Base64");
            }

            return _rsa.VerifyData(
                data,
                signatureBytes,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1
            );
        }
    }
}
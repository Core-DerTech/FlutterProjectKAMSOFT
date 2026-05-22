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
        private const int OAEP_SHA256_HASH_SIZE_BYTES = 32;
        private const char ENCRYPTED_CHUNK_SEPARATOR = '.';
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
            int maxChunkSize = (_rsa.KeySize / 8) - (2 * OAEP_SHA256_HASH_SIZE_BYTES) - 2;

            if (data.Length <= maxChunkSize)
            {
                byte[] encryptedData = _rsa.Encrypt(
                    data,
                    RSAEncryptionPadding.OaepSHA256
                );

                return Convert.ToBase64String(encryptedData);
            }

            return string.Join(
                ENCRYPTED_CHUNK_SEPARATOR,
                data.Chunk(maxChunkSize)
                    .Select(chunk => Convert.ToBase64String(_rsa.Encrypt(
                        chunk,
                        RSAEncryptionPadding.OaepSHA256
                    )))
            );
        }

        public string Decrypt(ChipherTextRequest request)
        {
            _validator.ValidateAndThrow(request);
            string[] encryptedChunks = request.Text.Split(ENCRYPTED_CHUNK_SEPARATOR);
            using MemoryStream decryptedData = new MemoryStream();

            foreach (string encryptedChunk in encryptedChunks)
            {
                byte[] encryptedData;

                try
                {
                    encryptedData = Convert.FromBase64String(encryptedChunk);
                }
                catch
                {
                    throw new ArgumentException("Encrypted text is not valid Base64");
                }

                byte[] decryptedChunk = _rsa.Decrypt(
                    encryptedData,
                    RSAEncryptionPadding.OaepSHA256
                );

                decryptedData.Write(decryptedChunk);
            }

            return Encoding.UTF8.GetString(decryptedData.ToArray());
        }

        public string GenerateHash(ChipherTextRequest request)
        {
            _validator.ValidateAndThrow(request);

            byte[] hash = SHA256.HashData(
                Encoding.UTF8.GetBytes(request.Text)
            );

            return Convert.ToHexString(hash);
        }

        public bool VerifyHash(ChipherTextRequest request, string expectedHash)
        {
            _validator.ValidateAndThrow(request);

            if (string.IsNullOrWhiteSpace(expectedHash))
                throw new ArgumentException("Hash cannot be null or empty");

            string currentHash = GenerateHash(request);

            return currentHash.Equals(
                expectedHash,
                StringComparison.OrdinalIgnoreCase
            );
        }

        public string SignData(ChipherTextRequest request)
        {
            _validator.ValidateAndThrow(request);
            byte[] data = Encoding.UTF8.GetBytes(request.Text);

            byte[] signature = _rsa.SignData(
                data,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1
            );

            return Convert.ToBase64String(signature);
        }
        
        public bool VerifySignature(ChipherTextRequest request, string signature)
        {
           _validator.ValidateAndThrow(request);

            if (string.IsNullOrWhiteSpace(signature))
                throw new ArgumentException("Signature cannot be null or empty");

            byte[] data = Encoding.UTF8.GetBytes(request.Text);

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

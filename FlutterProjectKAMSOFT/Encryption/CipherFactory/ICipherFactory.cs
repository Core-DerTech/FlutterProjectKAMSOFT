using FlutterProjectKAMSOFT.Encryption.Ciphers;

namespace FlutterProjectKAMSOFT.Encryption.CipherFactory
{
    public class CipherFactory
    {
        private readonly CaesarCipher _caesar;
        private readonly VigenereCipher _vigenere;
        private readonly RSAEncryption _rsa;
        private readonly SHAEncription _sha;

        public CipherFactory(
            CaesarCipher caesar,
            VigenereCipher vigenere,
            RSAEncryption rsa,
            SHAEncription sha)
        {
            _caesar = caesar;
            _vigenere = vigenere;
            _rsa = rsa;
            _sha = sha;
        }

        public object CreateCipher(CipherType cipherType)
            => cipherType switch
            {
                CipherType.Caesar => _caesar,
                CipherType.Vigenere => _vigenere,
                CipherType.RSAEncryption => _rsa,
                CipherType.SHA1Encryption => _sha,
                _ => throw new ArgumentException("No cipher was provided")
            };
    }
}
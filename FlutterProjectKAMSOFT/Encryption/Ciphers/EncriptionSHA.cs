using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.CipherFactory;
using FlutterProjectKAMSOFT.Encryption.Models;
using System.Security.Cryptography;
using System.Text;

namespace FlutterProjectKAMSOFT.Encryption.Ciphers
{
    public class EncriptionSHA : ICipher<ChipherTextRequest>
    {
        private IValidator<ChipherTextRequest> _validator;
        public EncriptionSHA(IValidator<ChipherTextRequest> validator)
        {
            _validator = validator;
        }

        public string Decrypt(ChipherTextRequest request)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(ChipherTextRequest request)
        {
            _validator.ValidateAndThrow(request);
            byte[] hash = SHA256.HashData(
                Encoding.UTF8.GetBytes(request.Text)
            );
            return Convert.ToHexString(hash);
        }
    }
}

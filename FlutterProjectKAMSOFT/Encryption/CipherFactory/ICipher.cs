using FlutterProjectKAMSOFT.Encryption.Models;

namespace FlutterProjectKAMSOFT.Encryption.CipherFactory
{
    public interface ICipher<T> where T : ChipherTextRequest
    {
        string Encrypt(T request);
        string Decrypt(T request);
    }
}

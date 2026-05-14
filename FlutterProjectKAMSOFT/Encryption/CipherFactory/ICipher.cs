namespace FlutterProjectKAMSOFT.Encryption.CipherFactory
{
    public interface ICipher
    {
        string Encrypt(CipherRequest request);

        string Decrypt(CipherRequest request);
    }
}

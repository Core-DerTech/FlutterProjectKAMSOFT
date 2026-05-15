namespace FlutterProjectKAMSOFT.Encryption.CipherFactory
{
    public interface ICipherFactory
    {
        ICipher Create(CipherType type);
    }
}

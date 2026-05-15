namespace FlutterProjectKAMSOFT.Encryption.CipherFactory
{
    public class CipherRequest
    {
        public required string Text { get; set; } 
        public string Key { get; set; } = string.Empty;
        public string Alphabet { get; set; } = string.Empty;
        public int? Shift { get; set; }
        public CipherType CipherType { get; set; }

    }
}

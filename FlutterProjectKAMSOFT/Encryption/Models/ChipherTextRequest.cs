using FlutterProjectKAMSOFT.Encryption.CipherFactory;

namespace FlutterProjectKAMSOFT.Encryption.Models
{
    public class ChipherTextRequest
    {
        public required string Text { get; set; }
        public string Alphabet { get; set; } = string.Empty;
    }
}

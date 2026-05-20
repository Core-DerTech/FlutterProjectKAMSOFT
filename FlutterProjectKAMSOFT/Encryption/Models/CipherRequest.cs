using FlutterProjectKAMSOFT.Encryption.CipherFactory;

namespace FlutterProjectKAMSOFT.Encryption.Models
{
    public class CipherRequest
    {
        public required string Text { get; set; } 
        public string Alphabet { get; set; } = string.Empty;
        public string? Key { get; set; } = string.Empty;
        public int? Shift { get; set; }
        public CipherType CipherType { get; set; }

    }
}

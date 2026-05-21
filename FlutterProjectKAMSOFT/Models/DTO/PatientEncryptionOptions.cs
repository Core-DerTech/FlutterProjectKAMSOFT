using FlutterProjectKAMSOFT.Encryption.CipherFactory;

namespace FlutterProjectKAMSOFT.Models.DTO
{
    public class PatientEncryptionOptions
    {
        public CipherType CipherType { get; set; }
        public string Alphabet { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -:/.";
        public string Key { get; set; } = "MEDICALKEY";
        public int Shift { get; set; } = 4;
    }
}

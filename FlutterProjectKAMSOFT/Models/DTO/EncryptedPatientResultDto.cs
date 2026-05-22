using FlutterProjectKAMSOFT.Encryption.CipherFactory;

namespace FlutterProjectKAMSOFT.Models.DTO
{
    public record EncryptedPatientResultDto(
        CipherType CipherType,
        string PatientName,
        string FormattedValue,
        string StatusColor,
        bool IsCritical
    );
}

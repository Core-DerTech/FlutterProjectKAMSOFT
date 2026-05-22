using FlutterProjectKAMSOFT.Encryption.CipherFactory;

namespace FlutterProjectKAMSOFT.Models.DTO
{
    public record EncryptedPatientDto(
        CipherType CipherType,
        EncryptedPatientNameDto Name,
        string Pessel,
        string DateOfBirth,
        string DiseaseDescription,
        IReadOnlyList<EncryptedAppointmentDto> Appointments
    );

    public record EncryptedPatientNameDto(
        string FirstName,
        string LastName
    );
}

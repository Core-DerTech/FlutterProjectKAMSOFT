using System.Globalization;
using FlutterProjectKAMSOFT.Encryption.CipherFactory;
using FlutterProjectKAMSOFT.Encryption.Models;
using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Models.DTO;

namespace FlutterProjectKAMSOFT.Services
{
    public class PatientDataEncryptionService
    {
        private readonly CipherFactory _cipherFactory;

        public PatientDataEncryptionService(CipherFactory cipherFactory)
        {
            _cipherFactory = cipherFactory;
        }

        public EncryptedPatientDto EncryptPatient(Patient patient, PatientEncryptionOptions options)
        {
            return new EncryptedPatientDto(
                CipherType: options.CipherType,
                Name: new EncryptedPatientNameDto(
                    FirstName: EncryptValue(patient.Name.FirstName, options),
                    LastName: EncryptValue(patient.Name.LastName, options)
                ),
                Pessel: EncryptValue(patient.PESSEL.ToString(CultureInfo.InvariantCulture), options),
                DateOfBirth: EncryptValue(patient.DateOfBirth.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), options),
                DiseaseDescription: EncryptValue(patient.GetPatientDisease(), options),
                Appointments: patient.Appointments
                    .Select(appointment => new EncryptedAppointmentDto(
                        AppointmentDate: EncryptValue(appointment.AppointmentDate.ToString("O", CultureInfo.InvariantCulture), options),
                        Description: EncryptValue(appointment.Description, options),
                        Title: EncryptValue(appointment.Title, options),
                        Type: EncryptValue(appointment.Type, options)
                    ))
                    .ToList()
            );
        }

        public EncryptedPatientResultDto EncryptPatientResult(PatientResultDto patientResult, PatientEncryptionOptions options)
        {
            return new EncryptedPatientResultDto(
                CipherType: options.CipherType,
                PatientName: EncryptValue(patientResult.PatientName, options),
                FormattedValue: EncryptValue(patientResult.FormattedValue, options),
                StatusColor: patientResult.StatusColor,
                IsCritical: patientResult.IsCritical
            );
        }

        private string EncryptValue(string value, PatientEncryptionOptions options)
        {
            object cipher = _cipherFactory.CreateCipher(options.CipherType);

            return options.CipherType switch
            {
                CipherType.Caesar => ((ICipher<CipherRequestCaesar>)cipher).Encrypt(new CipherRequestCaesar
                {
                    Text = value,
                    Alphabet = options.Alphabet,
                    Shift = options.Shift
                }),
                CipherType.Vigenere => ((ICipher<CipherRequestVigenere>)cipher).Encrypt(new CipherRequestVigenere
                {
                    Text = value,
                    Alphabet = options.Alphabet,
                    Key = options.Key
                }),
                CipherType.RSAEncryption => ((ICipher<ChipherTextRequest>)cipher).Encrypt(new ChipherTextRequest
                {
                    Text = value
                }),
                CipherType.SHA1Encryption => ((ICipher<ChipherTextRequest>)cipher).Encrypt(new ChipherTextRequest
                {
                    Text = value
                }),
                _ => throw new ArgumentException("No cipher was provided")
            };
        }
    }
}

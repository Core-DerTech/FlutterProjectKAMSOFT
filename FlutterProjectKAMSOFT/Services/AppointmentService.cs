using FlutterProjectKAMSOFT.Models;
using System.Text;

namespace FlutterProjectKAMSOFT.Services
{
    public class AppointmentService
    {
        public AppointmentService()
        {
        }

        public List<Appointment> GetAllAppointments(Patient patient)
            => patient.Appointments;

        public void AddAppointment(Patient patient, Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentException("Please provide a proper appointment");
            }
            patient.Appointments.Add(appointment);
        }

        public void GetPacientData(Patient patient)
        {
            patient.WritePatientData();
        }
        public string GetPatientDiseaseDescription(Patient patient)
        {
            return patient.GetPatientDisease();
        }
        public string GetAllPatientData(Patient patient)
        { 
           return new StringBuilder()
                .AppendLine($"Patient name: {patient.Name.FirstName} {patient.Name.LastName}")
                .AppendLine($"Patient PESEL: {patient.PESSEL}")
                .AppendLine($"Patient date of birth: {patient.DateOfBirth}")
                .AppendLine($"Patient disease: {patient.GetPatientDisease()}")
                .ToString();
        }
    }
}
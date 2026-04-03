using FlutterProjectKAMSOFT.Patterns.Strategy;

namespace FlutterProjectKAMSOFT.Models
{
    public class Patient
    {
        public PatientName Name { get; set; }
        public long PESSEL { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required List<Appointment> Appointments { get; set; }
        public IDiseaseStrategy Disease { get;  set; }
        public void WritePatientData()
        {
            Console.WriteLine($"First name: {Name.FirstName}");
            Console.WriteLine($"Last name: {Name.LastName}");
            Console.WriteLine($"PESSEL: {PESSEL}");
            Console.WriteLine($"Date of birth: {DateOfBirth.ToString()}");

            Console.WriteLine("______________________________________________________");
            for (int i = 0; i < Appointments.Count; i++)
            {
                Console.WriteLine($"Appointment {i + 1}:");
                Console.WriteLine($"Title: {Appointments[i].Title}");
                Console.WriteLine($"Description: {Appointments[i].Description}");
                Console.WriteLine($"Type: {Appointments[i].Type}");
                Console.WriteLine($"Date: {Appointments[i].AppointmentDate.ToString()}");
            }
        }
        public void SetDisease(IDiseaseStrategy disease)
        {
            Disease = disease;
        }
        public string GetPatientDisease()
        {
            if (Disease == null)
            {
                return "No disease assigned to the patient.";
            }
            return Disease.GetDiseaseDescription();
        }
    }
}

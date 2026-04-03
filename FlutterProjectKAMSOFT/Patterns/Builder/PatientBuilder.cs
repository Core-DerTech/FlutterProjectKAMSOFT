using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Patterns.Factory;
using FlutterProjectKAMSOFT.Patterns.Strategy;

namespace FlutterProjectKAMSOFT.Patterns.Builder
{
    public class PatientBuilder
    {
        private PatientName _name;
        private long _pessel;
        private DateOnly _dateOfBirth;
        private List<Appointment> _appointments;
        private IDiseaseStrategy _disease;
        public PatientBuilder WithName( PatientName name)
        {
            if (string.IsNullOrWhiteSpace(name.FirstName) || string.IsNullOrWhiteSpace(name.LastName))
            {
                throw new ArgumentException("Please provide a proper name");
            }
            _name = name;

            return this;
        }

        public PatientBuilder WithPessel(long pessel)
        {
            if (pessel <= 0)
            {
                throw new ArgumentException("Please provide a proper pessel");
            }
            _pessel = pessel;
            return this;
        }

        public PatientBuilder WithDateOfBirth(DateOnly dateOfBirth)
        {
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Please provide a proper date of birth");
            }
            _dateOfBirth = dateOfBirth;
            return this;
        }
        public PatientBuilder WithAppointments(List<Appointment> appointments)
        {
            if (appointments == null)
            {
                _appointments = new();
            }
            else
            {
                _appointments = appointments;
            }
            return this;

        }
        public PatientBuilder WithDiseaseDescription(DiseaseClassification disease)
        {
            var factory = new DiseaseFactory();
            _disease = factory.Create(disease);
            return this;
        }
        public Patient Build()
        {
            return new Patient
            {
                Name = _name,
                PESSEL = _pessel,
                DateOfBirth = _dateOfBirth,
                Appointments = _appointments ?? new List<Appointment>(),
                Disease = _disease
            };
        }
    }
}

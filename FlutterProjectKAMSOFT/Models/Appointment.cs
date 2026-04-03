namespace FlutterProjectKAMSOFT.Models
{
    public class Appointment
    {
        public DateTime AppointmentDate { get; set; }
        public required string Description { get; set; }
        public required string Title { get; set; }
        public required string Type { get; set; }
    }
}

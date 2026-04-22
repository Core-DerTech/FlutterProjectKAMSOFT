namespace FlutterProjectKAMSOFT.Patterns.ProjectionDDD
{
    public class PatientSettlementsProjection
    {
        public required Guid PatientId { get; set; }
        public int SettledServiceCount { get; private set; }
        public double TotalGrossAmount { get; private set; }
        public DateTimeOffset LastSettledAt { get; private set; }

        public static PatientSettlementsProjection FromServiceSettledEvent(ServiceSettledEvent @event)
        {
            return new PatientSettlementsProjection
            {
                PatientId = @event.PatientId,
                SettledServiceCount = 1,
                TotalGrossAmount = (double)@event.GrossAmmount,
                LastSettledAt = @event.SettledAt
            };
        }
        public static PatientSettlementsProjection CreateEmpty(Guid patientId) => new PatientSettlementsProjection
        {
            PatientId = patientId,
            SettledServiceCount = 0,
            TotalGrossAmount = 0,
            LastSettledAt = DateTimeOffset.MinValue
        };

        public void Apply(ServiceSettledEvent @event)
        {
            if (@event.PatientId != PatientId)
            {
                throw new InvalidOperationException("Event patient ID does not match projection patient ID.");
            }
            SettledServiceCount++;
            TotalGrossAmount += (double)@event.GrossAmmount;
            if (@event.SettledAt > LastSettledAt)
            {
                LastSettledAt = @event.SettledAt;
            }
        }
    }
}

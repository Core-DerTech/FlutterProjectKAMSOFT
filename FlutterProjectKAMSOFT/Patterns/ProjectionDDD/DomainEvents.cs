namespace FlutterProjectKAMSOFT.Patterns.ProjectionDDD
{
    public abstract record DomainEvents;
    public record ServiceSettledEvent(Guid PatientId, string ServiceCode, decimal GrossAmmount, DateTimeOffset SettledAt) : DomainEvents;
}

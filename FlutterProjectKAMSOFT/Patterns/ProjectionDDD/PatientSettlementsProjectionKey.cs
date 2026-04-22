namespace FlutterProjectKAMSOFT.Patterns.ProjectionDDD
{
    public readonly record struct PatientSettlementsProjectionKey(Guid PatientId)
    {
        public static PatientSettlementsProjectionKey FromServiceSettledEvent(ServiceSettledEvent @event) => new PatientSettlementsProjectionKey(@event.PatientId);
    }
}

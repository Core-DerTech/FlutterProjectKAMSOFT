namespace FlutterProjectKAMSOFT.Patterns.ProjectionDDD
{
    public class ServiceSettledProjectionHandler
    {
        public void Handle(ServiceSettledEvent @event)
        {
            var projectionKey = PatientSettlementsProjectionKey.FromServiceSettledEvent(@event);
            var store = new Store();

            var projection = store.Get<PatientSettlementsProjection>(projectionKey)
                          ?? PatientSettlementsProjection.CreateEmpty(@event.PatientId);

            projection.Apply(@event);
            store.Save(projection, projectionKey);
        }
    }
}
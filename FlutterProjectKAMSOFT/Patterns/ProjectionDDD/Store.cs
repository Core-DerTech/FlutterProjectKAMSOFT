namespace FlutterProjectKAMSOFT.Patterns.ProjectionDDD
{
    public class Store
    {
        private readonly Dictionary<(Type Type, object Key), object> _projections = new();

        public void Save<TProjection>(TProjection projection, object key) where TProjection : class
        {
            var cacheKey = (typeof(TProjection), key);
            _projections[cacheKey] = projection;

            Console.WriteLine($"The projection of the type: {typeof(TProjection).Name} has been saved");
        }

        public TProjection? Get<TProjection>(object key) where TProjection : class
        {
            var cacheKey = (typeof(TProjection), key);

            if (_projections.TryGetValue(cacheKey, out var value))
            {
                return value as TProjection;
            }

            return null;
        }

    }
}

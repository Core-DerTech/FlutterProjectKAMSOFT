using FlutterProjectKAMSOFT.Processing.Filters;

namespace FlutterProjectKAMSOFT.Processing
{
    public class Pipe<T>
    {
        private readonly List<IFilter<T>> _filters = new();

        public Pipe<T> AddFilter(IFilter<T> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public T Execute(T input)
        {
            return _filters.Aggregate(input, (current, filter) => filter.Execute(current));
        }
    }
}
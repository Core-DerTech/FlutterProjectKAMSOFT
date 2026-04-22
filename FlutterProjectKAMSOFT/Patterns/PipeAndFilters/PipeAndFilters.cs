namespace FlutterProjectKAMSOFT.Patterns.PipeAndFilters
{
    public class Pipe<T>
    {
        private readonly List<IFilter<T>> _filters = new List<IFilter<T>>();

        public Pipe<T> AddFilter(IFilter<T> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public T Execute(T input)
        {
            T result = input;

            foreach (var filter in _filters)
            {
                result = filter.Execute(result);
            }

            return result;
        }
    }

    public interface IFilter<T>
    {
        T Execute(T input);
    }

    public class TrimFilter : IFilter<string>
    {
        public string Execute(string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : input.Trim();
        }
    }

    public class ToUpperFilter : IFilter<string>
    {
        public string Execute(string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : input.ToUpper();
        }
    }

    public class PipeAndFilterExample
    {
        public void Run()
        {
            var pipe = new Pipe<string>()
                .AddFilter(new TrimFilter())
                .AddFilter(new ToUpperFilter());

            string input = "  hello world  ";
            string result = pipe.Execute(input);

            Console.WriteLine(result);
        }
    }
}

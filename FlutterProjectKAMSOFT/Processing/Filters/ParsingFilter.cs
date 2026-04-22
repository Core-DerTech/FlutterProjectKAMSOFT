using FlutterProjectKAMSOFT.JSON;
using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Processing.Filters.Parsers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlutterProjectKAMSOFT.Processing.Filters
{
    public class ParsingFilter : IFilter<ProcessingContext>
    {
        private readonly Dictionary<string, ILabParser> _parsers;
        public ParsingFilter(IEnumerable<ILabParser> parsers)
            => _parsers = parsers.ToDictionary(p => p.SupportedLab);

        public ProcessingContext Execute(ProcessingContext context)
        {
            if (_parsers.TryGetValue(context.SourceLab, out var parser))
                context.NormalizedResult = parser.Parse(context.RawInput);
            return context;
        }
    }
}

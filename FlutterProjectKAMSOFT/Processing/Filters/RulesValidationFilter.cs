namespace FlutterProjectKAMSOFT.Processing.Filters
{
    public class RulesValidationFilter : IFilter<ProcessingContext>
    {
        private readonly IEnumerable<MedicalRule> _rules;
        public RulesValidationFilter(IEnumerable<MedicalRule> rules) => _rules = rules;

        public ProcessingContext Execute(ProcessingContext context)
        {
            var res = context.NormalizedResult;
            if (res == null) return context;

            var rule = _rules.FirstOrDefault(r => r.TestCode == res.TestCode);
            rule?.Evaluate(res.Value, context.Violations);

            return context;
        }
    }
}

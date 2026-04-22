namespace FlutterProjectKAMSOFT.Processing.Filters
{
    public class MedicalRule
    {
        public string TestCode { get; }
        private record Threshold(Func<decimal, bool> Condition, string Message, string Severity);

        private readonly List<Threshold> _thresholds = new();

        public MedicalRule(string testCode) => TestCode = testCode;

        public MedicalRule WhenGreater(decimal limit, string msg, string severity)
        {
            _thresholds.Add(new Threshold(val => val > limit, msg, severity));
            return this;
        }

        public MedicalRule WhenLess(decimal limit, string msg, string severity)
        {
            _thresholds.Add(new Threshold(val => val < limit, msg, severity));
            return this;
        }

        public void Evaluate(decimal value, List<RuleViolation> violations)
        {
            foreach (var t in _thresholds)
            {
                if (t.Condition(value))
                    violations.Add(new RuleViolation($"{TestCode}_VIOLATION", t.Message, t.Severity));
            }
        }
    }
}

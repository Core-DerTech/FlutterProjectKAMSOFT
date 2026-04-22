namespace FlutterProjectKAMSOFT.Processing
{
    public record RuleViolation(string Code, string Message, string Severity = "Warning")
    {
        public bool IsCritical => Severity.Equals("Critical", StringComparison.OrdinalIgnoreCase);
    }
}

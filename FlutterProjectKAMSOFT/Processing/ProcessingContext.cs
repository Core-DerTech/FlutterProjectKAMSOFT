using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Processing;

public class ProcessingContext
{
    public object RawInput { get; }
    public string SourceLab { get; }
    public MedicalTestResult? NormalizedResult { get; set; }
    public List<RuleViolation> Violations { get; } = new();

    public ProcessingContext(object rawInput, string sourceLab)
    {
        RawInput = rawInput;
        SourceLab = sourceLab;
    }
}
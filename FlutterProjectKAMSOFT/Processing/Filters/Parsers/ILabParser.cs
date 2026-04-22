using FlutterProjectKAMSOFT.Models;

namespace FlutterProjectKAMSOFT.Processing.Filters.Parsers
{
    public interface ILabParser
    {
        string SupportedLab { get; }
        MedicalTestResult Parse(object rawInput);
    }
}

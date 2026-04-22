using FlutterProjectKAMSOFT.JSON;
using FlutterProjectKAMSOFT.Models;

namespace FlutterProjectKAMSOFT.Processing.Filters.Parsers
{
    public class LabAParser : ILabParser
    {
        public string SupportedLab => "Lab_A";
        public MedicalTestResult Parse(object rawInput)
        {
            var data = JsonHelper.Deserialize<LabAResultDto>(rawInput);
            return new MedicalTestResult(data?.TestCode ?? "UNK", data?.TestName ?? "Unknown", data?.Result ?? 0, data?.Unit ?? "", SupportedLab);
        }
    }
}

using FlutterProjectKAMSOFT.Models;
using System.Globalization;

namespace FlutterProjectKAMSOFT.Processing.Filters.Parsers
{
    public class LabBParser : ILabParser
    {
        public string SupportedLab => "Lab_B";
        public MedicalTestResult Parse(object rawInput)
        {
            var text = rawInput?.ToString() ?? "";
            var parts = text.Split(';').Select(p => p.Split(':')).ToDictionary(kv => kv[0].Trim().ToUpper(), kv => kv[1].Trim());

            return new MedicalTestResult(
                parts["BADANIE"] == "CUKIER" ? "GLU" : "CHOL",
                parts["BADANIE"],
                decimal.Parse(parts["WYNIK"].Replace(',', '.'), CultureInfo.InvariantCulture),
                parts["JEDNOSTKA"],
                SupportedLab
            );
        }
    }
}

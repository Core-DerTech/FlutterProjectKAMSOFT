using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Processing.Filters;
using FlutterProjectKAMSOFT.Processing.Filters.Parsers;

namespace FlutterProjectKAMSOFT.Processing
{
    public class MedicalProcessingService
    {
        public ProcessingContext Run(string lab, object rawData)
        {
            var parsers = new List<ILabParser>
            { 
                new LabAParser(),
                new LabBParser() 
            };

            var rules = new List<MedicalRule> {
                new MedicalRule("GLU")
                .WhenGreater(126, "Hyperglycemia", "Critical")
                .WhenLess(70, "Hypoglycemia", "Warning"),
                new MedicalRule("CHOL").WhenGreater(200, "High Cholesterol", "Warning")
            };

            var pipeline = new Pipe<ProcessingContext>()
                .AddFilter(new ParsingFilter(parsers))
                .AddFilter(new RulesValidationFilter(rules));

            return pipeline.Execute(new ProcessingContext(rawData, lab));
        }
    }
}

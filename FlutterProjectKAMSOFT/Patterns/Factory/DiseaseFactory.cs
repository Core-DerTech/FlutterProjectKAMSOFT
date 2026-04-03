using FlutterProjectKAMSOFT.Models.Diseases;
using FlutterProjectKAMSOFT.Patterns.Strategy;

namespace FlutterProjectKAMSOFT.Patterns.Factory
{
    public class DiseaseFactory
    {
        public IDiseaseStrategy Create(DiseaseClassification type)
        {
            return type switch
            {
                DiseaseClassification.Asthma => new AsthmaDiseaseStrategy(),
                DiseaseClassification.Cancer => new CancerDiseaseStrategy(),
                _ => throw new ArgumentException("Invalid disease classification")
            };
        }
    }
}

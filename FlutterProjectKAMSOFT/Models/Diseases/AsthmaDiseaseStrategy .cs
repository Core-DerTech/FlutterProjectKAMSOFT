using FlutterProjectKAMSOFT.Patterns.Strategy;
using System.Text;

namespace FlutterProjectKAMSOFT.Models.Diseases
{
    public class AsthmaDiseaseStrategy : IDiseaseStrategy
    {
        public string GetDiseaseDescription() => new StringBuilder( "Asthma is a chronic respiratory condition characterized by inflammation and narrowing of the airways, leading to difficulty breathing. It can cause symptoms such as wheezing, coughing, chest tightness, and shortness of breath. Asthma can be triggered by various factors, including allergens, exercise, cold air, and respiratory infections. While there is no cure for asthma, " +
                                                 "it can be managed with medications and lifestyle adjustments to control symptoms and prevent asthma attacks.").ToString();
    }
}

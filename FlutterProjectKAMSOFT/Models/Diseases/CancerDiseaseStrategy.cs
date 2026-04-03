using FlutterProjectKAMSOFT.Patterns.Strategy;
using System.Text;

namespace FlutterProjectKAMSOFT.Models.Diseases
{
    public class CancerDiseaseStrategy : IDiseaseStrategy
    {
        public string GetDiseaseDescription() => new StringBuilder( "Cancer is a group of diseases involving abnormal cell growth with the potential to invade or spread to other parts of the body. " +
                                                                    "It can affect any part of the body and is often characterized by uncontrolled cell division and the ability to metastasize.").ToString();
    }
}

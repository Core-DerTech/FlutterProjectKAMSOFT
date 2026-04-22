namespace FlutterProjectKAMSOFT.Models
{
    public record MedicalTestResult(
     string TestCode,
     string TestName,
     decimal Value,
     string Unit,
     string SourceLab
 );
}

namespace FlutterProjectKAMSOFT.Models.DTO
{
    public record PatientResultDto(
          string PatientName,
          string FormattedValue, 
          string StatusColor,
          bool IsCritical
      );
}

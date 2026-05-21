using Microsoft.AspNetCore.Mvc;
using FlutterProjectKAMSOFT.Processing;
using FlutterProjectKAMSOFT.Models.DTO;
using FlutterProjectKAMSOFT.Services;

namespace FlutterProjectKAMSOFT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalResultsController : ControllerBase
    {
        private readonly MedicalProcessingService _processingService;
        private readonly PatientDataEncryptionService _encryptionService;

        private static readonly List<PatientResultDto> _database = new();

        public MedicalResultsController(PatientDataEncryptionService encryptionService)
        {
            _processingService = new MedicalProcessingService();
            _encryptionService = encryptionService;
        }

        [HttpGet("dashboard")]
        public ActionResult<List<EncryptedPatientResultDto>> GetDashboard([FromQuery] PatientEncryptionOptions encryptionOptions)
        {
            var encryptedDashboard = _database
                .Select(patientResult => _encryptionService.EncryptPatientResult(patientResult, encryptionOptions))
                .ToList();

            return Ok(encryptedDashboard);
        }

        [HttpPost("submit")]
        public ActionResult SubmitResult([FromBody] LabSubmissionRequest request)
        {
            try
            {
                var context = _processingService.Run(request.LabSource, request.RawData);

                var projection = MapToPatientDto(context, request.PatientName);
                _database.Add(projection);

                return Ok(new { message = "Data processed successfully", isCritical = projection.IsCritical });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private PatientResultDto MapToPatientDto(ProcessingContext context, string name)
        {
            var res = context.NormalizedResult;
            string color = "Green";
            bool critical = false;

            if (context.Violations.Any(v => v.IsCritical))
            {
                color = "Red";
                critical = true;
            }
            else if (context.Violations.Any())
            {
                color = "Yellow";
            }

            return new PatientResultDto(
                PatientName: name,
                FormattedValue: res != null ? $"{res.Value} {res.Unit}" : "Error",
                StatusColor: color,
                IsCritical: critical
            );
        }
    }

    public record LabSubmissionRequest(string LabSource, object RawData, string PatientName);
}

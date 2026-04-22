using Microsoft.AspNetCore.Mvc;
using FlutterProjectKAMSOFT.Processing;
using FlutterProjectKAMSOFT.Models.DTO;

namespace FlutterProjectKAMSOFT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalResultsController : ControllerBase
    {
        private readonly MedicalProcessingService _processingService;

        private static readonly List<PatientResultDto> _database = new();

        public MedicalResultsController()
        {
            _processingService = new MedicalProcessingService();
        }

        [HttpGet("dashboard")]
        public ActionResult<List<PatientResultDto>> GetDashboard()
        {
            return Ok(_database);
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
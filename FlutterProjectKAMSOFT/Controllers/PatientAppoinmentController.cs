using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Models.DTO;
using FlutterProjectKAMSOFT.Patterns;
using FlutterProjectKAMSOFT.Patterns.Builder;
using FlutterProjectKAMSOFT.Patterns.Factory;
using FlutterProjectKAMSOFT.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientAppoinmentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly PatientDataEncryptionService _encryptionService;

    public PatientAppoinmentController(IMediator mediator, PatientDataEncryptionService encryptionService)
    {
        _mediator = mediator;
        _encryptionService = encryptionService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<EncryptedPatientDto>> CreateAppointment(
        [FromBody] CreateAppointmentCommand command,
        [FromQuery] PatientEncryptionOptions encryptionOptions)
    {
        var result = await _mediator.Send(command);
        return Ok(_encryptionService.EncryptPatient(result, encryptionOptions));
    }
    [HttpGet("get-patient-data")]
    public ActionResult<List<EncryptedPatientDto>> GetPatientData(
        [FromQuery] long pessel,
        [FromQuery] PatientEncryptionOptions encryptionOptions)
    {
        var builder = new PatientBuilder();

        Patient patient = builder
            .WithName(new PatientName { FirstName = "Konrad", LastName = "Niderla" })
            .WithPessel(12345678901)
            .WithDateOfBirth(new DateOnly(1990, 5, 20))
            .WithDiseaseDescription(DiseaseClassification.Cancer)
            .Build(); 
        Patient patient1 = builder
            .WithName(new PatientName { FirstName = "Roman", LastName = "Nahirnyi" })
            .WithPessel(12345678901)
            .WithDateOfBirth(new DateOnly(1990, 5, 20))
            .WithDiseaseDescription(DiseaseClassification.Cancer)
            .Build(); 
        List<Patient> patients = new List<Patient> { patient, patient1 };
        var encryptedPatients = patients
            .Where(patientData => pessel == 0 || patientData.PESSEL == pessel)
            .Select(patientData => _encryptionService.EncryptPatient(patientData, encryptionOptions))
            .ToList();

        return Ok(encryptedPatients);
    }
}

using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Patterns;
using FlutterProjectKAMSOFT.Patterns.Builder;
using FlutterProjectKAMSOFT.Patterns.Factory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientAppoinmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientAppoinmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Patient>> CreateAppointment([FromBody] CreateAppointmentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("get-patient-data")]
    public ActionResult<Patient> GetPatientData([FromQuery] long pessel)
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
        return Ok(patients);
    }
}
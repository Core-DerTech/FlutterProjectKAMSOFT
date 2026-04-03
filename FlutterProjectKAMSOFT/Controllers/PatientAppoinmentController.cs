using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Patterns;
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
}
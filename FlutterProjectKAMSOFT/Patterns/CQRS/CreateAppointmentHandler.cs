using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Patterns;
using FlutterProjectKAMSOFT.Patterns.Builder;
using FlutterProjectKAMSOFT.Services;
using MediatR;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Patient>
{
    private readonly AppointmentService _appointmentService;

    public CreateAppointmentHandler(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public async Task<Patient> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var builder = new PatientBuilder();
        var patient = builder
            .WithName(new PatientName { FirstName = request.FirstName, LastName = request.LastName })
            .WithPessel(request.Pessel)
            .WithDateOfBirth(request.DateOfBirth)
            .WithDiseaseDescription(request.Disease)
            .Build();

        _appointmentService.AddAppointment(patient, new Appointment
        {
            AppointmentDate = DateTime.Now,
            Title = request.AppointmentTitle,
            Description = request.AppointmentDescription,
            Type = "General"
        });

        return await Task.FromResult(patient);
    }
}
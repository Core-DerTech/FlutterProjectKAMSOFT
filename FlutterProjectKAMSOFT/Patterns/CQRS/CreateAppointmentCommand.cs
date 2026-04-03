using MediatR;
using FlutterProjectKAMSOFT.Models;
using FlutterProjectKAMSOFT.Patterns.Factory;

namespace FlutterProjectKAMSOFT.Patterns
{
    public record CreateAppointmentCommand(
        string FirstName,
        string LastName,
        long Pessel,
        DateOnly DateOfBirth,
        DiseaseClassification Disease,
        string AppointmentTitle,
        string AppointmentDescription
    ) : IRequest<Patient>;
}
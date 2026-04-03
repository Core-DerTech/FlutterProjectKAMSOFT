using FluentValidation;
using FlutterProjectKAMSOFT.Patterns;

namespace FlutterProjectKAMSOFT.Validation
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is obligatory");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is obligatory");
            RuleFor(x => x.Pessel).GreaterThan(0).WithMessage("PESSEL has to be more than 0");
            RuleFor(x => x.DateOfBirth).LessThan(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("You cannot be borned tomottow, idiot");
        }
    }
}
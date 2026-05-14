using FluentValidation;

namespace FlutterProjectKAMSOFT.Ciphers.CipherValidation
{
    public class CipherValidator : AbstractValidator<CipherDataModel>
    {
        private const int MIN_ALPHABET_SYMBOLS_NUMBER = 20;
        private const int MIN_PASSWORD_LENGTH = 8;
        public CipherValidator()
        {
            RuleFor(x => x.Alphabet)
                .NotEmpty()
                .WithMessage("Empty input data")

                .MinimumLength(MIN_ALPHABET_SYMBOLS_NUMBER)
                .WithMessage($"Number of symbols cannot be less than {MIN_ALPHABET_SYMBOLS_NUMBER}");

            When(x => x.RequirePassword, () =>
            {

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("The password is empty")
                
                    .MinimumLength(MIN_PASSWORD_LENGTH)
                    .WithMessage($"The password has to contain at leats {MIN_PASSWORD_LENGTH} symbols");
            });
        }
    }
}

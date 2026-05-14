using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.CipherFactory;

namespace FlutterProjectKAMSOFT.Ciphers.CipherValidation
{
    public class CipherValidator : AbstractValidator<CipherRequest>
    {
        private const int MIN_ALPHABET_SYMBOLS_NUMBER = 20;
        private const int MIN_KEY_LENGTH = 8;
        private const int MIN_SHIFT_VALUE = 3;
        public CipherValidator()
        {
            RuleFor(x => x.Alphabet)
                .NotEmpty()
                .WithMessage("Empty input data")
                
                .MinimumLength(MIN_ALPHABET_SYMBOLS_NUMBER)
                .WithMessage($"Number of symbols cannot be less than {MIN_ALPHABET_SYMBOLS_NUMBER}")
                .Must(a => a.Distinct().Count() == a.Length)
                .WithMessage("Alphabet must contain unique characters"); ;



            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Empty input data");

            When(x => x.CipherType == CipherType.Vigenere, () =>
            {

                RuleFor(x => x.Key)
                    .NotEmpty()
                    .WithMessage("The key cannot be empty")
                
                    .MinimumLength(MIN_KEY_LENGTH)
                    .WithMessage($"The key has to contain at leats {MIN_KEY_LENGTH} symbols");
            });
            When(x => x.CipherType == CipherType.Caesar, () =>
            {

                RuleFor(x => x.Shift)
                    .NotEmpty()
                    .WithMessage("The shift cannot be empty")

                    .GreaterThan(MIN_SHIFT_VALUE)
                    .WithMessage($"The key has to contain at leats {MIN_KEY_LENGTH} symbols");
            });
        }
    }
}

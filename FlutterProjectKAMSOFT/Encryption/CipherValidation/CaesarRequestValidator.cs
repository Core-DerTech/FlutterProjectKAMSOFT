using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.Models;

namespace FlutterProjectKAMSOFT.Encryption.CipherValidation
{
    public class CaesarRequestValidator : TextCipherValidator<CipherRequestCaesar>
    {

        public CaesarRequestValidator()
        {
            RuleFor(x => x.Alphabet)
                .NotEmpty()
                .WithMessage("Empty input data")

                .MinimumLength(MIN_ALPHABET_SYMBOLS_NUMBER)
                .WithMessage($"Number of symbols cannot be less than {MIN_ALPHABET_SYMBOLS_NUMBER}")
                .Must(a => a.Distinct().Count() == a.Length)
                .WithMessage("Alphabet must contain unique characters");

            RuleFor(x => x.Shift)
                .NotEmpty()
                .WithMessage("The shift cannot be empty")

                .GreaterThan(MIN_SHIFT_VALUE)
                .WithMessage($"The key has to contain at leats {MIN_KEY_LENGTH} symbols");
        }
    }
}

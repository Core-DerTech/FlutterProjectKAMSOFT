using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.Models;

namespace FlutterProjectKAMSOFT.Encryption.CipherValidation
{
    public class VigenereRequestValidator : TextCipherValidator<CipherRequestVigenere>
    {
        public VigenereRequestValidator()
        {
            RuleFor(x => x.Alphabet)
            .NotEmpty()
            .WithMessage("Empty input data")

            .MinimumLength(MIN_ALPHABET_SYMBOLS_NUMBER)
            .WithMessage($"Number of symbols cannot be less than {MIN_ALPHABET_SYMBOLS_NUMBER}")
            .Must(a => a.Distinct().Count() == a.Length)
            .WithMessage("Alphabet must contain unique characters"); ;

            RuleFor(x => x.Key)
                    .NotEmpty()
                    .WithMessage("The key cannot be empty")

                    .MinimumLength(MIN_KEY_LENGTH)
                    .WithMessage($"The key has to contain at leats {MIN_KEY_LENGTH} symbols");
        }
    }
}

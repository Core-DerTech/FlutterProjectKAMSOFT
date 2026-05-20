using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.Models;

namespace FlutterProjectKAMSOFT.Encryption.CipherValidation
{
    public class TextCipherValidator<T> : AbstractValidator<T> where T : ChipherTextRequest
    {
        protected const int MIN_ALPHABET_SYMBOLS_NUMBER = 20;
        protected const int MIN_KEY_LENGTH = 8;
        protected const int MIN_SHIFT_VALUE = 3;
        public TextCipherValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Empty input data");

        }
    }
}

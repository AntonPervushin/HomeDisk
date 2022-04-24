using FluentValidation;

namespace HomeDisk.Api.Commands.User.ChangePassword
{
    public sealed class ChangeUserPasswordCommandValidator
        : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
        }
    }
}

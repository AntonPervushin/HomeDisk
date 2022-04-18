using FluentValidation;

namespace HomeDisk.Api.Commands.User
{
    public sealed class CreateUserCommandValidator
        : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}

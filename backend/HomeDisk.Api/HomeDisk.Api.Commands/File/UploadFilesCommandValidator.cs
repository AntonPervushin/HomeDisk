using FluentValidation;

namespace HomeDisk.Api.Commands.File
{
    public sealed class UploadFilesCommandValidator
        : AbstractValidator<UploadFilesCommand>
    {
        public UploadFilesCommandValidator()
        {
            RuleFor(x => x.Files).NotEmpty();
            RuleForEach(x => x.Files)
                .ChildRules(ch =>
                {
                    ch.RuleFor(c => c.Key).NotEmpty();
                    ch.RuleFor(c => c.Value).NotEmpty();
                });
        }
    }
}

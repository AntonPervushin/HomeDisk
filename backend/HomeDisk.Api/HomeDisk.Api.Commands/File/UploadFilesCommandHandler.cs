using HomeDisk.Api.Common.Access;
using HomeDisk.Api.Common.FileProcessing;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Commands.File
{
    public sealed class UploadFilesCommandHandler
        : IRequestHandler<UploadFilesCommand, Unit>
    {
        private readonly FileManager _fileManager;

        public UploadFilesCommandHandler(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public Task<Unit> Handle(
            UploadFilesCommand command, 
            CancellationToken cancellationToken)
        {
            var user = AppIdentityAccessor.Current;

            foreach(var file in command.Files)
            {
                cancellationToken.ThrowIfCancellationRequested();

                _fileManager.Save(user.UserName, file.Key, file.Value);
            }

            return Unit.Task;
        }
    }
}

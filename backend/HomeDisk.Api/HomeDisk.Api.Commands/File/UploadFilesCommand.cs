using MediatR;
using System.Collections.Generic;

namespace HomeDisk.Api.Commands.File
{
    public sealed class UploadFilesCommand : IRequest
    {
        public IReadOnlyDictionary<string, byte[]> Files { get; }

        public UploadFilesCommand(IReadOnlyDictionary<string, byte[]> files)
        {
            Files = files;
        }
    }
}

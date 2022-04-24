using HomeDisk.Api.Commands.File;
using HomeDisk.Api.Common.Identity;
using HomeDisk.Api.Infrastructure.Filters;
using HomeDisk.Api.Infrastructure.Helpers.FileHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost]
        //[Roles(AppIdentityRole.Admin)]
        //[Route("upload")]
        //[DisableFormValueModelBinding]
        ////[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UploadFileAsync(
        //    IFormFile uploadedFile,
        //    CancellationToken cancellationToken)
        //{

        //    if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
        //    {
        //        return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
        //    }

        //    if (Request.Form.Files.Count > 1)
        //        return BadRequest("Multiple files upload is not supported.");

        //    var boundary = MultipartRequestHelper.GetBoundary(
        //        MediaTypeHeaderValue.Parse(Request.ContentType),
        //        FormOptions.DefaultMultipartBoundaryLengthLimit);

        //    var file = Request.Form.Files[0];
        //    using (Stream stream = file.OpenReadStream())
        //    {
        //        using var ms = new MemoryStream();
        //        await stream.CopyToAsync(ms);
        //        ms.Position = 0;
        //        var reader = new MultipartReader(boundary, ms);
        //        var section = await reader.ReadNextSectionAsync();

        //        if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
        //            return BadRequest("Section has no content disposition header.");

        //        if (!MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
        //            return BadRequest("Section has no file content disposition.");

        //        var fileName = contentDisposition.FileName.Value;
        //        var uploadCommand = new UploadFilesCommand(fileName, ((MemoryStream)section.Body).ToArray());
        //        await _mediator.Send(uploadCommand, cancellationToken);
        //    }

        //    return Ok();
        //}


        [HttpPost]
        [Roles(AppIdentityRole.Admin)]
        [Route("upload")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadFileAsync(
            IFormFile uploadedFile,
            CancellationToken cancellationToken)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            var files = new Dictionary<string, byte[]>();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                FormOptions.DefaultMultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
                    return BadRequest("Section has no content disposition header.");

                if (!MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    return BadRequest("Section has no file content disposition.");

                var fileName = contentDisposition.FileName.Value;
                files[fileName] = GetFileContent(section.Body);

                section = await reader.ReadNextSectionAsync();
            }

            var uploadCommand = new UploadFilesCommand(files);
            await _mediator.Send(uploadCommand, cancellationToken);

            return Ok();
        }

        private byte[] GetFileContent(Stream stream)
        {
            if(stream is MemoryStream)
            {
                return ((MemoryStream)stream).ToArray();
            }

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}

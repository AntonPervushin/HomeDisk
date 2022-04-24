using AutoMapper;
using HomeDisk.Api.Commands.User;
using HomeDisk.Api.Commands.User.ChangePassword;
using HomeDisk.Api.Commands.User.Create;
using HomeDisk.Api.Common.Identity;
using HomeDisk.Api.Controllers.Dtos.User;
using HomeDisk.Api.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [Roles(AppIdentityRole.Admin)]
        public async Task<CreateUserResponseDto> CreateAsync(
            [FromBody] CreateUserRequestDto request,
            CancellationToken cancellationToken)
        {
            var roles = _mapper.Map<IEnumerable<AppIdentityRole>>(request.Roles);
            var command = new CreateUserCommand(request.UserName, roles);
            var password = await _mediator.Send(command, cancellationToken);

            return new CreateUserResponseDto
            {
                Password = password
            };
        }

        [HttpPost("changePasword")]
        [Roles(new[] { AppIdentityRole.Admin, AppIdentityRole.User })]
        public async Task<IActionResult> ChangePasswordAsync(
        [FromBody] ChangeUserPasswordRequestDto request,
        CancellationToken cancellationToken)
        {
            var command = new ChangeUserPasswordCommand(request.CurrentPassword, request.NewPassword);
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}

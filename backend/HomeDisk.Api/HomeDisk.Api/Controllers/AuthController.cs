using HomeDisk.Api.Commands.Authentication;
using HomeDisk.Api.Commands.Authentication.Logout;
using HomeDisk.Api.Controllers.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task LoginAsync([FromBody] AuthDto request, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Login, request.Password);
            await _mediator.Send(command, cancellationToken);
		}

        [HttpPost]
        [AllowAnonymous]
        [Route("logout")]
        public async Task LogoutAsync(CancellationToken cancellationToken)
        {
            var command = new LogoutCommand();
            await _mediator.Send(command, cancellationToken);
        }
    }
}

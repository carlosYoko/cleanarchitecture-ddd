using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DirectorController : ControllerBase
    {
        private IMediator? _mediator;

        public DirectorController(IMediator? mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateDirector")]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateDirector([FromBody] CreateDirectorCommand commanmd)
        {
            return await _mediator!.Send(commanmd);
        }
    }
}

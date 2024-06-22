using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator? _mediator;

        public VideoController(IMediator? mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{userName}", Name = "GetVideo")]
        [ProducesResponseType(typeof(IEnumerable<VideosVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<VideosVM>>> GetVideosByUserName(string username)
        {
            var query = new GetVideosListQuery(username);
            var videos = await _mediator!.Send(query);

            return Ok(videos);
        }
    }

}

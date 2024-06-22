using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQuery : IRequest<List<VideosVM>>
    {
        public string UserName { get; set; } = string.Empty;
        public GetVideosListQuery(string userName)
        {
            this.UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
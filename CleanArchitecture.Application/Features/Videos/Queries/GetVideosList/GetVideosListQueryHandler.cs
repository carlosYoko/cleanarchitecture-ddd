using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideosVM>>
    {
        private readonly IVideoRepository? _videoRepository;
        private readonly IMapper? _mapper;

        public GetVideosListQueryHandler(IVideoRepository? videoRepository, IMapper? mapper)
        {
            this._videoRepository = videoRepository;
            this._mapper = mapper;
        }

        public async Task<List<VideosVM>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            var videoList = await _videoRepository!.GetVideoByUserName(request.UserName);

            return _mapper!.Map<List<VideosVM>>(videoList);
        }
    }
}
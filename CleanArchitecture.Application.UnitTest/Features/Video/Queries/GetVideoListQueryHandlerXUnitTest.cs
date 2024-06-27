using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTest.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTest.Features.Video.Queries
{
    public class GetVideoListQueryHandlerXUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetVideoListQueryHandlerXUnitTest()
        {
            _unitOfWork = MockUnitOfWork.GeUnitOfWork();

            var mapperConfig = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetVideoListTest()
        {
            var handler = new GetVideosListQueryHandler(_unitOfWork.Object, _mapper);
            var request = new GetVideosListQuery("Carlos");

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<VideosVM>>();
            result.Count.ShouldBe(1);
        }
    }
}

using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.UnitTest.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<VideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var videos = fixture.CreateMany<Video>().ToList();

            videos.Add(fixture.Build<Video>()
                    .With(tr => tr.CreateBy, "Carlos")
                    .Create()
                );

            var options = new DbContextOptionsBuilder<StreamerDbContext>()
                .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{Guid.NewGuid}")
                .Options;

            var streamerDbContextFake = new StreamerDbContext(options);
            streamerDbContextFake.Videos!.AddRange(videos);
            streamerDbContextFake.SaveChanges();

            var mockRepository = new Mock<VideoRepository>(streamerDbContextFake);
            return mockRepository;
        }
    }
}

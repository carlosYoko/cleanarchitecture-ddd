using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GeUnitOfWork()
        {
            var options = new DbContextOptionsBuilder<StreamerDbContext>()
               .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{Guid.NewGuid}")
               .Options;

            var streamerDbContextFake = new StreamerDbContext(options);

            streamerDbContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWork>(streamerDbContextFake);

            return mockUnitOfWork;
        }
    }
}

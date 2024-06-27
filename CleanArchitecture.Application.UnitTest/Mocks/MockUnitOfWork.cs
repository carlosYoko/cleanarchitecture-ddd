using CleanArchitecture.Application.Contracts.Persistence;
using Moq;

namespace CleanArchitecture.Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GeUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockVideoRepository = MockVideoRepository.GetVideoRepository();

            mockUnitOfWork.Setup(r => r.VideoRepository).Returns(mockVideoRepository.Object);

            return mockUnitOfWork;
        }
    }
}

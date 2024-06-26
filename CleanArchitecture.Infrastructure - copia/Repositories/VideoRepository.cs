using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context) : base(context)
        { }

        public async Task<Video?> GetVideoByName(string videoName)
        {
            return await _context!.Videos!.Where(v => v.Name == videoName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Video>> GetVideoByUserName(string userName)
        {
            return await _context!.Videos!.Where(v => v.CreateBy == userName).ToListAsync();
        }
    }
}

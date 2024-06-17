using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Streamer : BaseDomainModel
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public ICollection<Video>? Videos { get; set; }
    }
}
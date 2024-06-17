using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Video : BaseDomainModel
    {
        public Video()
        {
            Actors = new HashSet<Actor>();
        }

        public string Name { get; set; } = string.Empty;
        public int StreamerId { get; set; }
        public virtual Streamer? Streamer { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
    }
}

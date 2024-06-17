using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Actor : BaseDomainModel
    {
        public Actor()
        {
            Videos = new HashSet<Video>();
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Video> Videos { get; set; }
    }
}

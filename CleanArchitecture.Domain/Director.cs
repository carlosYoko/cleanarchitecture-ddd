using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Director : BaseDomainModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

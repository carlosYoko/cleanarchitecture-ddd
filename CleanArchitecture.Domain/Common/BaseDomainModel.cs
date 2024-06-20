namespace CleanArchitecture.Domain.Common
{
    abstract class BaseDomainModel
    {
        public int Id { get; set; }
        public DateTime? DateCreate { get; set; }
        public string CreateBy { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
    }
}

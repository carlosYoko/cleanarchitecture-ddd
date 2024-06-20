namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseDomainModel
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
    }
}

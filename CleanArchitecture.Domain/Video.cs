namespace CleanArchitecture.Domain
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StramerId { get; set; }
        public virtual Streamer? Streamer { get; set; }
    }
}

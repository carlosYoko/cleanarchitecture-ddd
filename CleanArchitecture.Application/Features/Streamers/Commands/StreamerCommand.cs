using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class StreamerCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public StreamerCommand(string name, string url)
        {
            this.Name = name;
            this.Url = url;
        }
    }
}

using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        private readonly IStreamerRepository? _streamerRepository;
        private readonly IMapper? _mapper;
        private readonly ILogger<DeleteStreamerCommandHandler>? _logger;

        public DeleteStreamerCommandHandler(IStreamerRepository? streamerRepository, IMapper? mapper, ILogger<DeleteStreamerCommandHandler>? logger)
        {
            this._streamerRepository = streamerRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToDelete = await _streamerRepository!.GetByIdAsync(request.Id);
            if (streamerToDelete == null)
            {
                _logger!.LogError($"El streamer {request.Id} no exite");
                throw new NotFoundExcpetion(nameof(Streamer), request.Id);
            }

            await _streamerRepository.DeleteAsync(streamerToDelete);
            _logger!.LogInformation($"El streamer {request.Id} ha sido eliminado con exito");

            return Unit.Value;
        }
    }
}

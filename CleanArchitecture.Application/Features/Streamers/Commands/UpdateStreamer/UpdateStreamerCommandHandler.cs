using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IStreamerRepository? _streamerRepository;
        private readonly IMapper? _mapper;
        private readonly ILogger<UpdateStreamerCommandHandler>? _logger;

        public UpdateStreamerCommandHandler(IStreamerRepository? streamerRepository, IMapper? mapper, ILogger<UpdateStreamerCommandHandler>? logger)
        {
            this._streamerRepository = streamerRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _streamerRepository!.GetByIdAsync(request.Id);

            if (streamerToUpdate == null)
            {
                _logger!.LogError($"No se ha encontrado el streamer con ID {request.Id}");
                throw new NotFoundExcpetion(nameof(Streamer), request.Id);
            }

            _mapper!.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));

            await _streamerRepository!.UpdateAsync(streamerToUpdate);

            _logger!.LogInformation($"El streamer {request.Id} se ha actualizado correctamente");

            return Unit.Value;
        }
    }
}

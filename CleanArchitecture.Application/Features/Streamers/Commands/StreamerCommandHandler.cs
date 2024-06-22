using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class StreamerCommandHandler : IRequestHandler<StreamerCommand, int>
    {
        private readonly IStreamerRepository? _streamerRepository;
        private readonly IMapper? _mapper;
        private readonly IEmailService? _emailService;
        private readonly ILogger<StreamerCommandHandler>? _logger;

        public StreamerCommandHandler(IStreamerRepository? streamerRepository, IMapper? mapper, IEmailService? emailService, ILogger<StreamerCommandHandler>? logger)
        {
            this._streamerRepository = streamerRepository;
            this._mapper = mapper;
            this._emailService = emailService;
            this._logger = logger;
        }

        public async Task<int> Handle(StreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper!.Map<Streamer>(request);
            var newStreamer = await _streamerRepository!.AddAsync(streamerEntity);

            _logger!.LogInformation($"Streamer: {newStreamer.Name} (ID: {newStreamer.Id}) ha sido creado");

            await SendEmail(newStreamer);

            return newStreamer.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "carlosgibe@gmail.com",
                Body = $"La compañía {streamer.Name} se ha creado correctamente",
                Subject = "MyStreamer"
            };

            try
            {
                await _emailService!.SendEmail(email);
            }
            catch (Exception)
            {
                _logger!.LogError($"Error al enviar el email de {streamer.Id}");
            }
        }
    }
}

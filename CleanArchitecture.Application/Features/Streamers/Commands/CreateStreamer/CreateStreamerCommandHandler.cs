using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        //private readonly IStreamerRepository? _streamerRepository;
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper? _mapper;
        private readonly IEmailService? _emailService;
        private readonly ILogger<CreateStreamerCommandHandler>? _logger;

        public CreateStreamerCommandHandler(IUnitOfWork? unitOfWork, IMapper? mapper, IEmailService? emailService, ILogger<CreateStreamerCommandHandler>? logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper!.Map<Streamer>(request);
            _unitOfWork!.StreamerRepository.AddEntity(streamerEntity);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                throw new Exception("No se pudo insertar el streamer");
            }

            _logger!.LogInformation($"Streamer: {streamerEntity.Name} (ID: {streamerEntity.Id}) ha sido creado");

            await SendEmail(streamerEntity);

            return streamerEntity.Id;
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
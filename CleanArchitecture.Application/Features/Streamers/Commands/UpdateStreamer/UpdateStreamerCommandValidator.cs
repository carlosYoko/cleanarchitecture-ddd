using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    internal class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(p => p.Name)
           .NotEmpty().WithMessage("El nombre no puede estar en blanco")
           .NotNull().WithMessage("El valor no puede ser nulo")
           .MaximumLength(50).WithMessage("{Nombre} no puede exceder de los 50 caracteres");

            RuleFor(p => p.Url)
           .NotEmpty().WithMessage("La URL no puede estar en blanco");
        }
    }
}

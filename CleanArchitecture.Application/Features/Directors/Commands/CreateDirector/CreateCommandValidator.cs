using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(p => p.Name)
                    .NotNull().WithMessage("{Name} no puede ser nulo");

            RuleFor(p => p.SurName)
              .NotNull().WithMessage("{SurName} no puede ser nulo");
        }
    }
}

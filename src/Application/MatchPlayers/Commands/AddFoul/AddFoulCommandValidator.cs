using FluentValidation;

namespace Application.MatchPlayers.Commands.AddFoul
{
    public class AddFoulCommandValidator : AbstractValidator<AddFoulCommand>
    {
        public AddFoulCommandValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty().WithMessage("Igrac je obavezan");
        }
    }
}

using FluentValidation;

namespace Application.MatchPlayers.Commands.AddPoints
{
    public class AddPointsCommandValidator : AbstractValidator<AddPointsCommand>
    {
        public AddPointsCommandValidator()
        {
            RuleFor(x => x.ScorePlayerId)
                .NotEmpty().WithMessage("Igrac koji je postigao poene je obavezan");

            RuleFor(x => x.Points)
                .NotEmpty().WithMessage("Broj poena je obavezan")
                .InclusiveBetween(1, 2).WithMessage("Broj poena mora biti 1 ili 2");
        }
    }
}

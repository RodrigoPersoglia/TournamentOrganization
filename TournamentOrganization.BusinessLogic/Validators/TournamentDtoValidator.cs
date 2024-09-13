using FluentValidation;
using TournamentOrganization.BusinessLogic.Dtos;

public class TournamentDtoValidator : AbstractValidator<TournamentDto>
{
    public TournamentDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .Must(date => date.Date >= DateTime.UtcNow.Date)
            .WithMessage("StartDate cannot be in the past.");

        RuleFor(x => x.PlayerGender)
            .NotEmpty()
            .Must(g => g == "Male" || g == "Female")
            .WithMessage("PlayerGender must be 'Male' or 'Female'.");

        RuleFor(x => x.PlayersId)
            .Must(ids => ids.Count > 0 && ValidationHelper.IsPowerOfTwo(ids.Count))
            .WithMessage("The number of PlayerIds must be a power of 2.");

        RuleFor(x => x.PlayersId)
            .Must(players => players.Distinct().Count() == players.Count())
            .WithMessage("PlayerId must be unique.");
    }
}

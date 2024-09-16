using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.BusinessLogic.Helpers;

namespace TournamentOrganization.BusinessLogic.Interfaces
{
    public interface ITournamentService
    {
        Task<OperationResult<string>> CreateTournament(TournamentDto tournamentDto);

        Task<List<MatchDto>> GetTournamentsByFilter(int? TournamentId, string? TournamentName, DateTime? date, string? gender, string? stage);
    }
}

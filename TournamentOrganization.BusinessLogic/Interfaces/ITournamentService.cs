using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.BusinessLogic.Helpers;

namespace TournamentOrganization.BusinessLogic.Interfaces
{
    public interface ITournamentService
    {
        Task<OperationResult<string>> CreateTournament(TournamentDto tournamentDto);
    }
}

using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.BusinessLogic.Interfaces
{
    public interface ITournamentSimulationService
    {
        Task<Player> SimulateTournamentAsync(int tournamentId);
    }
}

using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.DataAccess.Repositories.Interfaces
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task<List<Match>> GetMatchesByTournamentAsync(int tournamentId);
        Task<Match> GetMatchByPlayersAndTournamentAsync(int player1Id, int player2Id, int tournamentId);
    }
}

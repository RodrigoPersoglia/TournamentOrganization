using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.DataAccess.Repositories.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<List<Player>> GetPlayersByIdsAsync(IList<int> playerIds);
    }
}

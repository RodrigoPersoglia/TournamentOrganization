using Microsoft.EntityFrameworkCore;
using TournamentOrganization.DataAccess.Repositories.Interfaces;
using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.DataAccess.Repositories.Implementations
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        AppDbContext _context;
        public PlayerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetPlayersByIdsAsync(IList<int> playerIds)
        {
            var result = await _context.Players.Where(p => playerIds.Contains(p.Id)).ToListAsync();
            return result;
        }

        public async Task<List<Player>> GetPlayersByTournamentIdAsync(int tournamentId)
        {
            return await _context.Players.Where(p => p.PlayerTournaments.Any(t => t.TournamentId == tournamentId)).ToListAsync();
        }
    }
}

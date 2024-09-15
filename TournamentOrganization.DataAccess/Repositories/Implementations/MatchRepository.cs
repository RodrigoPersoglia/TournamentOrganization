using Microsoft.EntityFrameworkCore;
using TournamentOrganization.DataAccess.Repositories.Interfaces;
using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.DataAccess.Repositories.Implementations
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        private readonly DbContext _context;

        public MatchRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetMatchesByTournamentAsync(int tournamentId)
        {
            return await _context.Set<Match>().Where(m => m.TournamentId == tournamentId).ToListAsync();
        }

        public async Task<Match> GetMatchByPlayersAndTournamentAsync(int player1Id, int player2Id, int tournamentId)
        {
            return await _context.Set<Match>().FirstOrDefaultAsync(m => m.Player1Id == player1Id && m.Player2Id == player2Id && m.TournamentId == tournamentId);
        }

        public new async Task AddAsync(Match entity)
        {
            await base.AddAsync(entity);
            await SaveAsync();
        }
    }

}

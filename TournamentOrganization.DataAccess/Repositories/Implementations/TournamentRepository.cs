using TournamentOrganization.DataAccess.Repositories.Interfaces;

namespace TournamentOrganization.DataAccess.Repositories.Implementations
{
    public class TournamentRepository : Repository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<Tournament> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(Tournament tournament)
        {
            await base.AddAsync(tournament);
            await SaveAsync();
        }

        public new async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }
    }
}

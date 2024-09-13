namespace TournamentOrganization.DataAccess.Repositories.Interfaces
{
    public interface ITournamentRepository : IRepository<Tournament>
    {
        Task<Tournament> GetByIdAsync(int id);
        Task AddAsync(Tournament tournament);
        Task<IEnumerable<Tournament>> GetAllAsync();
    }
}

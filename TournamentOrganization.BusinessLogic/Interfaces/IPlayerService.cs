using TournamentOrganization.BusinessLogic.Dtos;

namespace TournamentOrganization.BusinessLogic.Interfaces
{
    public interface IPlayerService
    {
        Task<List<PlayerDto>> GetAll();
    }
}

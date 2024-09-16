using AutoMapper;
using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.BusinessLogic.Interfaces;
using TournamentOrganization.DataAccess.Repositories.Interfaces;

namespace TournamentOrganization.BusinessLogic.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerService;
        private readonly IMapper _mapper;
        public PlayerService(IPlayerRepository playerService, IMapper mapper) 
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        public async Task<List<PlayerDto>> GetAll()
        {
            var players = await _playerService.GetAllAsync();
            var playersDto = _mapper.Map<List<PlayerDto>>(players);
            return playersDto;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TournamentOrganization.BusinessLogic.Interfaces;

namespace TournamentOrganization.Presentation.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("GetPlayers")]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var results = await _playerService.GetAll();
                return Ok(results);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

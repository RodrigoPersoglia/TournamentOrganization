using Microsoft.AspNetCore.Mvc;
using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.BusinessLogic.Interfaces;

namespace TournamentOrganization.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService= tournamentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament(TournamentDto tournament)
        {
            try
            {
                var result = await _tournamentService.CreateTournament(tournament);
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get(string tournament, DateTime startDate)
        {
            return Ok(tournament);
        }
    }
}
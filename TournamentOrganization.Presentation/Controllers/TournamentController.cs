using Microsoft.AspNetCore.Mvc;
using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.BusinessLogic.Exceptions;
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
            _tournamentService = tournamentService;
        }

        [HttpPost("CreateTournament")]
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

        [HttpGet("GetTournamentResults")]
        public async Task<IActionResult> GetTournamentResults(int? tournamentId, string? tournamentName, DateTime? date, string? gender, string? stage)
        {
            try
            {
                var results = await _tournamentService.GetTournamentsByFilter(tournamentId, tournamentName, date, gender, stage);
                if (results == null || !results.Any())
                {
                    return NotFound("No tournament results found with the given filters.");
                }

                return Ok(results);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
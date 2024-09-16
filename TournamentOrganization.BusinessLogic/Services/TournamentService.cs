using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.BusinessLogic.Exceptions;
using TournamentOrganization.BusinessLogic.Helpers;
using TournamentOrganization.BusinessLogic.Interfaces;
using TournamentOrganization.DataAccess.Repositories.Interfaces;

namespace TournamentOrganization.BusinessLogic.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IValidator<TournamentDto> _tournamentValidator;
        private readonly ITournamentSimulationService _tournamentSimulationService;
        private readonly IMatchRepository _matchRepository;

        public TournamentService(IMapper mapper, ITournamentRepository tournamentRepository,
            IPlayerRepository playerRepository, IValidator<TournamentDto> tournamentValidator,
            ITournamentSimulationService tournamentSimulationService, IMatchRepository matchRepository)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
            _playerRepository = playerRepository;
            _tournamentValidator = tournamentValidator;
            _tournamentSimulationService = tournamentSimulationService;
            _matchRepository = matchRepository;
        }

        public async Task<OperationResult<string>> CreateTournament(TournamentDto tournamentDto)
        {
            try
            {
                var validationResult = await _tournamentValidator.ValidateAsync(tournamentDto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    string messageError = string.Join(" | ", errors);
                    return CreateErrorResponse(messageError);
                }

                if (!await ValidateTournamentParticipants(tournamentDto))
                {
                    return CreateErrorResponse("The participants are not all of the same gender.");
                }

                var tournament = _mapper.Map<Tournament>(tournamentDto);
                await _tournamentRepository.AddAsync(tournament);

                var winner = await _tournamentSimulationService.SimulateTournamentAsync(tournament.Id);

                var result = new OperationResult<string>()
                {
                    Success = true,
                    Data = $"Tournament successfully completed. The champion of the tournament is: {winner.FirstName} {winner.LastName}. Tournament ID = {tournament.Id}"
                };

                return result;
            }
            catch (DbUpdateException)
            {
                return CreateErrorResponse("There are issues with the submitted data.");
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ex.Message);
            }
        }

        public async Task<List<MatchDto>> GetTournamentsByFilter(int? TournamentId, string? TournamentName, DateTime? date, string? gender, string? stage)
        {
            if (!TournamentId.HasValue & string.IsNullOrEmpty(TournamentName))
            {
                throw new CustomException("Both TournamentId and tournamentName fields cannot be null.");
            }

            if (!string.IsNullOrEmpty(gender) && !gender.Equals("Male") && !gender.Equals("Female"))
            {
                throw new CustomException("Gender must be 'Male' or 'Female'.");
            }

            var query = _matchRepository.GetAllAsQueryable();
            query = query.Where(t =>
            (!TournamentId.HasValue || t.TournamentId == TournamentId) &&
            (string.IsNullOrEmpty(TournamentName) || t.Tournament.Name == TournamentName) &&
            (!date.HasValue || t.Tournament.StartDate.Date == date.Value.Date) &&
            (string.IsNullOrEmpty(gender) || t.Tournament.PlayerGender == gender) &&
            (string.IsNullOrEmpty(stage) || t.Stage == stage))
            .Include(m => m.Player1)
            .Include(m => m.Player2)
            .Include(m => m.Winner);

            var matches = await query.ToListAsync();

            var matchDtos = _mapper.Map<List<MatchDto>>(matches);

            return matchDtos;
        }

        #region Private Methods
        private OperationResult<string> CreateErrorResponse(string errorMessage)
        {
            var result = new OperationResult<string>()
            {
                Success = false,
                ErrorMessage = $"Error: {errorMessage}"
            };
            return result;
        }

        public async Task<bool> ValidateTournamentParticipants(TournamentDto tournament)
        {
            var players = await _playerRepository.GetPlayersByIdsAsync(tournament.PlayersId);
            return players.All(p => p.Gender.Equals(tournament.PlayerGender, StringComparison.OrdinalIgnoreCase));
        }
        #endregion

    }
}

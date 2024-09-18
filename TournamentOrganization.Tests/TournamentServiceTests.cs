using AutoMapper;
using FluentValidation;
using Moq;
using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.BusinessLogic.Interfaces;
using TournamentOrganization.BusinessLogic.Services;
using TournamentOrganization.DataAccess.Repositories.Interfaces;
using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.Tests
{
    public class TournamentServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ITournamentRepository> _tournamentRepositoryMock;
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly Mock<ITournamentSimulationService> _tournamentSimulationServiceMock;
        private readonly Mock<IMatchRepository> _matchRepositoryMock;

        public TournamentServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _tournamentRepositoryMock = new Mock<ITournamentRepository>();
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _tournamentSimulationServiceMock = new Mock<ITournamentSimulationService>();
            _matchRepositoryMock = new Mock<IMatchRepository>();
        }

        [Fact]
        public async Task CreateTournament_Should_Return_Error_If_Not_Power_Of_Two_Players()
        {
            var tournamentService = new TournamentService(
                _mapperMock.Object,
                _tournamentRepositoryMock.Object,
                _playerRepositoryMock.Object,
                new TournamentDtoValidator(),
                _tournamentSimulationServiceMock.Object,
                _matchRepositoryMock.Object
            );

            var tournamentDto = new TournamentDto
            {
                Name = "Test",
                PlayerGender = "Male",
                StartDate = DateTime.Now,
                PlayersId = new List<int> { 1, 2, 3 }
            };

            var result = await tournamentService.CreateTournament(tournamentDto);

            Assert.False(result.Success);
            Assert.Contains("Error: The number of PlayerIds must be a power of 2.", result.ErrorMessage);
        }

        [Fact]
        public async Task CreateTournament_Should_Return_Error_If_Players_Not_Same_Gender2()
        {
            var mockPlayers = new List<Player>
{
                new Player { Id = 1, FirstName = "Novak", LastName = "Djokovic", Gender = "Male" },
                new Player { Id = 2, FirstName = "Carlos", LastName = "Alcaraz", Gender = "Male" },
                new Player { Id = 3, FirstName = "Daniil", LastName = "Medvedev", Gender = "Male" },
                new Player { Id = 4, FirstName = "Katerina", LastName = "Johnson", Gender = "Female" }
            };

            var playerRepositoryMock = new Mock<IPlayerRepository>();

            playerRepositoryMock
                .Setup(repo => repo.GetPlayersByIdsAsync(It.IsAny<IList<int>>()))
                .ReturnsAsync((IList<int> playerIds) =>
                {
                    return mockPlayers.Where(p => playerIds.Contains(p.Id)).ToList();
                });


            var tournamentService = new TournamentService(
                _mapperMock.Object,
                _tournamentRepositoryMock.Object,
                playerRepositoryMock.Object,
                new TournamentDtoValidator(),
                _tournamentSimulationServiceMock.Object,
                _matchRepositoryMock.Object
            );

            var tournamentDto = new TournamentDto
            {
                Name = "Test",
                PlayerGender = "Male",
                StartDate = DateTime.Now,
                PlayersId = new List<int> { 1, 2, 3, 4 }
            };

            var result = await tournamentService.CreateTournament(tournamentDto);

            Assert.False(result.Success); 
            Assert.Contains("Error: The participants are not all of the same gender.", result.ErrorMessage);
        }

    }

}

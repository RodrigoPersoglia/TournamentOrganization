using TournamentOrganization.BusinessLogic.Interfaces;
using TournamentOrganization.DataAccess.Repositories.Interfaces;
using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.BusinessLogic.Services
{
    public class TournamentSimulationService : ITournamentSimulationService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchRepository _matchRepository;

        public TournamentSimulationService(IPlayerRepository playerRepository, IMatchRepository matchRepository)
        {
            _playerRepository = playerRepository;
            _matchRepository = matchRepository;
        }

        public async Task<Player> SimulateTournamentAsync(int tournamentId)
        {
            var players = await _playerRepository.GetPlayersByTournamentIdAsync(tournamentId);

            players = players.OrderByDescending(p => p.SkillLevel).ToList();
            List<Player> winners = new List<Player>();

            int roundMatches = players.Count / 2;
            while (players.Count > 1)
            {
                for (int i = 0; i < roundMatches; i++)
                {
                    var player1 = players[i];
                    var player2 = players[roundMatches + i];

                    var winner = SimulateMatch(player1, player2, tournamentId);
                    winners.Add(winner);

                    await _matchRepository.AddAsync(new Match
                    {
                        Player1Id = player1.Id,
                        Player2Id = player2.Id,
                        WinnerId = winner.Id,
                        TournamentId = tournamentId,
                        Date = DateTime.Now,
                        Stage = DetermineStage(players.Count)
                    });
                }

                players = winners;
                winners = new List<Player>();

                roundMatches = players.Count / 2;
            }

            return players.First();
        }

        private Player SimulateMatch(Player player1, Player player2, int tournamentId)
        {
            double score1 = CalculateScore(player1);
            double score2 = CalculateScore(player2);

            var winner = score1 >= score2 ? player1 : player2;
            return winner;
        }

        private double CalculateScore(Player player)
        {
            double randomNumber = new Random().Next(0, 51);
            var luckFactor = randomNumber /100 * player.SkillLevel;
            if (player.Gender == "Male")
            {
                int strength = player.Strength ?? 0;
                int speed = player.Speed ?? 0;
                double score = player.SkillLevel + strength * (speed + luckFactor)/2;
                return score;
            }
            else
            {
                int reactionTime = player.ReactionTime ?? 0;
                double score = player.SkillLevel + reactionTime + luckFactor;
                return score;
            }
        }

        private string DetermineStage(int remainingPlayers)
        {
            switch (remainingPlayers)
            {
                case 2: return "Final";
                case 4: return "Semifinals";
                case 8: return "Quarterfinals";
                default: return $"Round of {remainingPlayers}";
            }
        }

    }

}

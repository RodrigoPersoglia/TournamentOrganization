using TournamentOrganization.Domain.Entities;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public string PlayerGender { get; set; }

    public ICollection<PlayerTournament> PlayerTournaments { get; set; }
    public ICollection<Match> Matches { get; set; }
}

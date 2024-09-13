namespace TournamentOrganization.Domain.Entities
{
    public class PlayerTournament
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TournamentId { get; set; }
        public Player Player { get; set; } 
        public Tournament Tournament { get; set; }
    }
}

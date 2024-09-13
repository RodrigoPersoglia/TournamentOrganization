namespace TournamentOrganization.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int TournamentId { get; set; }
        public int WinnerId { get; set; }
        public string Stage { get; set; }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player Winner { get; set; }
        public Tournament Tournament { get; set; }
    }
}

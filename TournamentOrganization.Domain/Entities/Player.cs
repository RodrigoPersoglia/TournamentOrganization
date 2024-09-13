namespace TournamentOrganization.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SkillLevel { get; set; } 
        public string Gender { get; set; }
        public int? Strength { get; set; }  
        public int? Speed { get; set; } 
        public int? ReactionTime { get; set; }

        public ICollection<PlayerTournament> PlayerTournaments { get; set; }
    }
}

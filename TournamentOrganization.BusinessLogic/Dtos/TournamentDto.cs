namespace TournamentOrganization.BusinessLogic.Dtos
{
    public class TournamentDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string PlayerGender { get; set; }
        public IList<int> PlayersId { get; set; }
    }
}

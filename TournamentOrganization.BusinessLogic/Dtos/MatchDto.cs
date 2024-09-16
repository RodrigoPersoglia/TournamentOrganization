using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentOrganization.BusinessLogic.Dtos
{
    public class MatchDto
    {
        public DateTime Date { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int TournamentId { get; set; }
        public string Winner { get; set; }
        public string Stage { get; set; }
    }
}

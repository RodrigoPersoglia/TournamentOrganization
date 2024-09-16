using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.BusinessLogic.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SkillLevel { get; set; }
        public string Gender { get; set; }
        public int? Strength { get; set; }
        public int? Speed { get; set; }
        public int? ReactionTime { get; set; }

    }
}

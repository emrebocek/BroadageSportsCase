using System;
using System.Collections.Generic;

namespace BroadageSportsCaseWebApp.Models
{
    public partial class HomeTeam
    {
        public HomeTeam()
        {
            Matches = new HashSet<Match>();
        }

        public int HomeTeamId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? MediumName { get; set; }
        public int? ScoreId { get; set; }

        public virtual Score? Score { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}

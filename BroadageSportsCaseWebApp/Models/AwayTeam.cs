using System;
using System.Collections.Generic;

namespace BroadageSportsCaseWebApp.Models
{
    public partial class AwayTeam
    {
        public AwayTeam()
        {
            Matches = new HashSet<Match>();
        }

        public int AwayTeamId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? MediumName { get; set; }
        public int? ScoreId { get; set; }

        public virtual Score? Score { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BroadageSportsCaseWebApp.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Matches = new HashSet<Match>();
        }

        public int TournamentId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}

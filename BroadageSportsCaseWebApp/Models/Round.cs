using System;
using System.Collections.Generic;

namespace BroadageSportsCaseWebApp.Models
{
    public partial class Round
    {
        public Round()
        {
            Matches = new HashSet<Match>();
        }

        public int RoundId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}

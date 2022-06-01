using System;
using System.Collections.Generic;

namespace BroadageSportsCaseWebApp.Models
{
    public partial class Status
    {
        public Status()
        {
            Matches = new HashSet<Match>();
        }

        public int StatusId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}

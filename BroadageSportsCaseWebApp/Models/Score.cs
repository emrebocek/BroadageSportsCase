using System;
using System.Collections.Generic;

namespace BroadageSportsCaseWebApp.Models
{
    public partial class Score
    {
        public Score()
        {
            AwayTeams = new HashSet<AwayTeam>();
            HomeTeams = new HashSet<HomeTeam>();
        }

        public int ScoreId { get; set; }
        public int? Regular { get; set; }
        public int? HalfTime { get; set; }
        public int? Current { get; set; }

        public virtual ICollection<AwayTeam> AwayTeams { get; set; }
        public virtual ICollection<HomeTeam> HomeTeams { get; set; }
    }
}

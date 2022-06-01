using System;
using System.Collections.Generic;

namespace BroadageSportsCaseWebApp.Models
{
    public partial class Match
    {
        public long MatchId { get; set; }
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public int? StatusId { get; set; }
        public int? TournamentId { get; set; }
        public int? StageId { get; set; }
        public int? RoundId { get; set; }
        public DateTime? MatchDate { get; set; }

        public virtual AwayTeam? AwayTeam { get; set; }
        public virtual HomeTeam? HomeTeam { get; set; }
        public virtual Round? Round { get; set; }
        public virtual Stage? Stage { get; set; }
        public virtual Status? Status { get; set; }
        public virtual Tournament? Tournament { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadageSportsCase.Model
{
    public class BroadageSportsMatchListModel
    {
        public List<Match> matches { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class AwayTeam
    {
        public Score score { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string mediumName { get; set; }
        public int id { get; set; }
    }

    public class HomeTeam
    {
        public Score score { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string mediumName { get; set; }
        public int id { get; set; }
    }

    public class Match
    {
        public HomeTeam homeTeam { get; set; }
        public AwayTeam awayTeam { get; set; }
        public Status status { get; set; }
        public string date { get; set; }
        public int id { get; set; }
        public Tournament tournament { get; set; }
        public Stage stage { get; set; }
        public Round round { get; set; }
    }

    public class Round
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public int id { get; set; }
    }

    public class Score
    {
        public int regular { get; set; }
        public int halfTime { get; set; }
        public int current { get; set; }
    }

    public class Stage
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public int id { get; set; }
    }

    public class Status
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public int id { get; set; }
    }

    public class Tournament
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public int id { get; set; }
    }


}

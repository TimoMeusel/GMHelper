using System.Collections.Generic;
using System.IO;
using System.Linq;
using GM.Model;

namespace GM.Persistence
{
    public class CsvImport
    {
        public IEnumerable<Player> Load (string path)
        {
            List<Skater> skaters = new List<Skater>();
            List<Goalie> goalies = new List<Goalie>();

            List<string> dataSets = File.ReadLines(path).ToList();
            string header = dataSets.First();
            dataSets.RemoveAt(0);

            var headers = header.Split('\t').ToList();
            var teamHeader = headers.Last();

            while (dataSets.Any() && !string.IsNullOrWhiteSpace(dataSets[0]))
            {
                var line = dataSets[0];
                var values = line.Split('\t');
                var dict = new Dictionary<string, string>();
                for ( int i = 0; i < headers.Count; i++ )
                {
                    dict.Add(headers[i], values[i]);
                }

                var team = GetTeam(dict, teamHeader);

                var skater = new Skater(team, dict);
                skaters.Add(skater);

                dataSets.RemoveAt(0);
            }

            // remove whitespace line
            dataSets.RemoveAt(0);

            header = dataSets.First();
            dataSets.RemoveAt(0);

            headers = header.Split('\t').ToList();
            teamHeader = headers.Last();
            while ( dataSets.Any() && !string.IsNullOrWhiteSpace(dataSets[0]) )
            {
                var line = dataSets[0];
                var values = line.Split('\t');
                var dict = new Dictionary<string, string>();
                for ( int i = 0; i < headers.Count; i++ )
                {
                    dict.Add(headers[i], values[i]);
                }
                var team = GetTeam(dict, teamHeader);
                var goalie = new Goalie(team, dict);
                goalies.Add(goalie);

                dataSets.RemoveAt(0);
            }

            var players = new List<Player>();
            players.AddRange(skaters);
            players.AddRange(goalies);

            return players;
        }

        private static Team GetTeam (Dictionary<string, string> dict, string teamHeader)
        {
            Team team;
            string teamValue = dict[teamHeader];
            string[] split = teamValue.Split('-');
            if (split.Length > 1)
            {
                Team proTeam = new Team(split[1].Trim());
                team = new FarmTeam(split[0].Trim(), proTeam);
            }
            else
            {
                team = new Team(teamValue);
            }
            return team;
        }
    }
}

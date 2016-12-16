using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GM.Model;

namespace GM.Persistence
{
    public class CsvExport
    {
        public void Export (string path)
        {
            StringBuilder builder = new StringBuilder();
            
            var allSkaters = DataGrabber.Players.Where(s => s is Skater).Cast<Skater>().ToList();
            var allGoalies = DataGrabber.Players.Where(s => s is Goalie).Cast<Goalie>().ToList();

            var allPros = DataGrabber.Teams.Where(t => !(t is FarmTeam)).ToList();
            var allFarms = DataGrabber.Teams.Where(t => t is FarmTeam).ToList();

            string skaters = AppendPlayers(allSkaters);
            string goalies = AppendGoalies(allGoalies);
            string proTeams = AppendTeams(allPros);
            string farmTeams = AppendTeams(allFarms);

            builder.Append(skaters);
            builder.AppendLine(string.Empty);
            builder.Append(goalies);
            builder.AppendLine(string.Empty);
            builder.Append(proTeams);
            builder.AppendLine(string.Empty);
            builder.Append(farmTeams);

            File.WriteAllText(path, builder.ToString());
        }

        private static string AppendPlayers (List<Skater> allSkaters)
        {
            StringBuilder builder = new StringBuilder();
            List<string> headers = allSkaters.First().Values.Keys.ToList().ToList();
            headers.Add("Team");
            headers.RemoveAt(0);

            var header = string.Join("\t", headers);
            builder.AppendLine(header);

            foreach (var skater in allSkaters)
            {
                string teamDisplay = GenerateTeamDisplay(skater);

                var line = string.Join("\t", skater.Values.Values.Concat(new[] { teamDisplay }).Skip(1));
                builder.AppendLine(line);
            }
            return builder.ToString();
        }

        private static string AppendGoalies (List<Goalie> allGoalies)
        {
            StringBuilder builder = new StringBuilder();
            List<string> headers = allGoalies.First().Values.Keys.ToList().ToList();
            headers.Add("Team");

            var header = string.Join("\t", headers);
            builder.AppendLine(header);

            foreach (var goalie in allGoalies)
            {
                string teamDisplay = GenerateTeamDisplay(goalie);

                var line = string.Join("\t", goalie.Values.Values.Concat(new[] { teamDisplay }));
                builder.AppendLine(line);
            }
            return builder.ToString();
        }

        private static string AppendTeams (List<Team> teams)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(AppendTeam(teams));
            
            return builder.ToString();
        }

        private static string AppendTeam (List<Team> teams)
        {
            StringBuilder teamBuilder = new StringBuilder();
            foreach (var team in teams)
            {
                teamBuilder.AppendLine(team.Name);

                var players = DataGrabber.Players.Where(p => p.Team.Equals(team)).ToList();

                teamBuilder.Append(AppendPlayers(players.Where(p => p is Skater).Cast<Skater>().ToList()));
                teamBuilder.Append(AppendGoalies(players.Where(p => p is Goalie).Cast<Goalie>().ToList()));
                teamBuilder.AppendLine(string.Empty);
            }
            return teamBuilder.ToString();
        }

        private static string GenerateTeamDisplay(Player player)
        {
            string teamDisplay;
            var farmTeam = player.Team as FarmTeam;
            if ( farmTeam != null )
            {
                teamDisplay = farmTeam.Name + " - " + farmTeam.ProTeam.Name;
            }
            else
            {
                teamDisplay = player.Team.Name;
            }
            return teamDisplay;
        }

    }
}

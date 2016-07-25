using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using GM.Model;
using GM.ViewModel;

namespace GM.Persistence
{
    public class CsvExport
    {
        public void Export (MainWindowViewModel mainWindowViewModel, string path)
        {
            if (mainWindowViewModel == null || mainWindowViewModel.AllGoaliesOverviewViewModel == null ||
                mainWindowViewModel.AllSkatersOverviewViewModel == null || mainWindowViewModel.TeamsOverviewViewModel == null)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            
            var allSkaters = mainWindowViewModel.AllSkatersOverviewViewModel.Players.ToList();
            var allGoalies = mainWindowViewModel.AllGoaliesOverviewViewModel.Players.ToList();

            mainWindowViewModel.TeamsOverviewViewModel.ShowPro = true;
            var allPros = mainWindowViewModel.TeamsOverviewViewModel.Teams.ToList();

            mainWindowViewModel.TeamsOverviewViewModel.ShowPro = false;
            var allFarms = mainWindowViewModel.TeamsOverviewViewModel.Teams.ToList();
            
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

        private static string AppendTeams (List<TeamViewModel> teams)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(AppendTeam(teams));
            
            return builder.ToString();
        }

        private static string AppendTeam (List<TeamViewModel> teams)
        {
            StringBuilder teamBuilder = new StringBuilder();
            foreach (var teamViewModel in teams)
            {
                teamBuilder.AppendLine(teamViewModel.TeamName);
                teamBuilder.Append(AppendPlayers(teamViewModel.Skaters.ToList()));
                teamBuilder.Append(AppendGoalies(teamViewModel.Goalies.ToList()));
                teamBuilder.AppendLine(string.Empty);
            }
            return teamBuilder.ToString();
        }

        private static string GenerateTeamDisplay(Player player)
        {
            var teamDisplay = string.Empty;
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

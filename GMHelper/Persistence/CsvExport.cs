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
            
            var allSkaters = mainWindowViewModel.AllSkatersOverviewViewModel.Skaters.ToList();
            var allGoalies = mainWindowViewModel.AllGoaliesOverviewViewModel.Goalies.ToList();

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
                var line = string.Join("\t", skater.Values.Values.Concat(new[] { skater.Team.Name }).Skip(1));
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
                var line = string.Join("\t", goalie.Values.Values.Concat(new[] { goalie.Team.Name }));
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
    }
}

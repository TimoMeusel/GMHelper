using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GM.Model;

namespace GM.ViewModel
{
    /// <summary>
    ///     Tries to grab the current data from the webserver
    /// </summary>
    public class GrabCommand: BaseCommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        /// <summary>
        ///     Instantiates the command to grab the data
        /// </summary>
        public GrabCommand (MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        /// <summary>
        ///     Overrides <see cref="BaseCommand.CanExecute"/>
        /// </summary>
        public override bool CanExecute (object parameter)
        {
            return true;
        }
        
        /// <summary>
        ///     Overrides <see cref="BaseCommand.Execute"/>
        /// </summary>
        public override void Execute (object parameter)
        {
            try
            {
                // load the players from the website
                var players =
                    new ObservableCollection<Player>(
                        DataGrabber.GrabPlayers());

                var teams = SetUpTeams(players);
                _mainWindowViewModel.Teams = teams;
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        private static List<TeamViewModel> SetUpTeams (ObservableCollection<Player> players)
        {
            Dictionary<Player, Team> teamMap = new Dictionary<Player, Team>();
            foreach (Player player in players)
            {
                teamMap.Add(player, player.Team);
            }

            List<Team> teams = teamMap.Select(p => p.Value).Distinct().ToList();
            List<TeamViewModel> teamsViewModels = new List<TeamViewModel>();

            foreach (var team in teams)
            {
                List<Skater> skaters =
                    teamMap.Where(t => t.Value.Equals(team)).Where(t => t.Key is Skater).Select(t => t.Key).Cast<Skater>().ToList();
                List<Goalie> goalies =
                    teamMap.Where(t => t.Value.Equals(team)).Where(t => t.Key is Goalie).Select(t => t.Key).Cast<Goalie>().ToList();

                teamsViewModels.Add(new TeamViewModel(team, skaters, goalies));
            }
            return teamsViewModels;
        }
    }
}

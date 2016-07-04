using System;
using GM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace GM.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private IEnumerable<TeamViewModel> _teams;

        public MainWindowViewModel()
        {
            try
            {
                var players =
                    new ObservableCollection<Player>(
                        DataGrabber.Grab("http://nfhl.eu.dd25712.kasserver.com/2/NFHL-ProTeamRoster.html"));


                Dictionary<Player, string> teamMap = new Dictionary<Player, string>();
                foreach (Player player in players)
                {
                    teamMap.Add(player, player.Team);
                }

                List<string> teamNames = teamMap.Select(p => p.Value).Distinct().ToList();
                List<TeamViewModel> teams = new List<TeamViewModel>();

                foreach (var teamName in teamNames)
                {
                    List<Skater> skaters = teamMap.Where(t => t.Value == teamName).Where(t => t.Key is Skater).Select(t => t.Key).Cast<Skater>().ToList();
                    List<Goalie> goalies = teamMap.Where(t => t.Value == teamName).Where(t => t.Key is Goalie).Select(t => t.Key).Cast<Goalie>().ToList();

                    teams.Add(new TeamViewModel(teamName, skaters, goalies));
                }
                Teams = teams;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public IEnumerable<TeamViewModel> Teams { get
            {
                return _teams;
            }
            set
            {
                _teams = value;
                OnPropertyChanged("Teams");
            }
        }
    }
}

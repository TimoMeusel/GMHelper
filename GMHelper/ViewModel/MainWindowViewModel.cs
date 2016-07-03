using GM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GM.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private IEnumerable<TeamViewModel> _teams;

        public MainWindowViewModel()
        {
            var players = new ObservableCollection<Player>(DataGrabber.Grab("http://nfhl.eu.dd25712.kasserver.com/2/NFHL-ProTeamRoster.html"));

            var teamMap = players.ToDictionary(x => x, y => y.Team);
            List<string> teamNames = teamMap.Select(p => p.Value).Distinct().ToList();
            List<TeamViewModel> teams = new List<TeamViewModel>();

            foreach(var teamName in teamNames)
            {
                teams.Add(new TeamViewModel(teamName, teamMap.Where(t => t.Value == teamName).Select(t => t.Key)));
            }
            Teams = teams;
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

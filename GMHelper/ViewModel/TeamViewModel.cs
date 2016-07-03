using GM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GM.ViewModel
{
    public class TeamViewModel
    {
        public TeamViewModel(string teamName, IEnumerable<Player> players)
        {
            TeamName = teamName;
            Players = new ObservableCollection<Player>(players);
        }

        public string TeamName { get; set; }
        public ObservableCollection<Player> Players { get; set; }
    }
}

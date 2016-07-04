using GM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GM.ViewModel
{
    public class TeamViewModel
    {
        public TeamViewModel(string teamName, IEnumerable<Skater> skaters, IEnumerable<Goalie> goalies)
        {
            TeamName = teamName;
            Skaters = new ObservableCollection<Skater>(skaters);
            Goalies = new ObservableCollection<Goalie>(goalies);
        }

        public string TeamName { get; set; }
        public ObservableCollection<Skater> Skaters{ get; set; }
        public ObservableCollection<Goalie> Goalies{ get; set; }
    }
}

using GM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GM.ViewModel
{
    /// <summary>
    ///     Represents a team
    /// </summary>
    public class TeamViewModel
    {
        /// <summary>
        ///     Instantiates a team with
        /// </summary>
        public TeamViewModel(Team team, IEnumerable<Skater> skaters, IEnumerable<Goalie> goalies)
        {
            TeamName = team.Name;
            IsPro = team.IsPro;
            Skaters = new ObservableCollection<Skater>(skaters);
            Goalies = new ObservableCollection<Goalie>(goalies);
        }

        /// <summary>
        ///     The name of the team
        /// </summary>
        public string TeamName { get; }
        
        /// <summary>
        ///     Indicator whether team is pro or farm
        /// </summary>
        public bool IsPro { get; }

        /// <summary>
        ///     All skaters
        /// </summary>
        public ObservableCollection<Skater> Skaters{ get; }

        /// <summary>
        ///     All goalies
        /// </summary>
        public ObservableCollection<Goalie> Goalies{ get; }
    }
}

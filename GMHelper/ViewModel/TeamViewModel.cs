using System.Collections.Generic;
using System.Collections.ObjectModel;
using GM.Model;

namespace GM.ViewModel
{
    /// <summary>
    ///     Represents a team
    /// </summary>
    public class TeamViewModel: ViewModelBase
    {
        private readonly Team _team;

        /// <summary>
        ///     Instantiates a team with
        /// </summary>
        public TeamViewModel(Team team, IEnumerable<Skater> skaters, IEnumerable<Goalie> goalies)
        {
            _team = team;
            Skaters = new ObservableCollection<Skater>(skaters);
            Goalies = new ObservableCollection<Goalie>(goalies);
            IsPro = !(team is FarmTeam);
            
            OnPropertyChanged("HeaderText");
        }

        public string HeaderText
        {
            get
            {
                string headerText = TeamName;
                if (!IsPro)
                {
                    headerText += " - " + ((FarmTeam)_team).ProTeam.Name;
                }
                return headerText;
            }
        }

        /// <summary>
        ///     The name of the team
        /// </summary>
        public string TeamName
        {
            get { return _team.Name; }
        }

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

using System.Collections.Generic;
using System.Linq;

namespace GM.ViewModel
{
    public class TeamsOverviewViewModel: ViewModelBase
    {
        private IEnumerable<TeamViewModel> _teams;
        private bool _showPro;
        private PlayerViewModel _selectedPlayer;

        public IEnumerable<TeamViewModel> Teams
        {
            get
            {
                return _teams.Where(t => t.IsPro == ShowPro);
            }
            set
            {
                _teams = value;
                OnPropertyChanged("Teams");
            }
        }

        public bool ShowPro
        {
            get { return _showPro; }
            set
            {
                _showPro = value;
                OnPropertyChanged("ShowPro");
                OnPropertyChanged("Teams");
            }
        }

        public PlayerViewModel SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                OnPropertyChanged();
                OnPropertyChanged("Url");
            }
        }

        public string Url
        {
            get
            {
                if ( SelectedPlayer?.EliteProspectsId != null )
                {
                    return $"http://www.eliteprospects.com/iframe_player_stats.php?player={SelectedPlayer.EliteProspectsId}";
                }

                return $"http://www.eliteprospects.com/iframe_player_stats.php";
            }
        }
    }
}
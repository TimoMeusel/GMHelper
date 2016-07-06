using System.Collections.Generic;
using System.Linq;

namespace GM.ViewModel
{
    public class TeamsOverviewViewModel: ViewModelBase
    {
        private IEnumerable<TeamViewModel> _teams;
        private bool _showPro;

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
    }
}
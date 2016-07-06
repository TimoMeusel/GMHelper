using System.Collections.Generic;
using GM.Model;

namespace GM.ViewModel
{
    public class AllGoaliesOverviewViewModel: ViewModelBase
    {
        private IEnumerable<Goalie> _goalies;

        public IEnumerable<Goalie> Goalies
        {
            get
            {
                return _goalies;
            }
            set
            {
                _goalies = value;
                OnPropertyChanged("Goalies");
            }
        }
    }
}
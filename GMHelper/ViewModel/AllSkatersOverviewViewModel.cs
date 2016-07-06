using System.Collections.Generic;
using GM.Model;

namespace GM.ViewModel
{
    public class AllSkatersOverviewViewModel: ViewModelBase
    {
        private IEnumerable<Skater> _skaters;

        public IEnumerable<Skater> Skaters
        {
            get
            {
                return _skaters;
            }
            set
            {
                _skaters = value;
                OnPropertyChanged("Skaters");
            }
        }
    }
}
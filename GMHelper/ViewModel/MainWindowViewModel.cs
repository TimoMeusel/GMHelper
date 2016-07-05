using System.Collections.Generic;
using System.Windows.Input;

namespace GM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IEnumerable<TeamViewModel> _teams;

        public MainWindowViewModel()
        {
            GrabCommand = new GrabCommand(this);
        }

        public ICommand GrabCommand { get; private set; }

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

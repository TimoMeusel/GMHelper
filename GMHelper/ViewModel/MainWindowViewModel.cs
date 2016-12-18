using System.Windows.Input;

namespace GM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _current;
        private double _progress;

        public MainWindowViewModel()
        {
            TeamsOverviewViewModel = new TeamsOverviewViewModel();
            AllSkatersOverviewViewModel = new AllSkatersOverviewViewModel();
            AllGoaliesOverviewViewModel = new AllGoaliesOverviewViewModel();
            GrabCommand = new GrabCommand(this);
            EliteProspectsCommand = new EliteProspectsCommand(this);
            CsvExportCommand = new CsvExportCommand();
            CsvImportCommand = new CsvImportCommand();

            ShowAllSkatersCommand = new RelayCommand(o => Current = AllSkatersOverviewViewModel);
            ShowAllGoaliesCommand = new RelayCommand(o => Current = AllGoaliesOverviewViewModel);
            ShowProTeamsCommand = new RelayCommand(ShowProTeams);
            ShowFarmTeamsCommand = new RelayCommand(ShowFarmTeams);

            Current = AllSkatersOverviewViewModel;
        }

        public ICommand GrabCommand { get; private set; }
        public ICommand EliteProspectsCommand { get; private set; }
        public ICommand CsvExportCommand { get; private set; }
        public ICommand CsvImportCommand { get; private set; }
        public ICommand ShowProTeamsCommand { get; private set; }
        public ICommand ShowFarmTeamsCommand { get; private set; }
        public ICommand ShowAllSkatersCommand { get; private set; }
        public ICommand ShowAllGoaliesCommand { get; private set; }

        public TeamsOverviewViewModel TeamsOverviewViewModel { get; set; }

        public AllSkatersOverviewViewModel AllSkatersOverviewViewModel { get; set; }
        public AllGoaliesOverviewViewModel AllGoaliesOverviewViewModel { get; set; }

        public ViewModelBase Current
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged("Current");
            }
        }

        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
                OnPropertyChanged("ProgressVisibility");
            }
        }

        public bool ProgressVisibility => _progress > 0;

        private void ShowFarmTeams(object o)
        {
            TeamsOverviewViewModel.ShowPro = false;
            Current = TeamsOverviewViewModel;
        }

        private void ShowProTeams(object o)
        {
            TeamsOverviewViewModel.ShowPro = true;
            Current = TeamsOverviewViewModel;
        }
    }
}

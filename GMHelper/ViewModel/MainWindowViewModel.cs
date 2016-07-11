using System.Windows.Input;

namespace GM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _current;

        public MainWindowViewModel()
        {
            TeamsOverviewViewModel = new TeamsOverviewViewModel();
            AllSkatersOverviewViewModel = new AllSkatersOverviewViewModel();
            AllGoaliesOverviewViewModel = new AllGoaliesOverviewViewModel();
            GrabCommand = new GrabCommand(this);
            ExcelExportCommand = new ExcelExportCommand();
            CsvExportCommand = new CsvExportCommand();

            ShowAllSkatersCommand = new RelayCommand(o => Current = AllSkatersOverviewViewModel);
            ShowAllGoaliesCommand = new RelayCommand(o => Current = AllGoaliesOverviewViewModel);
            ShowProTeamsCommand = new RelayCommand(ShowProTeams);
            ShowFarmTeamsCommand = new RelayCommand(ShowFarmTeams);

            Current = AllSkatersOverviewViewModel;
        }

        public ICommand GrabCommand { get; private set; }
        public ICommand ExcelExportCommand { get; private set; }
        public ICommand CsvExportCommand { get; private set; }
        public ICommand ShowProTeamsCommand { get; private set; }
        public ICommand ShowFarmTeamsCommand { get; private set; }
        public ICommand ShowAllSkatersCommand { get; private set; }
        public ICommand ShowAllGoaliesCommand { get; private set; }

        public TeamsOverviewViewModel TeamsOverviewViewModel { get; private set; }

        public AllSkatersOverviewViewModel AllSkatersOverviewViewModel { get; private set; }
        public AllGoaliesOverviewViewModel AllGoaliesOverviewViewModel { get; private set; }

        public ViewModelBase Current
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged("Current");
            }
        }


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

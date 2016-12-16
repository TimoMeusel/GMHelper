using GM.Model;
using Microsoft.Win32;

namespace GM.ViewModel
{
    public class CsvImportCommand: BaseCommand
    {
        public override bool CanExecute (object parameter)
        {
            return parameter is MainWindowViewModel;
        }

        protected override void ExecuteInternal (object parameter)
        {
            var viewModel = parameter as MainWindowViewModel;

            var dialog = new OpenFileDialog();
            dialog.Filter = "*CSV|*.csv";

            // if cancel has been pressed, the dialog returns false
            if ( dialog.ShowDialog() == false )
            {
                return;
            }
                
            string path = dialog.FileName;

            DataGrabber.LoadFromCsv(path);

            var teamFactory = new TeamFactory();
            teamFactory.CreateTeams();
            var teams = teamFactory.Teams;
            viewModel.TeamsOverviewViewModel.Teams = teams;
            viewModel.AllSkatersOverviewViewModel.Players = teamFactory.AllSkaters;
            viewModel.AllGoaliesOverviewViewModel.Players = teamFactory.AllGoalies;
        }
    }
}

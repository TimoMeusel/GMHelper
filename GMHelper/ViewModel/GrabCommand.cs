using System;
using System.Windows;
using System.Windows.Input;
using GM.Model;

namespace GM.ViewModel
{
    /// <summary>
    ///     Tries to grab the current data from the webserver
    /// </summary>
    public class GrabCommand: BaseCommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        /// <summary>
        ///     Instantiates the command to grab the data
        /// </summary>
        public GrabCommand (MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        /// <summary>
        ///     Overrides <see cref="BaseCommand.CanExecute"/>
        /// </summary>
        public override bool CanExecute (object parameter)
        {
            return true;
        }
        
        /// <summary>
        ///     Overrides <see cref="BaseCommand.Execute"/>
        /// </summary>
        protected override void ExecuteInternal (object parameter)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                // load the players from the website

                DataGrabber.GrabPlayers();

                var teamFactory = new TeamFactory();
                teamFactory.CreateTeams();
                var teams = teamFactory.Teams;

                _mainWindowViewModel.TeamsOverviewViewModel.Teams = teams;
                _mainWindowViewModel.AllSkatersOverviewViewModel.Players = teamFactory.AllSkaters;
                _mainWindowViewModel.AllGoaliesOverviewViewModel.Players = teamFactory.AllGoalies;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
    }
}

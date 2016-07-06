using System;
using System.Collections.ObjectModel;
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
        public override void Execute (object parameter)
        {
            var currentCursor = Mouse.OverrideCursor;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                // load the players from the website
                var players =
                    new ObservableCollection<Player>(
                        DataGrabber.GrabPlayers());

                var teamRegistry = new TeamRegistry();
                var teams = teamRegistry.CreateTeams(players);
                _mainWindowViewModel.TeamsOverviewViewModel.Teams = teams;
                _mainWindowViewModel.AllSkatersOverviewViewModel.Skaters = teamRegistry.AllSkaters;
                _mainWindowViewModel.AllGoaliesOverviewViewModel.Goalies = teamRegistry.AllGoalies;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace);
            }
            finally
            {
                Mouse.OverrideCursor = currentCursor;
            }
        }
    }
}

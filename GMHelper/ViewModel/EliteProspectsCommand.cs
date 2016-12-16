using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GM.Model;

namespace GM.ViewModel
{
    /// <summary>
    ///     Tries to grab the current data from the webserver
    /// </summary>
    public class EliteProspectsCommand : BaseCommand
    {
        private readonly BackgroundWorker _bgWorker;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public EliteProspectsCommand (MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;

            _bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
            _bgWorker.DoWork += BgWorkerOnDoWork;
            _bgWorker.RunWorkerCompleted += BgWorkerOnRunWorkerCompleted;
            _bgWorker.ProgressChanged += BgWorkerOnProgressChanged;
        }

        private void BgWorkerOnRunWorkerCompleted (object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            _mainWindowViewModel.Progress = 0;
        }

        private void BgWorkerOnProgressChanged (object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            _mainWindowViewModel.Progress = progressChangedEventArgs.ProgressPercentage;
        }

        /// <summary>
        ///     Overrides <see cref="BaseCommand.CanExecute"/>
        /// </summary>
        public override bool CanExecute(object parameter)
        {
            return DataGrabber.Players != null && DataGrabber.Players.Any();
        }

        /// <summary>
        ///     Overrides <see cref="BaseCommand.Execute"/>
        /// </summary>
        protected override void ExecuteInternal(object parameter)
        {
            try
            {
                _bgWorker.RunWorkerAsync();
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        private void BgWorkerOnDoWork (object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var task = new Task(DataGrabber.GetEliteProspectsId);
            task.Start();

            do
            {
                int total = DataGrabber.Players.Count;
                int haveId = DataGrabber.Players.Count(p => !string.IsNullOrWhiteSpace(p.EliteProspectsId));
                var ratio = (double)haveId / total * 100;
                _bgWorker.WorkerReportsProgress = true;
                _bgWorker.ReportProgress((int)ratio);
            }
            while (!task.IsCompleted);
        }
    }
}

using System.Linq;
using GM.Persistence;
using Microsoft.Win32;

namespace GM.ViewModel
{
    public class ExcelExportCommand: BaseCommand
    {
        /// <summary>
        ///     Overrides <see cref="BaseCommand.CanExecute"/>
        /// </summary>
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as MainWindowViewModel;
            var players = viewModel?.AllSkatersOverviewViewModel?.Skaters;
            return players != null && players.Any();
        }

        /// <summary>
        ///     Overrides <see cref="BaseCommand.Execute"/>
        /// </summary>
        protected override void ExecuteInternal (object parameter)
        {
            var viewModel = parameter as MainWindowViewModel;
            var players = viewModel?.AllSkatersOverviewViewModel?.Skaters;

            ExcelExport export = new ExcelExport();
            
            var dialog = new SaveFileDialog();

            // if cancel has been pressed, the dialog returns false
            if (dialog.ShowDialog() == false)
            {
                return;
            }

            string path = dialog.FileName;
            export.Export(players.ToList(), path);
        }
    }
}

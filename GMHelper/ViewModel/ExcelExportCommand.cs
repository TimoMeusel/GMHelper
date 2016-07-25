using System.Linq;
using GM.Persistence;
using Microsoft.Win32;

namespace GM.ViewModel
{
    /// <summary>
    ///     http://christian.bloggingon.net/archive/2008/09/05/exceldateien-mit-c-erstellen.aspx
    /// </summary>
    public class ExcelExportCommand: BaseCommand
    {
        /// <summary>
        ///     Overrides <see cref="BaseCommand.CanExecute"/>
        /// </summary>
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as MainWindowViewModel;
            var players = viewModel?.AllSkatersOverviewViewModel?.Players;
            return players != null && players.Any();
        }

        /// <summary>
        ///     Overrides <see cref="BaseCommand.Execute"/>
        /// </summary>
        protected override void ExecuteInternal (object parameter)
        {
            var viewModel = parameter as MainWindowViewModel;
            var players = viewModel?.AllSkatersOverviewViewModel?.Players;

            ExcelExport export = new ExcelExport();
            
            var dialog = new SaveFileDialog();
            dialog.Filter = "*Excel|*.xlsx";


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

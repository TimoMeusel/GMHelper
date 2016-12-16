using System.Linq;
using GM.Model;
using GM.Persistence;
using Microsoft.Win32;

namespace GM.ViewModel
{
    public class CsvExportCommand: BaseCommand
    {

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
            CsvExport export = new CsvExport();

            var dialog = new SaveFileDialog { Filter = "*CSV|*.csv" };

            // if cancel has been pressed, the dialog returns false
            if ( dialog.ShowDialog() == false )
            {
                return;
            }

            string path = dialog.FileName;

            export.Export(path);
        }
    }
}

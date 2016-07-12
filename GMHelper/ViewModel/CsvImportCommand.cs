using GM.Persistence;
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

            var csvImport = new CsvImport();
            var skaters = csvImport.Load(path);

            viewModel.AllSkatersOverviewViewModel.Skaters = skaters;
        }
    }
}

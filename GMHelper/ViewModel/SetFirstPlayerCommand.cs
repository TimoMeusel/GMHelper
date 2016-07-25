using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM.Model;

namespace GM.ViewModel
{
    public class SetFirstPlayerCommand<T>:BaseCommand where T:Player
    {
        private readonly OverviewViewModel<T> _overviewViewModel;

        public SetFirstPlayerCommand (OverviewViewModel<T> overviewViewModel )
        {
            _overviewViewModel = overviewViewModel;
        }

        public override bool CanExecute (object parameter)
        {
            return _overviewViewModel?.Players != null && _overviewViewModel.Players.Any() && parameter is T;
        }

        protected override void ExecuteInternal (object parameter)
        {
            _overviewViewModel.ComparisonViewModel.FirstPlayer = parameter as T;
        }
    }
}

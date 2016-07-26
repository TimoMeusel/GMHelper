using System.Linq;
using GM.Model;

namespace GM.ViewModel
{
    public class SetSecondPlayerCommand<T>:BaseCommand where T:PlayerViewModel
    {
        private readonly OverviewViewModel<T> _overviewViewModel;

        public SetSecondPlayerCommand(OverviewViewModel<T> overviewViewModel )
        {
            _overviewViewModel = overviewViewModel;
        }

        public override bool CanExecute (object parameter)
        {
            return _overviewViewModel?.Players != null && _overviewViewModel.Players.Any() && parameter is T;
        }

        protected override void ExecuteInternal (object parameter)
        {
            _overviewViewModel.ComparisonViewModel.SecondPlayer = parameter as T;
        }
    }
}

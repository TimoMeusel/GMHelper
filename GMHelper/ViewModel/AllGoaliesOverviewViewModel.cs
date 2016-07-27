namespace GM.ViewModel
{
    public class AllGoaliesOverviewViewModel: OverviewViewModel<GoalieViewModel>
    {
        public AllGoaliesOverviewViewModel ()
        {
            SetFirstPlayerCommand = new SetFirstPlayerCommand<GoalieViewModel>(this);
            SetSecondPlayerCommand = new SetSecondPlayerCommand<GoalieViewModel>(this);
            ComparisonViewModel = new ComparisonViewModel<GoalieViewModel>();
        }
    }
}
using System.Collections.Generic;
using System.Windows.Input;
using GM.Model;

namespace GM.ViewModel
{
    public class AllSkatersOverviewViewModel: OverviewViewModel<Skater>
    {
        public AllSkatersOverviewViewModel ()
        {
            SetFirstPlayerCommand = new SetFirstPlayerCommand<Skater>(this);
            SetSecondPlayerCommand = new SetSecondPlayerCommand<Skater>(this);
            ComparisonViewModel = new ComparisonViewModel<Skater>();
        }
    }
}
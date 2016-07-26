﻿using System.Collections.Generic;
using System.Windows.Input;
using GM.Model;

namespace GM.ViewModel
{
    public class AllSkatersOverviewViewModel: OverviewViewModel<SkaterViewModel>
    {
        public AllSkatersOverviewViewModel ()
        {
            SetFirstPlayerCommand = new SetFirstPlayerCommand<SkaterViewModel>(this);
            SetSecondPlayerCommand = new SetSecondPlayerCommand<SkaterViewModel>(this);
            ComparisonViewModel = new ComparisonViewModel<SkaterViewModel>();
        }
    }
}
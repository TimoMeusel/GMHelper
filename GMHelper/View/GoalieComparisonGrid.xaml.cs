using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using GM.ViewModel;

namespace GM.View
{
    /// <summary>
    /// Interaction logic for ComparisonGrid.xaml
    /// </summary>
    public partial class GoalieComparisonGrid
    {
        private readonly GoalieViewModel[] _shadowCopy;

        public GoalieComparisonGrid()
        {
            InitializeComponent();
            _shadowCopy = new GoalieViewModel[2];
            Players = new ObservableCollection<GoalieViewModel>();
        }

        #region Players

        public static readonly DependencyProperty PlayersProperty =
            DependencyProperty.Register("Players", typeof(ObservableCollection<GoalieViewModel>), typeof(GoalieComparisonGrid), new UIPropertyMetadata(null));

        public ObservableCollection<GoalieViewModel> Players
        {
            [ExcludeFromCodeCoverage]
            get { return (ObservableCollection<GoalieViewModel> )GetValue(PlayersProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(PlayersProperty, value); }
        }

        #endregion

        #region FirstPlayer

        public static readonly DependencyProperty FirstPlayerProperty =
            DependencyProperty.Register("FirstPlayer", typeof(GoalieViewModel), typeof(GoalieComparisonGrid), new UIPropertyMetadata(null, FirstPlayerChanged));

        private static void FirstPlayerChanged (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if ( dependencyPropertyChangedEventArgs.NewValue == null )
            {
                return;
            }

            var comparisonGrid = (GoalieComparisonGrid)dependencyObject;
            var newPlayer = ( GoalieViewModel ) dependencyPropertyChangedEventArgs.NewValue;
            
            if (comparisonGrid._shadowCopy[0] != null)
            {
                comparisonGrid.Players.RemoveAt(0);
            }

            comparisonGrid.Players.Insert(0, newPlayer);
            comparisonGrid._shadowCopy[0] = newPlayer;
        }

        public GoalieViewModel FirstPlayer
        {
            [ExcludeFromCodeCoverage]
            get { return ( GoalieViewModel ) GetValue(FirstPlayerProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(FirstPlayerProperty, value); }
        }

        #endregion


        #region SecondPlayer

        public static readonly DependencyProperty SecondPlayerProperty =
            DependencyProperty.Register("SecondPlayer", typeof(GoalieViewModel), typeof(GoalieComparisonGrid), new UIPropertyMetadata(null, SecondPlayerChanged));

        private static void SecondPlayerChanged (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if ( dependencyPropertyChangedEventArgs.NewValue == null )
            {
                return;
            }

            var comparisonGrid = ( GoalieComparisonGrid ) dependencyObject;
            var newPlayer = ( GoalieViewModel ) dependencyPropertyChangedEventArgs.NewValue;

            if ( comparisonGrid._shadowCopy[1] != null )
            {
                comparisonGrid.Players.RemoveAt(1);
            }

            comparisonGrid.Players.Insert(1, newPlayer);
            comparisonGrid._shadowCopy[1] = newPlayer;
        }

        public GoalieViewModel SecondPlayer
        {
            [ExcludeFromCodeCoverage]
            get
            { return ( GoalieViewModel ) GetValue(SecondPlayerProperty); }

            [ExcludeFromCodeCoverage]
            set
            { SetValue(SecondPlayerProperty, value); }
        }

        #endregion
        
        #region Comparison

        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register("Comparison", typeof(GoalieViewModel), typeof(GoalieComparisonGrid), new UIPropertyMetadata(null));
        
        public GoalieViewModel Comparison
        {
            [ExcludeFromCodeCoverage]
            get
            { return ( GoalieViewModel ) GetValue(ComparisonProperty); }

            [ExcludeFromCodeCoverage]
            set
            { SetValue(ComparisonProperty, value); }
        }

        #endregion

        #region Headers

        public static readonly DependencyProperty HeadersProperty =
            DependencyProperty.Register("Headers", typeof(IEnumerable<string>), typeof(GoalieComparisonGrid), new UIPropertyMetadata(null));

        public IEnumerable<string> Headers
        {
            [ExcludeFromCodeCoverage]
            get { return (IEnumerable<string>)GetValue(HeadersProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(HeadersProperty, value); }
        }

        #endregion

        private void CalculatedDelta ()
        {
            var p1 = FirstPlayer;
            var p2 = SecondPlayer;
            var headers = Headers.ToArray();

            if (p1.GetType() != p2.GetType())
            {
                return;
            }

            double[] delta = new double[p1.Values.Count];

            for (var i = 0; i < p1.Values.Count; i++)
            {
                double value1;
                if (!double.TryParse(p1.Values[headers[i]], out value1))
                {
                    value1 = double.PositiveInfinity;
                }

                double value2;
                if ( !double.TryParse(p2.Values[headers[i]], out value2) )
                {
                    value2 = double.PositiveInfinity;
                }

                delta[i] = value1 - value2;
            }
        }
    }
}

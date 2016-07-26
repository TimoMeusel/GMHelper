using System;
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
    public partial class ComparisonGrid
    {
        private readonly PlayerViewModel[] _shadowCopy;

        public ComparisonGrid()
        {
            InitializeComponent();
            _shadowCopy = new PlayerViewModel[2];
            Players = new ObservableCollection<PlayerViewModel>();
        }

        #region Players

        public static readonly DependencyProperty PlayersProperty =
            DependencyProperty.Register("Players", typeof(ObservableCollection<PlayerViewModel>), typeof(ComparisonGrid), new UIPropertyMetadata(null));

        public ObservableCollection<PlayerViewModel> Players
        {
            [ExcludeFromCodeCoverage]
            get { return (ObservableCollection<PlayerViewModel> )GetValue(PlayersProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(PlayersProperty, value); }
        }

        #endregion

        #region FirstPlayer

        public static readonly DependencyProperty FirstPlayerProperty =
            DependencyProperty.Register("FirstPlayer", typeof(PlayerViewModel), typeof(ComparisonGrid), new UIPropertyMetadata(null, FirstPlayerChanged));

        private static void FirstPlayerChanged (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if ( dependencyPropertyChangedEventArgs.NewValue == null )
            {
                return;
            }

            var comparisonGrid = (ComparisonGrid)dependencyObject;
            var newPlayer = (PlayerViewModel)dependencyPropertyChangedEventArgs.NewValue;

            if (comparisonGrid._shadowCopy[0] != null)
            {
                comparisonGrid.Players.RemoveAt(0);
            }

            comparisonGrid.Players.Insert(0, newPlayer);
            comparisonGrid._shadowCopy[0] = newPlayer;
        }

        public PlayerViewModel FirstPlayer
        {
            [ExcludeFromCodeCoverage]
            get { return ( PlayerViewModel ) GetValue(FirstPlayerProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(FirstPlayerProperty, value); }
        }

        #endregion


        #region SecondPlayer

        public static readonly DependencyProperty SecondPlayerProperty =
            DependencyProperty.Register("SecondPlayer", typeof(PlayerViewModel), typeof(ComparisonGrid), new UIPropertyMetadata(null, SecondPlayerChanged));

        private static void SecondPlayerChanged (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if ( dependencyPropertyChangedEventArgs.NewValue == null )
            {
                return;
            }

            var comparisonGrid = ( ComparisonGrid ) dependencyObject;
            var newPlayer = ( PlayerViewModel ) dependencyPropertyChangedEventArgs.NewValue;

            if ( comparisonGrid._shadowCopy[1] != null )
            {
                comparisonGrid.Players.RemoveAt(1);
            }

            comparisonGrid.Players.Insert(1, newPlayer);
            comparisonGrid._shadowCopy[1] = newPlayer;
        }

        public PlayerViewModel SecondPlayer
        {
            [ExcludeFromCodeCoverage]
            get
            { return ( PlayerViewModel ) GetValue(SecondPlayerProperty); }

            [ExcludeFromCodeCoverage]
            set
            { SetValue(SecondPlayerProperty, value); }
        }

        #endregion
        
        #region Comparison

        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register("Comparison", typeof(PlayerViewModel), typeof(ComparisonGrid), new UIPropertyMetadata(null));
        
        public PlayerViewModel Comparison
        {
            [ExcludeFromCodeCoverage]
            get
            { return ( PlayerViewModel ) GetValue(ComparisonProperty); }

            [ExcludeFromCodeCoverage]
            set
            { SetValue(ComparisonProperty, value); }
        }

        #endregion

        #region Headers

        public static readonly DependencyProperty HeadersProperty =
            DependencyProperty.Register("Headers", typeof(IEnumerable<string>), typeof(ComparisonGrid), new UIPropertyMetadata(null));

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

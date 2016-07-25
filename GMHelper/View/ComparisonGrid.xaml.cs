using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GM.Model;

namespace GM.View
{
    /// <summary>
    /// Interaction logic for ComparisonGrid.xaml
    /// </summary>
    public partial class ComparisonGrid
    {
        public ComparisonGrid()
        {
            InitializeComponent();
            Players = new ObservableCollection<Player>();
        }

        #region Players

        public static readonly DependencyProperty PlayersProperty =
            DependencyProperty.Register("Players", typeof(ObservableCollection<Player>), typeof(ComparisonGrid), new UIPropertyMetadata(null));

        public ObservableCollection<Player> Players
        {
            [ExcludeFromCodeCoverage]
            get { return (ObservableCollection<Player>)GetValue(PlayersProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(PlayersProperty, value); }
        }

        #endregion

        #region FirstPlayer

        public static readonly DependencyProperty FirstPlayerProperty =
            DependencyProperty.Register("FirstPlayer", typeof(Player), typeof(ComparisonGrid), new UIPropertyMetadata(null, PropertyChangedCallback));
        
        public Player FirstPlayer
        {
            [ExcludeFromCodeCoverage]
            get { return (Player)GetValue(FirstPlayerProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(FirstPlayerProperty, value); }
        }

        #endregion


        #region SecondPlayer

        public static readonly DependencyProperty SecondPlayerProperty =
            DependencyProperty.Register("SecondPlayer", typeof(Player), typeof(ComparisonGrid), new UIPropertyMetadata(null, PropertyChangedCallback));
        
        public Player SecondPlayer
        {
            [ExcludeFromCodeCoverage]
            get
            { return ( Player ) GetValue(SecondPlayerProperty); }

            [ExcludeFromCodeCoverage]
            set
            { SetValue(SecondPlayerProperty, value); }
        }

        #endregion

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var comparisonGrid = ( ComparisonGrid ) dependencyObject;
            comparisonGrid.Players.Clear();

            if (comparisonGrid.FirstPlayer != null)
            {
                comparisonGrid.Players.Add(comparisonGrid.FirstPlayer);
            }
            else
            {
                comparisonGrid.Players.Add(null);
            }

            if ( comparisonGrid.FirstPlayer != null )
            {
                comparisonGrid.Players.Add(comparisonGrid.SecondPlayer);
            }
            else
            {
                comparisonGrid.Players.Add(null);
            }
        }

        #region Comparison

        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register("Comparison", typeof(Player), typeof(ComparisonGrid), new UIPropertyMetadata(null));
        
        public Player Comparison
        {
            [ExcludeFromCodeCoverage]
            get
            { return ( Player ) GetValue(ComparisonProperty); }

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

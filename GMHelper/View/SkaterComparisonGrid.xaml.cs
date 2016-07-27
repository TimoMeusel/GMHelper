using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GM.ViewModel;

namespace GM.View
{
    /// <summary>
    /// Interaction logic for ComparisonGrid.xaml
    /// </summary>
    public partial class SkaterComparisonGrid
    {
        private readonly SkaterViewModel[] _shadowCopy;

        public SkaterComparisonGrid()
        {
            InitializeComponent();
            _shadowCopy = new SkaterViewModel[2];
            Players = new ObservableCollection<SkaterViewModel>();

            LayoutUpdated += (sender, args) => Compare();
        }

        #region Players

        public static readonly DependencyProperty PlayersProperty =
            DependencyProperty.Register("Players", typeof(ObservableCollection<SkaterViewModel>), typeof(SkaterComparisonGrid), new UIPropertyMetadata(null));

        public ObservableCollection<SkaterViewModel> Players
        {
            [ExcludeFromCodeCoverage]
            get { return (ObservableCollection<SkaterViewModel> )GetValue(PlayersProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(PlayersProperty, value); }
        }

        #endregion

        #region FirstPlayer

        public static readonly DependencyProperty FirstPlayerProperty =
            DependencyProperty.Register("FirstPlayer", typeof(SkaterViewModel), typeof(SkaterComparisonGrid), new UIPropertyMetadata(null, FirstPlayerChanged));

        private static void FirstPlayerChanged (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if ( dependencyPropertyChangedEventArgs.NewValue == null )
            {
                return;
            }

            var comparisonGrid = (SkaterComparisonGrid)dependencyObject;
            var newPlayer = ( SkaterViewModel ) dependencyPropertyChangedEventArgs.NewValue;
            
            if (comparisonGrid._shadowCopy[0] != null)
            {
                comparisonGrid.Players.RemoveAt(0);
            }

            comparisonGrid.Players.Insert(0, newPlayer);
            comparisonGrid._shadowCopy[0] = newPlayer;

            comparisonGrid.Compare();
        }

        public SkaterViewModel FirstPlayer
        {
            [ExcludeFromCodeCoverage]
            get { return ( SkaterViewModel ) GetValue(FirstPlayerProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(FirstPlayerProperty, value); }
        }

        #endregion


        #region SecondPlayer

        public static readonly DependencyProperty SecondPlayerProperty =
            DependencyProperty.Register("SecondPlayer", typeof(SkaterViewModel), typeof(SkaterComparisonGrid), new UIPropertyMetadata(null, SecondPlayerChanged));

        private static void SecondPlayerChanged (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if ( dependencyPropertyChangedEventArgs.NewValue == null )
            {
                return;
            }

            var comparisonGrid = ( SkaterComparisonGrid ) dependencyObject;
            var newPlayer = ( SkaterViewModel ) dependencyPropertyChangedEventArgs.NewValue;

            if ( comparisonGrid._shadowCopy[1] != null )
            {
                comparisonGrid.Players.RemoveAt(1);
            }

            comparisonGrid.Players.Add(newPlayer);
            comparisonGrid._shadowCopy[1] = newPlayer;
            
            comparisonGrid.Compare();
        }

        public SkaterViewModel SecondPlayer
        {
            [ExcludeFromCodeCoverage]
            get
            { return ( SkaterViewModel ) GetValue(SecondPlayerProperty); }

            [ExcludeFromCodeCoverage]
            set
            { SetValue(SecondPlayerProperty, value); }
        }

        #endregion
        
        #region Headers

        public static readonly DependencyProperty HeadersProperty =
            DependencyProperty.Register("Headers", typeof(IEnumerable<string>), typeof(SkaterComparisonGrid), new UIPropertyMetadata(null));

        public IEnumerable<string> Headers
        {
            [ExcludeFromCodeCoverage]
            get { return (IEnumerable<string>)GetValue(HeadersProperty); }

            [ExcludeFromCodeCoverage]
            set { SetValue(HeadersProperty, value); }
        }

        #endregion

        #region Private Helper

        private void Compare ()
        {
            if (Items.Count != 2)
            {
                return;
            }

            var firstRow = (DataGridRow)ItemContainerGenerator.ContainerFromItem(Items[0]);
            var secondRow = (DataGridRow)ItemContainerGenerator.ContainerFromItem(Items[1]);

            if (firstRow == null || secondRow == null)
            {
                return;
            }

            var firstCells = GetVisualDescendants<DataGridCell>(firstRow).ToList();
            var secondCells = GetVisualDescendants<DataGridCell>(secondRow).ToList();

            if (firstCells.Count != secondCells.Count)
            {
                //throw new InvalidOperationException("Inconsistent count of cells for comparison");
                return;
            }

            // Compare all but Age, Contract and Salary
            for (int i = 0; i < firstCells.Count - 3; i++)
            {
                int firstValue = GetValue(firstCells, i);
                if (firstValue == -1)
                {
                    continue;
                }

                int secondValue = GetValue(secondCells, i);
                if (secondValue == -1)
                {
                    continue;
                }


                if (firstValue > secondValue)
                {
                    firstCells[i].Background = Brushes.Lime;
                    secondCells[i].Background = Brushes.Red;
                }
                else if (firstValue < secondValue)
                {
                    firstCells[i].Background = Brushes.Red;
                    secondCells[i].Background = Brushes.Lime;
                }
            }
        }

        private static int GetValue (List<DataGridCell> cells, int i)
        {
            var text = GetVisualDescendants<TextBlock>(cells[i]).FirstOrDefault();
            if (text == null)
            {
                return -1;
            }

            int value;
            if (!int.TryParse(text.Text, out value))
            {
                return -1;
            }
            return value;
        }

        private static IEnumerable<T> GetVisualDescendants<T> (DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject == null)
            {
                yield break;
            }

            int childCount = VisualTreeHelper.GetChildrenCount(dependencyObject);

            for (int n = 0; n < childCount; n++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, n);

                if (child is T)
                {
                    yield return (T)child;
                }

                foreach (var match in GetVisualDescendants<T>(child))
                {
                    yield return match;
                }
            }
        }

        #endregion

    }
}

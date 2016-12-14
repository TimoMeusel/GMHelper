using System;
using System.Windows;
using System.Windows.Controls;

namespace GM.View
{
    /// <summary>
    /// Interaction logic for SkaterOverviewControl.xaml
    /// </summary>
    public partial class SkaterOverviewControl : UserControl
    {
        public SkaterOverviewControl()
        {
            InitializeComponent();
        }

        // This will be set to the target URL, when this window does not
        // host a created child view. The WebControl, is bound to this property.
        public Uri Source
        {
            get { return ( Uri ) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Identifies the <see cref="Source"/> dependency property.
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source",
            typeof(Uri), typeof(MainWindow),
            new FrameworkPropertyMetadata(null));
    }
}

using GM.Model;
using System.Windows;

namespace GM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var x = new DataGrabber("http://nfhl.eu.dd25712.kasserver.com/2/NFHL-ProTeamRoster.html");
            x.Grab();
        }
    }
}

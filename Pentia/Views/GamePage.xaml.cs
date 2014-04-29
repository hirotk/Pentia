using System.Windows.Controls;

namespace Pentia.Views {
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page {
        static MainWindow mainWnd;

        public GamePage(MainWindow wnd) {
            InitializeComponent();

            mainWnd = wnd;
            this.ShowsNavigationUI = true;
        }
    }
}

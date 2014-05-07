using System.Windows.Controls;
using Pentia.Controllers;

namespace Pentia.Views {
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page {
        public MainWindow MainWnd { get; private set; }
        private GameController game;

        public GamePage(MainWindow wnd) {
            InitializeComponent();

            this.MainWnd = wnd;
            this.ShowsNavigationUI = true;

            this.game = GameController.GetInstance();
            this.game.Initialize(this);
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            // Todo: Start Game
            game.Start();
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e) {
            // Todo: Terminate Game
            game.Terminate();
        }
    }
}

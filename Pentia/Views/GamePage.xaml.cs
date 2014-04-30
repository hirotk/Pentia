using System.Windows.Controls;
using Pentia.Controllers;

namespace Pentia.Views {
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page {
        static MainWindow mainWnd;
        private GameController game = GameController.GetInstance();

        public GamePage(MainWindow wnd) {
            InitializeComponent();

            mainWnd = wnd;
            this.ShowsNavigationUI = true;

            // Todo: Initialize Game
            game.Initialize(this);
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            // Todo: Start Game
            game.Start();
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e) {
            // Todo: Stop Game
            game.Stop();
        }
    }
}

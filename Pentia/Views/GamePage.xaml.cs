using System.Windows.Controls;
using Pentia.Controllers;

namespace Pentia.Views {
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page {
        public MainWindow MainWnd { get; private set; }
        public GameController GameCtrl { get; private set; }

        public GamePage(MainWindow wnd) {
            InitializeComponent();

            this.MainWnd = wnd;
            this.ShowsNavigationUI = true;
            this.GameCtrl = GameController.GetInstance();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            // Todo: Start Game
            this.GameCtrl.Initialize(this);
            GameCtrl.Start();
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e) {
            // Todo: Terminate Game
            GameCtrl.Terminate();
        }
    }
}

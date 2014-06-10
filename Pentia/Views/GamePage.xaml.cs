using System.Windows;
using System.Windows.Controls;
using Pentia.Controllers;
using System.Text.RegularExpressions;

namespace Pentia.Views {
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page {
        public MainWindow MainWnd { get; private set; }
        public bool IsBgmOn { get { return MainWnd.IsBgmOn; } }
        public bool IsSoundOn { get { return MainWnd.IsSoundOn; } }
        public GameController GameCtrl { get; private set; }

        public GamePage(MainWindow wnd) {
            InitializeComponent();

            this.MainWnd = wnd;
            this.ShowsNavigationUI = true;
            this.GameCtrl = GameController.GetInstance();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            this.GameCtrl.Initialize(this);
            GameCtrl.Start();
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e) {
            GameCtrl.Terminate();
        }

        private void NameInputButton_Click(object sender, System.Windows.RoutedEventArgs e) {
            var name = tbxPlayerName.Text;
            if (Regex.IsMatch(name, @"^[a-zA-Z\s\d_-]{1,12}$") == false) {
                tbxPlayerName.Text = "";
                tbMsg.Text = "Use letters or numbers.";
                return;
            }

            GameCtrl.Recorder.SaveRecord(name);
            tbxPlayerName.Text = "";
            spNameInput.Visibility = Visibility.Hidden;
        }
    }
}

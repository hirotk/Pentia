using System.Windows;
using System.Windows.Controls;

namespace Pentia.Views {
    /// <summary>
    /// Interaction logic for TitlePage.xaml
    /// </summary>
    public partial class TitlePage : Page {
        private MainWindow mainWnd;

        public TitlePage(MainWindow wnd) {
            InitializeComponent();
            this.mainWnd = wnd;
            this.ShowsNavigationUI = false;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new GamePage(this.mainWnd));
        }

        private void HiScoreButton_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new HighScorePage());
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new OptionPage(this.mainWnd));
        }
        
        private void QuitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}

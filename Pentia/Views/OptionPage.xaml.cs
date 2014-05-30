using System.Windows.Controls;

namespace Pentia.Views {
    /// <summary>
    /// Interaction logic for OptionPage.xaml
    /// </summary>
    public partial class OptionPage : Page {
        private MainWindow mainWnd;

        public OptionPage(MainWindow wnd) {
            InitializeComponent();

            mainWnd = wnd;
            this.ShowsNavigationUI = true;
            rdbBgmOn.IsChecked = mainWnd.IsBgmOn;
            rdbBgmOff.IsChecked = !mainWnd.IsBgmOn;
            rdbSndOn.IsChecked = mainWnd.IsSoundOn;
            rdbSndOff.IsChecked = !mainWnd.IsSoundOn;
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e) {
            mainWnd.IsBgmOn = (bool)rdbBgmOn.IsChecked;
            mainWnd.IsSoundOn = (bool)rdbSndOn.IsChecked;
        }
    }
}

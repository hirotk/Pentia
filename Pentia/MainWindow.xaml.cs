using System.Windows;
using System.Windows.Navigation;
using Pentia.Views;

namespace Pentia {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow {
        public bool IsBgmOn { get; set; }
        public bool IsSoundOn { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            this.Navigating += onNavigating;

            IsBgmOn = true;
            IsSoundOn = true;
            this.Navigate(new TitlePage(this));
        }

        private void onNavigating(object sender, NavigatingCancelEventArgs e) {
            if (e.NavigationMode == NavigationMode.Refresh) { // Disable F5 to refresh
                e.Cancel = true;
            }
        }
    }
}

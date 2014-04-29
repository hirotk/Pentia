using System.Windows;
using System.Windows.Navigation;
using Pentia.Views;

namespace Pentia {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow {
        TitlePage ttlPage;
        GamePage gmPage;
        public GamePage GmPage { get { return gmPage; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            this.ttlPage = new TitlePage(this);
            this.gmPage = new GamePage(this);
            this.Navigate(ttlPage);
        }
    }
}

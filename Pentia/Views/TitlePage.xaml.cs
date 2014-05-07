using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void QuitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}

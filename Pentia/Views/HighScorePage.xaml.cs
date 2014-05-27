using System.Windows;
using System.Windows.Controls;
using Pentia.Models;

namespace Pentia.Views {
    /// <summary>
    /// Interaction logic for HighScorePage.xaml
    /// </summary>
    public partial class HighScorePage : Page {
        private TextBlock[] tbScores;
        private TextBlock[] tbNames;

        public HighScorePage() {
            InitializeComponent();
            this.ShowsNavigationUI = true;

            int leng = Recorder.RECS_LENG;
            tbScores = new TextBlock[leng];
            tbNames = new TextBlock[leng];

            for (int i = 0; i < leng; i++) {
                tbScores[i] = new TextBlock();
                tbNames[i] = new TextBlock();

                Grid.SetRow(tbScores[i], i + 1);
                Grid.SetRow(tbNames[i], i + 1);

                Grid.SetColumn(tbScores[i], 1);
                Grid.SetColumn(tbNames[i], 2);

                this.gdHiScore.Children.Add(tbScores[i]);
                this.gdHiScore.Children.Add(tbNames[i]);
            }
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            var records = new Recorder().Records;

            for (int i = 0; i < records.Length; i++) {
                tbScores[i].Text = records[i].Score.ToString();
                tbNames[i].Text = records[i].Name;
            }
        }
    }
}

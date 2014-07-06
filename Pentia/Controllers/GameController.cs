using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Pentia.Views;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Input;
using Pentia.Models;
using Pentia.Utilities;

namespace Pentia.Controllers {
    public interface IGameController {
        void Initialize(GamePage page);
        void Terminate();
        bool Start();
        bool Stop();
        void OnTick(object sender, EventArgs e);
    }

    public interface IUpdatable {
        void Update();
        void Reset();
    }

    public class GameController : IGameController, IUpdatable, INotifyPropertyChanged {
        private static GameController instance = new GameController();
        public static GameController GetInstance() { return GameController.instance; }
        private GameController() {;}

        private GamePage page;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DispatcherTimer timer;

        private Board board;
        public Board Board { get { return board; } }
        private Recorder recorder;
        public Recorder Recorder { get {return recorder;} }

        private Binding initBinding(Binding binding, object souce, string property,
            string format = null, BindingMode bindingMode = BindingMode.TwoWay) {
            binding.Source = souce;
            binding.Path = new PropertyPath(property);
            if (format != null) { binding.StringFormat = format; }
            binding.Mode = bindingMode;
            return binding;
        }

        private void setResources() {
            if (ImageLoader.IsRegistered("Cells") == false) {
                ImageLoader.AddImage("Cells", new Uri(Properties.Resources.Cells, UriKind.Relative));
            }

            if (AudioPlayer.IsRegistered("Pause") == false) {
                AudioPlayer.AddSource("Pause", new Uri(Properties.Resources.Pause, UriKind.Relative));
            }
            if (AudioPlayer.IsRegistered("Restart") == false) {
                AudioPlayer.AddSource("Restart", new Uri(Properties.Resources.Restart, UriKind.Relative));
            }
            if (AudioPlayer.IsRegistered("GameOver") == false) {
                AudioPlayer.AddSource("GameOver", new Uri(Properties.Resources.GameOver, UriKind.Relative));
            }
            if (AudioPlayer.IsRegistered("DeleteRows") == false) {
                AudioPlayer.AddSource("DeleteRows", new Uri(Properties.Resources.DeleteRows, UriKind.Relative));
            }
            if (AudioPlayer.IsRegistered("BGM") == false) {
                AudioPlayer.AddSource("BGM", new Uri(Properties.Resources.BGM, UriKind.Relative));
            }
        }
        
        private void releaseResources() {
            ImageLoader.RemoveImage("Cells");

            AudioPlayer.RemoveSource("Pause");
            AudioPlayer.RemoveSource("Restart");
            AudioPlayer.RemoveSource("GameOver");
            AudioPlayer.RemoveSource("DeleteRows");
            AudioPlayer.RemoveSource("BGM");
        }

        private void setBinding() { 
            var binding = initBinding(new Binding(), recorder, "Score", "{0, 8}", BindingMode.OneWay);
            page.tbScore.SetBinding(TextBlock.TextProperty, binding);

            binding = initBinding(new Binding(), recorder, "Level", "{0, 8}", BindingMode.OneWay);
            page.tbLevel.SetBinding(TextBlock.TextProperty, binding);

            binding = initBinding(new Binding(), recorder, "Lines", "{0, 8}", BindingMode.OneWay);
            page.tbLines.SetBinding(TextBlock.TextProperty, binding);
        }

        public void Initialize(GamePage page) {
            this.page = page;

            setResources();

            this.timer = new DispatcherTimer();
            timer.Tick += this.OnTick;
            timer.Interval = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 1, milliseconds: 0);

            this.page.MainWnd.KeyDown += this.OnKeyDown;

            // Generate model objects
            var field = new Field(this.page.cvField, cols:12, rows:24,
                yOffset: Piece.PC_SIZE / 2, wallThickness: Piece.PC_SIZE / 2, dispWallThickness: 1);
            var nextField = new Field(this.page.cvNextField, cols: Piece.PC_SIZE, rows: Piece.PC_SIZE);
            board = new Board(field, nextField);

            recorder = new Recorder();

            setBinding();

            this.Reset();
        }

        public void Terminate() {
            this.Stop();
            releaseResources(); 
            this.page.MainWnd.KeyDown -= this.OnKeyDown;
            this.board = null;
            this.recorder = null;
        }

        public bool Start() {
            if (timer.IsEnabled == false) {
                timer.Start();
                if (page.IsBgmOn) { AudioPlayer.Play("BGM"); }
                return true;
            }
            return false;
        }

        public bool Stop() {
            if (timer.IsEnabled) {
                timer.Stop();
                if (page.IsBgmOn) { AudioPlayer.Stop("BGM"); }
                return true;
            }
            return false;
        }

        public void OnTick(object sender, EventArgs e) {
            this.Update();
        }


        public void OnKeyDown(object sender, KeyEventArgs e) {
            if (board.IsGameOver == false && (e.Key == Key.P || e.Key == Key.Q)) {
                if (this.Stop()) {
                    if (page.IsSoundOn) { AudioPlayer.Play("Pause", 1); }
                } else {
                    if (page.IsSoundOn) { AudioPlayer.Play("Restart", 1); }
                    this.Start();
                }
            }

            if (timer.IsEnabled == false) { return; }
            
            switch (e.Key) {
                case Key.J: case Key.S: board.MovePiece(Direction.Left); break;
                case Key.L: case Key.F: board.MovePiece(Direction.Right); break;
                case Key.N: case Key.V: board.MovePiece(Direction.Down); break;
                case Key.K: case Key.D: board.RotatePiece(RtDirection.Clockwise); break;
                case Key.I: case Key.E: board.RotatePiece(RtDirection.CtrClockwise); break;
            }
        }

        public void Update() {
            board.Update();

            if (0 < board.DeletedRowNum) {
                recorder.Update(board.DeletedRowNum);
                this.timer.Interval = recorder.TickInterval;

                if (page.IsSoundOn) { AudioPlayer.Play("DeleteRows", board.DeletedRowNum); }
            }

            if (board.IsGameOver) {
                if (page.IsSoundOn) { AudioPlayer.Play("GameOver", 1); }
                this.Stop();
                showGameOver();
                if (recorder.IsNewRecord) { showNameInput(); }
            }
        }

        public void Reset() {
            this.Stop();           
            board.Reset();
            recorder.Reset();
            hideNameInput();
            hideGameOver();
        }

        private void showNameInput() {
            page.spNameInput.Visibility = Visibility.Visible;
        }

        private void hideNameInput() {
            page.spNameInput.Visibility = Visibility.Hidden;
        }

        private void showGameOver() {
            page.tbGameOver.Visibility = Visibility.Visible;
        }

        private void hideGameOver() {
            page.tbGameOver.Visibility = Visibility.Hidden;
        }
    }
}

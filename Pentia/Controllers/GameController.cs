using System;
using System.Collections.Generic;
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

        private string status;
        public string Status {
            get { return status; }
            set {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DispatcherTimer timer;

        private Board board;

        public void Initialize(GamePage page) {
            this.page = page;
            this.timer = new DispatcherTimer();
            timer.Tick += this.OnTick;
            timer.Interval = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 1, milliseconds: 0);

            this.page.MainWnd.KeyDown += this.OnKeyDown;

            var binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath("Status");

            page.tbMonitor.SetBinding(TextBlock.TextProperty, binding);

            // Generate Model Objects
            var field = new Field(this.page.cvField, 10, 20);
            board = new Board(field);

            this.Reset();
        }

        public void Terminate() {
            //Todo: Release resources
            this.Stop();
            this.Status += "Terminate the game\n";
            this.page.MainWnd.KeyDown -= this.OnKeyDown;
        }

        public bool Start() {
            this.Status += "Start the game\n";
            if (timer.IsEnabled == false) {
                timer.Start();
            }
            return true;
        }

        public bool Stop() {
            this.Status += "Stop the game\n";
            if (timer.IsEnabled) {
                timer.Stop();
            }
            return true;
        }

        public void OnTick(object sender, EventArgs e) {
            this.Update();
        }

        public void OnKeyDown(object sender, KeyEventArgs e) {
            switch (e.Key) {
//                case Key.I: this.Status += "Up\n"; break;
                case Key.J: board.MovePiece(Direction.Left); break;
                case Key.L: board.MovePiece(Direction.Right); break;
                case Key.N: board.MovePiece(Direction.Down); break;
                case Key.K: board.RotatePiece(RtDirection.Clockwise); break;
                case Key.I: board.RotatePiece(RtDirection.CtrClockwise); break;
                case Key.P: 
                    this.Status += "Pause/Start\n";
                    if (this.timer.IsEnabled) {
                        this.Stop();
                    } else {
                        this.Start();
                    }
                    break;
            }
        }

        public void Update() {
//            this.Status += "Update the game\n";
            board.Update();
            if (board.Status == "Game over") {
                this.Terminate();
            }
            this.Status += board.Status;
            board.Status = "";
        }

        public void Reset() {
            this.Stop();
            this.Status = "Reset the game\n";            
            board.Reset();
            this.Status += board.Status;
            board.Status = "";
        }
    }
}

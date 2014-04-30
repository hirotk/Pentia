using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Pentia.Views;
using System.ComponentModel;

namespace Pentia.Controllers {
    public interface IGameController {
        void Initialize(GamePage page);
        bool Start();
        bool Stop();
    }

    public class GameController : IGameController, INotifyPropertyChanged {
        private static GameController instance = new GameController();
        public static GameController GetInstance() { return GameController.instance; }
        private GameController() {;}

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

        public void Initialize(GamePage page) {
            var binding = new Binding();

            binding.Source = this;
            binding.Path = new PropertyPath("Status");

            page.tbMonitor.SetBinding(TextBlock.TextProperty, binding);
            this.Status = "Initialize a game.\n";
        }

        public bool Start() {
            this.Status += "Start the game.\n";
            return true;
        }

        public bool Stop() {
            this.Status += "Stop the game.\n";
            return true;
        }
    }
}

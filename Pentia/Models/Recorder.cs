using System;
using Pentia.Controllers;
using System.ComponentModel;

namespace Pentia.Models {
    public class Recorder : IUpdatable, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private int score;
        public int Score {
            get { return score; }
            set {
                score = value;
                OnPropertyChanged("Score");
            }
        }

        private int level;
        public int Level {
            get { return level; }
            set {
                level = value;
                OnPropertyChanged("Level");
            }
        }

        private int lines;
        public int Lines {
            get { return lines; }
            set {
                lines = value;
                OnPropertyChanged("Lines");
            }
        }

        public TimeSpan TickInterval { get; private set; }
        private readonly int INIT_INTERVAL = 600; // [msec]

        public Recorder() {
            Reset();
        }

        public void Update() {
            Update(0);
        }

        public void Update(int deletedRowNum) {
            switch (deletedRowNum) {
                case 5:
                    Lines += 5;
                    Score += 1200;
                    break;
                case 4:
                    Lines += 4;
                    Score += 800;
                    break;
                case 3:
                    Lines += 3;
                    Score += 500;
                    break;
                case 2:
                    Lines += 2;
                    Score += 300;
                    break;
                case 1:
                    Lines += 1;
                    Score += 100;
                    break;
            }

            if (0 < deletedRowNum) {
                Level = Lines  / 10 + 1;
                TickInterval = new TimeSpan(0, 0, 0, 0, INIT_INTERVAL / 4 + INIT_INTERVAL * 3 / (4 * Level));
            }
        }

        public void Reset() {
            Score = 0;
            Level = 1;
            Lines = 0;
        }
    }
}

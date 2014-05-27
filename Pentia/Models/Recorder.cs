using System;
using Pentia.Controllers;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            Records = new Record[RECS_LENG];
            for (int i = 0; i < Records.Length; i++) {
                Records[i] = new Record(score: 0, name: "Player" + i);
            }
            loadRecord();
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

        public Record[] Records { get; private set; }
        public static readonly int RECS_LENG = 10;
        private string SCORE_DAT = "HighScores.dat";

        private void loadRecord() {
            if (File.Exists(SCORE_DAT) == false) { return; }

            using (var stream = File.Open(SCORE_DAT, FileMode.OpenOrCreate)) {
                var serializer = new BinaryFormatter();
                Records = (Record[])serializer.Deserialize(stream);
            }
        }

        public void SaveRecord(string name) {
            for (int i = 0; i < Records.Length; i++) {
                if (Records[i].Score < Score) {
                    for (int j = Records.Length - 2; j >= i; j--) {
                        Records[j + 1].Score = Records[j].Score;
                        Records[j + 1].Name = Records[j].Name;
                    }
                    Records[i].Score = Score;
                    Records[i].Name = name;
                    break;
                }
            }

            using (var stream = File.OpenWrite(SCORE_DAT)) {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, Records);
            }
        }

        public bool IsNewRecord {
            get { return Records[RECS_LENG - 1].Score < Score; }
        }
    }

    [Serializable]
    public class Record {
        public int Score { get; set; }
        public string Name { get; set; }

        public Record(int score, string name) {
            this.Score = score;
            this.Name = name;
        }
    }
}

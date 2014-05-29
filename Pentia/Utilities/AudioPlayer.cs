using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace Pentia.Utilities {
    class MediaPlayerEx : MediaPlayer {
        public int PlayNum { get; set; }
        public int PlayCount { get; set; }
    }

    public static class AudioPlayer {
        private static Dictionary<String, MediaPlayerEx> players = new Dictionary<string, MediaPlayerEx>();

        public static bool IsRegistered(string key) {
            return players.ContainsKey(key);
        }

        public static void AddSource(string key, Uri src) {
            if (players.ContainsKey(key)) {
                throw new ArgumentException(key + " is already registerd.");
            }

            var play = new MediaPlayerEx();
            play.MediaEnded += onEnd;

            if (File.Exists(src.OriginalString)) {
                play.Open(src);
            } else {
                throw new ArgumentException(src.OriginalString + " does not exist.");
            }
             
            players.Add(key, play);
        }

        public static void RemoveSource(string key) {
            if (players.ContainsKey(key)) {
                Stop(key);
                players[key].MediaEnded -= onEnd;
                Close(key);
                players.Remove(key);
            }
        }

        public static void Play(string key, int playNum = -1) {
            if (players.ContainsKey(key) == false) {
                throw new ArgumentException(key + " is not registerd.");
            }
            players[key].PlayNum = playNum;
            players[key].PlayCount = 1;
            players[key].Position = TimeSpan.Zero;
            players[key].Play();
        }

        private static void onEnd(object sender, EventArgs e) {
            var mp = sender as MediaPlayerEx;
            if (0 < mp.PlayNum && mp.PlayNum <= mp.PlayCount) {
                mp.Pause();
                return;
            }
            mp.Position = TimeSpan.Zero;
            if (mp.PlayNum < 0) { return; }
            mp.PlayCount++;
        }

        public static void Stop(string key) {
            if (players.ContainsKey(key)) {
                players[key].Stop();
            }
        }

        public static void Close(string key) {
            if (players.ContainsKey(key)) {
                players[key].Close();
            }
        }
    }
}

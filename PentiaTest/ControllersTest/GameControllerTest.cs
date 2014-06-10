using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Moq;
using Pentia;
using Pentia.Views;
using Pentia.Controllers;
using Pentia.Utilities;
using Pentia.Models;

namespace PentiaTest.ControllersTest {
    [TestClass]
    public class GameControllerTest {
        private static GameController target;
        private static GamePage page;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
            page = new GamePage(new MainWindow());
            target = page.GameCtrl;            
        }

        [ClassCleanup]
        public static void FinalizeTarget() {
            ImageLoader.RemoveImage("Cells");

            AudioPlayer.RemoveSource("Pause");
            AudioPlayer.RemoveSource("Restart");
            AudioPlayer.RemoveSource("GameOver");
            AudioPlayer.RemoveSource("DeleteRows");
            AudioPlayer.RemoveSource("BGM");
        }

        [TestInitialize]
        public void BeginTestMethod() {
            if (ImageLoader.IsRegistered("Cells") == false) {
                ImageLoader.AddImage("Cells", new Uri(@"..\..\..\Pentia\Resources\Cells.png", UriKind.Relative));
            }

            if (AudioPlayer.IsRegistered("Pause") == false) {
                AudioPlayer.AddSource("Pause", new Uri(@"..\..\..\Pentia\Resources\Pause.wav", UriKind.Relative));
            }
            if (AudioPlayer.IsRegistered("Restart") == false) {
                AudioPlayer.AddSource("Restart", new Uri(@"..\..\..\Pentia\Resources\Restart.wav", UriKind.Relative));
            }
            if (AudioPlayer.IsRegistered("GameOver") == false) {
                AudioPlayer.AddSource("GameOver", new Uri(@"..\..\..\Pentia\Resources\GameOver.wav", UriKind.Relative));
            }
            if (AudioPlayer.IsRegistered("DeleteRows") == false) {
                AudioPlayer.AddSource("DeleteRows", new Uri(@"..\..\..\Pentia\Resources\DeleteRows.wav", UriKind.Relative));
            }
            if (AudioPlayer.IsRegistered("BGM") == false) {
                AudioPlayer.AddSource("BGM", new Uri(@"..\..\..\Pentia\Resources\BGM.mp3", UriKind.Relative));
            }
            target.Initialize(page);
        }

        [TestMethod]
        public void InitializeTest() {
            target.Initialize(page);
            Assert.IsNotNull(target.Board);
            Assert.IsNotNull(target.Recorder);
        }

        [TestMethod]
        public void TerminateTest() {
            target.Start();
            target.Update();
            target.Terminate();
            Assert.IsNull(target.Board);
            Assert.IsNull(target.Recorder);
        }

        [TestMethod]
        public void StartTest() {
            bool expected = true;
            bool actual = target.Start();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StopTest() {
            bool expected = true;
            target.Start();
            bool actual = target.Stop();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OnKeyDownTest() {
            target.Start();
            int expected = target.Board.Piece.X + 1;
            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.L));

            int actual = target.Board.Piece.X;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResetTest() {
            var expected = PcColor.None;
            target.Board.Field[0, 0] = PcColor.Red;
            target.Board.NextField[1, 1] = PcColor.Blue;
            target.Reset();
            PcColor actual = target.Board.Field[0,0];
            Assert.AreEqual(expected, actual);
            actual = target.Board.NextField[1, 1];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest() {
            target.Start();
            int expected = target.Board.Piece.Y + 1;
            target.Update();
            int actual = target.Board.Piece.Y;
            Assert.AreEqual(expected, actual);
        }
    }
}

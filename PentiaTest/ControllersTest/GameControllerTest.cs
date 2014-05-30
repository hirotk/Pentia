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
            var sb = new StringBuilder();
            sb.Append("Reset the game\n");
            sb.Append("Reset the board\n");
            sb.Append("Reset the field\n");
            string expected = sb.ToString();
            target.Initialize(page);
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TerminateTest() {
            var sb = new StringBuilder();
            sb.Append("Reset the game\n");
            sb.Append("Reset the board\n");
            sb.Append("Reset the field\n");
            sb.Append("Start the game\n");
//            sb.Append("Update the game\n");
//            sb.Append("Update the board\n");
//            sb.Append("Update the field\n");
            sb.Append("Stop the game\n");
            sb.Append("Terminate the game\n");            
            string expected = sb.ToString();

            target.Initialize(page);
            target.Start();
            target.Update();
            target.Terminate();
            string actual = target.Status;
            Assert.AreEqual(expected.ToString(), actual);
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
            var sb = new StringBuilder();
            sb.Append("Reset the game\n");
            sb.Append("Reset the board\n");
            sb.Append("Reset the field\n"); 
            
//            sb.Append("Update the game\n");
//            sb.Append("Move the piece to the left\n");
//            sb.Append("Update the board\n");
//            sb.Append("Update the field\n");

            sb.Append("Pause/Start\n");
            sb.Append("Start the game\n");
            string expected = sb.ToString();

            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.J));
/*            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.L));
            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.N));
            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.K));*/

            target.Update();

            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.P));

            string actual = target.Status;
            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void ResetTest() {
            var sb = new StringBuilder();
            sb.Append("Reset the game\n");
            sb.Append("Reset the board\n");
            sb.Append("Reset the field\n"); 
            string expected = sb.ToString();
            target.Reset();
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest() {
            var sb = new StringBuilder();
            sb.Append("Reset the game\n");
            sb.Append("Reset the board\n");
            sb.Append("Reset the field\n");
            sb.Append("Start the game\n");
//            sb.Append("Update the game\n");
//            sb.Append("Update the board\n");
//            sb.Append("Update the field\n");
            string expected = sb.ToString();
            target.Initialize(page);
            target.Start();
            target.Update();
            string actual = target.Status;
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}

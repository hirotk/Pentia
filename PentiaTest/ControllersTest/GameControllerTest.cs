using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Controllers;
using Pentia.Views;
using Pentia;
using System.Text;
using System.Windows.Input;
using Moq;
using System.Windows;

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

        [TestInitialize()]
        public void BeginTestMethod() {
            target.Initialize(page);
        }

        [TestMethod]
        public void InitializeTest() {
            string expected = "Initialize a game.\n";
            target.Initialize(page);
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TerminateTest() {
            var expected = new StringBuilder();
            expected.Append("Initialize a game.\n");
            expected.Append("Start the game.\n");
            expected.Append("Update the game.\n");
            expected.Append("Stop the game.\n");
            expected.Append("Terminate the game.\n");
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
            bool actual = target.Stop();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OnKeyDownTest() {
            var expected = new StringBuilder();
            expected.Append("Initialize a game.\n");
            expected.Append("Left\n");
            expected.Append("Right\n");
            expected.Append("Down\n");
            expected.Append("Rotate\n");
            expected.Append("Pause/Start\n");

            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.J));
            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.L));
            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.N));
            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.K));
            target.OnKeyDown(page.MainWnd, new KeyEventArgs(
                Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, Key.P));

            string actual = target.Status;
            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void ResetTest() {
            string expected = "Reset a game.\n";
            target.Reset();
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest() {
            var expected = new StringBuilder();
            expected.Append("Initialize a game.\n");
            expected.Append("Start the game.\n");
            expected.Append("Update the game.\n");
            target.Initialize(page);
            target.Start();
            target.Update();
            string actual = target.Status;
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Controllers;
using Pentia.Views;
using Pentia;
using System.Text;
using System.Windows.Input;
using Moq;
using System.Windows;
using System.Windows.Controls;

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
            sb.Append("Update the game\n");
            sb.Append("Update the board\n");
            sb.Append("Update the field\n");
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
            bool actual = target.Stop();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OnKeyDownTest() {
            var sb = new StringBuilder();
            sb.Append("Reset the game\n");
            sb.Append("Reset the board\n");
            sb.Append("Reset the field\n"); 
            sb.Append("Left\n");
            sb.Append("Right\n");
            sb.Append("Down\n");
            sb.Append("Rotate\n");
            sb.Append("Pause/Start\n");
            sb.Append("Start the game\n");
            string expected = sb.ToString();

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
            sb.Append("Update the game\n");
            sb.Append("Update the board\n");
            sb.Append("Update the field\n");
            string expected = sb.ToString();
            target.Initialize(page);
            target.Start();
            target.Update();
            string actual = target.Status;
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}

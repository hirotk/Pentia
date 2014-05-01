using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Controllers;
using Pentia.Views;
using Pentia;
using System.Text;

namespace PentiaTest.ControllersTest {
    [TestClass]
    public class GameControllerTest {
        private static GameController target;
        private static MainWindow wnd;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
            target = GameController.GetInstance();
            wnd = new MainWindow();
        }

        [TestInitialize()]
        public void BeginTestMethod() {
        }

        [TestMethod]
        public void InitializeTest() {
            string expected = "Initialize a game.\n";
            target.Initialize(wnd.GmPage);
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
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
            target.Initialize(wnd.GmPage);
            target.Start();
            target.Update();
            string actual = target.Status;
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Controllers;
using Pentia.Views;
using Pentia;

namespace PentiaTest.ControllersTest {
    [TestClass]
    public class GameControllerTest {
        private static GameController target;
        private static GamePage page;
        private static MainWindow wnd;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
            target = GameController.GetInstance();
            wnd = new MainWindow();
            page = new GamePage(wnd);
        }

        [TestInitialize()]
        public void BeginTestMethod() {
        }

        [TestMethod]
        public void InitializeTest() {
            string expected = "Initialize a game.\n";
            target.Initialize(page);
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
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Models;

namespace PentiaTest.ModelsTest {
    [TestClass]
    public class BoardTest {
        private static Board target;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
        }

        [TestInitialize()]
        public void BeginTestMethod() {
            target = new Board();
        }

        [TestMethod]
        public void UpdateTest() {
            string expected = "Update";
            target.Update();
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResetTest() {
            string expected = "Reset";
            target.Reset();
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Models;

namespace PentiaTest.ModelsTest {
    [TestClass]
    public class RecorderTest {
        Recorder target;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
        }

        [TestInitialize]
        public void BeginTestMethod() {
            target = new Recorder();
        }

        [TestMethod]
        public void UpdateTest() {
            int expected = 1200;
            target.Update(5);
            int actual = target.Score;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResetTest() {
            int expected = 0;
            target.Update(5);
            target.Reset();
            int actual = target.Score;
            Assert.AreEqual(expected, actual);
        }
    }
}

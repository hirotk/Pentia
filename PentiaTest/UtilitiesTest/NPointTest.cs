using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Utilities;

namespace PentiaTest.UtilitiesTest {
    [TestClass]
    public class NPointTest {
        NPoint target;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {}

        [TestInitialize]
        public void BeginTestMethod() {
            target = new NPoint(2, 1);
        }        

        [TestMethod]
        public void MoveTest() {
            var expected = target - new NPoint(1, 0);
            Mover.Move(ref target, Direction.Left);
            Assert.AreEqual(expected, target);
        }

        [TestMethod]
        public void RotateTest() {
            var expected = new NPoint(-1 * target.y, 1 * target.x);
            Rotator.Rotate(ref target, RtDirection.Clockwise);
            Assert.AreEqual(expected, target);
        }
    }
}

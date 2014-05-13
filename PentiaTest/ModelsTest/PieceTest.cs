using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Models;

namespace PentiaTest.ModelsTest {
    [TestClass]
    public class PieceTest {
        Piece target;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
        }

        [TestInitialize]
        public void BeginTestMethod() {
            target = new Piece();
        }

        [TestMethod]
        public void MoveTest() {
            bool expected = true;
            bool actual =target.Move(Direction.Left);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RotateTest() {
            bool expected = true;
            bool actual = target.Rotate(RtDirection.Clockwise);
            Assert.AreEqual(expected, actual);
        }

    }
}

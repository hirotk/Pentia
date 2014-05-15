using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Models;
using System.Windows.Controls;

namespace PentiaTest.ModelsTest {
    [TestClass]
    public class PieceTest {
        static Field field;
        Piece target;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
            var cvs = new Canvas();
            cvs.Width = 200;
            cvs.Height = 400;
            field = new Field(cvs, 10, 20);
        }

        [TestInitialize]
        public void BeginTestMethod() {
            target = new Piece(field, field.COLS / 2, 1, PcColor.Blue, PcType.J);
        }

        [TestMethod]
        public void MoveLeftTest() {
            int expected = target.X - 1;
            target.Move(Direction.Left);
            int actual = target.X;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void MoveLeftMaxTest() {
            int expected = 2;
            for (int i = 0; i < field.COLS; i++) {
                target.Move(Direction.Left);
            }
            int actual = target.X;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MoveRightTest() {
            int expected = target.X + 1;
            target.Move(Direction.Right);
            int actual = target.X;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void MoveRightMaxTest() {
            int expected = field.COLS - 2;
            for (int i = 0; i < field.COLS; i++) {
                target.Move(Direction.Right);
            }
            int actual = target.X;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MoveDownTest() {
            int expected = target.Y + 1;
            target.Move(Direction.Down);
            int actual = target.Y;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void MoveDownMaxTest() {
            int expected = field.ROWS - 1;
            for (int j = 0; j < field.ROWS; j++) {
                target.Move(Direction.Down);
            }
            int actual = target.Y;
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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Models;
using System.Windows.Controls;

namespace PentiaTest.ModelsTest {
    [TestClass]
    public class BoardTest {
        private static Board target;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
        }

        [TestInitialize]
        public void BeginTestMethod() {
            var cvs = new Canvas();
            cvs.Width = 200;
            cvs.Height = 400;
            var field = new Field(cvs, 10, 20, Piece.PC_SIZE / 2, Piece.PC_SIZE / 2, 1);
            target = new Board(field);
        }

        [TestMethod]
        public void UpdateTest() {
            string expected = null;//"Update the board\n";
//            expected += "Update the field\n";
            target.Update();
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResetTest() {
            string expected = "Reset the board\n";
            expected += "Reset the field\n";
            target.Reset();
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

    }
}

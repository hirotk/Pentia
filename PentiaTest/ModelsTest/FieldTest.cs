using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Models;
using System.Windows.Controls;

namespace PentiaTest.ModelsTest {
    [TestClass]
    public class FieldTest {
        private static Field target;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
        }

        [TestInitialize]
        public void BeginTestMethod() {
            var cvs = new Canvas();
            cvs.Width = 200;
            cvs.Height = 400;
            target = new Field(cvs, 10, 20, Piece.PC_SIZE / 2, Piece.PC_SIZE / 2, 1);
        }

        [TestMethod]
        public void UpdateTest() {
            string expected = "Update the field\n";
            target.Update();
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResetTest() {
            var expected = PcColor.None;
            target[0, 0] = PcColor.Red;
            target.Reset();
            var actual = target[0,0];
            Assert.AreEqual(expected, actual);
        }

    }
}

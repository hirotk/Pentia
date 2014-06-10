using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using Pentia.Models;
using Pentia.Utilities;

namespace PentiaTest.ModelsTest {
    [TestClass]
    public class BoardTest {
        private static Board target;

        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
            if (ImageLoader.IsRegistered("Cells") == false) {
                ImageLoader.AddImage("Cells", new Uri(@"..\..\..\Pentia\Resources\Cells.png", UriKind.Relative));
            }        
        }

        [ClassCleanup]
        public static void FinalizeTarget() {
            ImageLoader.RemoveImage("Cells");
        }

        [TestInitialize]
        public void BeginTestMethod() {
            const int CELL_SIZE = 20;
            const int COLS = 12;
            const int ROWS = 24;
            var cvs = new Canvas();
            cvs.Width = Piece.PC_SIZE * CELL_SIZE;
            cvs.Height = Piece.PC_SIZE * CELL_SIZE;
            var nextField = new Field(cvs, Piece.PC_SIZE, Piece.PC_SIZE);

            cvs = new Canvas();
            cvs.Width = COLS * CELL_SIZE;
            cvs.Height = ROWS * CELL_SIZE;
            var field = new Field(cvs, COLS, ROWS, Piece.PC_SIZE / 2, Piece.PC_SIZE / 2, 1);
            target = new Board(field, nextField);
        }

        [TestMethod]
        public void UpdateTest() {
            int expected = 1;
            target.Update();
            int actual = target.Piece.Y;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ResetTest() {
            PcColor expected = PcColor.None;

            target.Field[0, 0] = PcColor.Red;
            target.NextField[1, 1] = PcColor.Blue;
            target.Reset();

            PcColor actual = target.Field[0, 0];
            Assert.AreEqual(expected, actual);
            actual = target.NextField[1, 1];
            Assert.AreEqual(expected, actual);
        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using System.Reflection;
using Pentia.Models;
using Pentia.Utilities;

namespace PentiaTest.ModelsTest {
    [TestClass]
    public class FieldTest {
        public object InvokePrivateMethod(string methodName, object[] argv) {
            object result = target.GetType().InvokeMember(methodName,
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, target, argv);
            return result;
        }

        private static Field target;

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


        [TestMethod]
        public void DelteRowsTest() {
            int expected = 3;

            // Set the field
            int bottom = target.ROWS - 1;
            for (int j = bottom; bottom - 5 < j; j--) {
                for (int i = 0; i < target.COLS; i++) {
                    target[i, j] = PcColor.Red;
                }
            }
            target[0, bottom] = PcColor.None;
            target[target.COLS - 1, bottom - 4] = PcColor.None;

            int actual = target.DeleteRows();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void checkRowsTest() {
            int expected = 5;

            // Set the field
            int bottom = target.ROWS - 1;
            for (int j = bottom; bottom - 5 < j; j--) {
                for (int i = 0; i < target.COLS; i++) {
                    target[i, j] = PcColor.Red;
                }
            }

            InvokePrivateMethod("checkRows", null);

            int actual = target.DelibleRowNum;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void deleteRowTest() {
            int expected = 4;

            // Set the field
            int bottom = target.ROWS - 1;
            for (int j = bottom; bottom - 5 < j; j--) {
                for (int i = 0; i < target.COLS; i++) {
                    target[i, j] = PcColor.Red;
                }
            }
            InvokePrivateMethod("checkRows", null);

            object[] argv = { bottom };
            InvokePrivateMethod("deleteRow", argv);
            
            int actual = target.DelibleRowNum;
            Assert.AreEqual(expected, actual);        
        }
    }
}

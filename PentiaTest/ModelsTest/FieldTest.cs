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

        [TestInitialize()]
        public void BeginTestMethod() {
            target = new Field(new Canvas(), 10, 20);
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
            string expected = "Reset the field\n";
            target.Reset();
            string actual = target.Status;
            Assert.AreEqual(expected, actual);
        }

    }
}

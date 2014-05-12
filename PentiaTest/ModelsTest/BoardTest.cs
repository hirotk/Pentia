﻿using System;
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

        [TestInitialize()]
        public void BeginTestMethod() {
            var field = new Field(new Canvas(), 10, 20);
            target = new Board(field);
        }

        [TestMethod]
        public void UpdateTest() {
            string expected = "Update the board\n";
            expected += "Update the field\n";
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

using System;
using System.Windows.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Utilities;

namespace PentiaTest.UtilitiesTest {
    [TestClass]
    public class ImageLoaderTest {
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

        [TestMethod]
        public void LoadImageTest() {
            int expected = 20 * 24;
            var rect = new Rectangle();
            ImageLoader.LoadImage("Cells", rect);
            int actual = (int)rect.Width;
            Assert.AreEqual(expected, actual);
        }
    }
}

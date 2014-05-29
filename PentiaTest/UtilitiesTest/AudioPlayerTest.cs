using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pentia.Utilities;

namespace PentiaTest.UtilitiesTest {
    [TestClass]
    public class AudioPlayerTest {
        [ClassInitialize]
        public static void InitializeTarget(TestContext testContext) {
            if (AudioPlayer.IsRegistered("Pause") == false) {
                AudioPlayer.AddSource("Pause", new Uri(@"..\..\..\Pentia\Resources\Pause.wav", UriKind.Relative));
            }
        }

        [ClassCleanup]
        public static void FinalizeTarget() {
            AudioPlayer.RemoveSource("Pause");
        }

        [TestMethod]
        public void PlayTest() {
            AudioPlayer.Play("Pause", 1);
        }
    }
}

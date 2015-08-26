using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakesTestClassLibrary;
using Microsoft.QualityTools.Testing.Fakes;
using FakeItEasy;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestStub_CurrentSWVersion()
        {
            // Arrange
            int expected = 42;

            // Act
            //var upgradeService = new UpgradeService();
            IUpgradeService upgradeService = new FakesTestClassLibrary.Fakes.StubIUpgradeService() 
            {
                CurrentSWVersionInt32 = (DeviceID) => { return 42; }

            };
           
            // Assert
            Assert.AreEqual(expected, upgradeService.CurrentSWVersion(1));
        }

        [TestMethod]
        public void TestShim_LastUpgradeDate()
        {
            // Arrange
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return new DateTime(1966, 3, 31); };

                var upgradeService = new UpgradeService();

                // Act
                DateTime lastUpgradeDate = upgradeService.LastUpgradeDate(24);

                // Assert
                Assert.AreEqual(new DateTime(1966,3,31), lastUpgradeDate);
            }
        }

        [TestMethod]
        public void TestFake_CurrentSWVersion()
        {
            // Arrange
            int expected = 42;
            var upgradeService = A.Fake<IUpgradeService>();

            // Act
            A.CallTo(() => upgradeService.CurrentSWVersion(32)).Returns(666);
            A.CallTo(() => upgradeService.CurrentSWVersion(33)).Returns(42);
            A.CallTo(() => upgradeService.CurrentSWVersion(A<int>._)).Returns(42); // Same as ignored

            // Assert
            Assert.AreEqual(expected, upgradeService.CurrentSWVersion(2));

        }
    }
}

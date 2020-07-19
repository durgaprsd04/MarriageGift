using System;
using MarriageGift.Model.OccasionModel;
using MarriageGift.Model.Interfaces;
using NUnit.Framework;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.OccasionalModel
{
    [TestFixture]
    class HouseWarmingTest
    {
        private Mock<ILog> mockLog;
        [SetUp]
        public void Setup()
        {
            mockLog = new Mock<ILog>();
        }
        [Test]
        public void modifyOccasion_PositiveTest1()
        {
            //Arrange
            var house = new HouseWarming("owner1");
            var house2 = new HouseWarming("owner2");
            //Act
            house.modifyOccasion(house2);
            //Assert
            Assert.AreEqual(house.Owner, "owner2");
        }
        [Test]
        public void modifyOccasion_NegativeTest1()
        {
            //Arrange
            var house = new HouseWarming("owner1");
            var expectedException = false;
            var errorType = string.Empty;
            //Act
            try
            {
                var house2 = new Mock<IOccassion>();
                house.modifyOccasion(house2.Object);
            }
            catch (ArgumentNullException)
            {
                expectedException = true;
            }
            catch (Exception e)
            {
                errorType = e.GetType().ToString();
            }
            //Assert
            Assert.IsTrue(expectedException, string.Format("Expected exception not found, got {0}", errorType));
        }
    }

}

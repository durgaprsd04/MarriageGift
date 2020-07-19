using System;
using MarriageGift.Model.OccasionModel;
using MarriageGift.Model.Interfaces;
using NUnit.Framework;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.OccasionalModel
{
    [TestFixture]
    class MarriageTest
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
            var marriage = new Marriage("bride1", "groom1");
            var marriage2 = new Marriage("bride2", "groom2");
            //Act
            marriage.modifyOccasion(marriage2);
            //Assert
            Assert.AreEqual(marriage.Bride, "bride2");
            Assert.AreEqual(marriage.Groom, "groom2");
        }
        [Test]
        public void modifyOccasion_NegativeTest1()
        {
            //Arrange
            var marriage = new Marriage("bride1", "groom1");
            var expectedException = false;
            var errorType = string.Empty;
            //Act
            try
            {
                var marriage2 = new Mock<IOccassion>();
                marriage.modifyOccasion(marriage2.Object);
            }
            catch(ArgumentNullException)
            {
                expectedException = true;
            }
            catch(Exception e)
            {
                errorType = e.GetType().ToString();
            }
            //Assert
            Assert.IsTrue(expectedException, string.Format("Expected exception not found, got {0}", errorType));
        }
    }
}

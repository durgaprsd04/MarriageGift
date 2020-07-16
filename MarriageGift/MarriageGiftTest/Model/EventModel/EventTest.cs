using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using log4net;
using MarriageGift.Enums;
using MarriageGift.Exceptions;
using MarriageGift.Model.EventModel;
using MarriageGift.Model.GiftModel;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;
namespace MarriageGiftTest.Model.EventModel
{
    [TestFixture]
    class EventTest
    {        
        Mock<ILog> mockLog;
        IMock<IOccassion> mockOccasion;
        string place;
        DateTime date;
        IGiftCollection<IGift> dummyExpectedGiftCollection;
        IGiftCollection<IGift> dummyRecievedGiftCollection;
        string dummyCustId = Guid.NewGuid().ToString();
        IMock<Customer> customer;
      [SetUp]
        public void Setup()
        {
            
            mockLog = new Mock<ILog>();
            mockOccasion = new Mock<IOccassion>();
            customer = new Mock<Customer>();
            //customer.Setup(x => x.CustId).Returns(dummyCustId);
            place = "testPlace";
            date = new DateTime(2020, 6, 30);
            dummyExpectedGiftCollection = GetDummyGiftCollection();
            dummyRecievedGiftCollection = GetDummyGiftCollection();
            
        }
        public Event GetEvent()
        {
            return new Event(mockOccasion.Object, place, date, dummyExpectedGiftCollection, dummyRecievedGiftCollection, dummyCustId, mockLog.Object);
        }
        public GiftCollection GetDummyGiftCollection()
        {
            var giftDict = new Dictionary<string, IGift>();
            var giftCollection = new GiftCollection(giftDict, mockLog.Object);
            return giftCollection;
        }
        public GiftCollection GetDummyGiftCollectionWithGiftId(string giftId)
        {
            var giftCollection = GetDummyGiftCollection();
            var gift = new Gift(giftId, "Pots", GiftItemType.Crockery, 2000, mockLog.Object);
            giftCollection.AddGift(gift);
            return giftCollection;
        }
        [Test]
        public void Cancel_PositiveTest1()
        {
            var dummyEvent = GetEvent();
            var result = dummyEvent.Cancel(true);
            Assert.IsTrue(result);
        }
        [Test]
        public void ModifyDate_PositiveTest1()
        {
            var dummyEvent = GetEvent();
            var testDate = new DateTime(2020, 05, 30);
            var result = dummyEvent.ModifyDate(testDate);
            Assert.AreEqual(dummyEvent.Date, testDate);
            Assert.IsTrue(result);
        }
        [Test]
        public void ModifyPlace_PositiveTest1()
        {
            var dummyEvent = GetEvent();
            var place = "new Place";
            var result = dummyEvent.ModifyPlace(place);
            Assert.IsTrue(result);
            Assert.AreEqual(place, dummyEvent.Place);
        }
        [Test]
        public void AddExpectedGift_NegativeTest1()
        {
            var dummyEvent = GetEvent();
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyGift = new Mock<IGift>();
                dummyEvent.AddExpectedGift(dummyGift.Object);
            }
            catch(ArgumentNullException)
            {
                result = true;
            }
            catch(Exception e)
            {
                errorType = e.GetType().ToString();
            }
            Assert.IsTrue(result, "Expected exception not found, got {0} instead", errorType);
        }
        [Test]
        public void AddExpectedGift_PositiveTest1()
        {
            var dummyEvent = GetEvent();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGift = new Mock<IGift>();
            dummyGift.Setup(x => x.GetGiftId()).Returns(dummyGiftId);
            var result = dummyEvent.AddExpectedGift(dummyGift.Object);
            Assert.AreEqual(dummyEvent.ExpectedGiftCollection().Count(), 1);
            Assert.IsTrue(result);
        }
        [Test]
        public void RemoveExpectedGift_NegativeTest1()
        {
            var dummyEvent = GetEvent();
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyGift = new Mock<IGift>();
                dummyEvent.RemoveExpectedGift(dummyGift.Object);
            }
            catch (ArgumentNullException)
            {
                result = true;
            }
            catch (Exception e)
            {
                errorType = e.GetType().ToString();
            }
            Assert.IsTrue(result, "Expected exception not found, got {0} instead", errorType);
        }
        [Test]
        public void RemoveExpectedGift_PositiveTest1()
        {
            var dummyEvent = GetEvent();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGift = new Mock<IGift>();
            dummyGift.Setup(x => x.GetGiftId()).Returns(dummyGiftId);
            var result1 = dummyEvent.AddExpectedGift(dummyGift.Object);
            var result2 = dummyEvent.RemoveExpectedGift(dummyGift.Object);
            Assert.AreEqual(dummyEvent.RecievedGiftCollection().Count(), 0);
            Assert.IsTrue(result1&result2);
        }
        [Test]
        public void AddRecievedGifts_PositiveTest1()
        {
            //Arrange
            var dummyEvent = GetEvent();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGift = new Mock<IGift>();
            dummyGift.Setup(x => x.GetGiftId()).Returns(dummyGiftId);
            var result1 = dummyEvent.AddExpectedGift(dummyGift.Object);
            //Act
            var result2 = dummyEvent.AddRecievedGifts(dummyGift.Object);
            var result3 = dummyEvent.ExpectedGiftCollection().Count();
            var result4 = dummyEvent.RecievedGiftCollection().Count();
            //Assert
            Assert.IsTrue(result1 && result2, "Failure while adding to ExpectedGiftCollection/RecievedGiftCollection");
            Assert.AreEqual(result3, 0);
            Assert.AreEqual(result4, 1); 
        }
        [Test]
        public void AddRecievedGifts_NegativeTest1()
        {
            //Arrange
            var dummyEvent = GetEvent();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyGift = new Mock<IGift>();
            dummyGift.Setup(x => x.GetGiftId()).Returns(dummyGiftId);
            var dummyGift2 = new Mock<IGift>();
            dummyGift2.Setup(x => x.GetGiftId()).Returns(dummyGiftId2);
            var result1 = dummyEvent.AddExpectedGift(dummyGift.Object);
            //Act
            var result2 = dummyEvent.AddRecievedGifts(dummyGift2.Object);
            var result3 = dummyEvent.ExpectedGiftCollection().Count();
            var result4 = dummyEvent.RecievedGiftCollection().Count();
            //Assert
            Assert.IsTrue(result1 && !result2, "Failure while adding to ExpectedGiftCollection/RecievedGiftCollection");
            Assert.AreEqual(result3, 1);
            Assert.AreEqual(result4, 0);
        }
        [Test]
        public void RemoveRecievedGifts_PositiveTest1()
        {
            //Arrange
            var dummyEvent = GetEvent();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGift = new Mock<IGift>();
            dummyGift.Setup(x => x.GetGiftId()).Returns(dummyGiftId);
            dummyEvent.AddExpectedGift(dummyGift.Object);
            dummyEvent.AddRecievedGifts(dummyGift.Object);
            //Act
            dummyEvent.RemoveRecievedGifts(dummyGift.Object);
            //Assert
            var result1 = dummyEvent.RecievedGiftCollection().Count();
            var result2 = dummyEvent.ExpectedGiftCollection().Count();
            Assert.AreEqual(result1, 0);
            Assert.AreEqual(result2, 1);
        }
        [Test]
        public void RemoveRecievedGifts_NegativeTest1()
        {
            //Arrange
            var dummyEvent = GetEvent();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyGift = new Mock<IGift>();
            dummyGift.Setup(x => x.GetGiftId()).Returns(dummyGiftId);
            var dummyGift2 = new Mock<IGift>();
            dummyGift2.Setup(x => x.GetGiftId()).Returns(dummyGiftId2);
            dummyEvent.AddExpectedGift(dummyGift.Object);
            dummyEvent.AddRecievedGifts(dummyGift.Object);
            dummyEvent.RemoveRecievedGifts(dummyGift2.Object);
            //Act
            var result1 = dummyEvent.ExpectedGiftCollection().Count();
            var result2 = dummyEvent.RecievedGiftCollection().Count();
            //Assert            
            Assert.AreEqual(result1, 0);
            Assert.AreEqual(result2, 1);
        }
    }
}

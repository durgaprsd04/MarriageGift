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
        Event dummyEvent;
        Mock<ILog> mockLog;
        [SetUp]
        public void Setup()
        {
            var dummyCustId = Guid.NewGuid().ToString();
            mockLog = new Mock<ILog>();
            var mockOccasion = new Mock<IOccassion>();
            var customer = new Mock<Customer>();
            //customer.Setup(x => x.CustId).Returns(dummyCustId);
            var place = "testPlace";
            var date = new DateTime(2020, 6, 30);
            var dummyExpectedGiftCollection = GetDummyGiftCollection();
            var dummyRecievedGiftCollection = GetDummyGiftCollection();
            dummyEvent = new Event(mockOccasion.Object, place, date, dummyExpectedGiftCollection, dummyRecievedGiftCollection, dummyCustId, mockLog.Object);
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
            var result = dummyEvent.Cancel(true);
            Assert.IsTrue(result);
        }
        [Test]
        public void ModifyDate_PositiveTest1()
        {
            var testDate = new DateTime(2020, 05, 30);
            var result = dummyEvent.ModifyDate(testDate);
            Assert.AreEqual(dummyEvent.Date, testDate);
            Assert.IsTrue(result);
        }
        [Test]
        public void ModifyPlace_PositiveTest1()
        {
            var place = "new Place";
            var result = dummyEvent.ModifyPlace(place);
            Assert.IsTrue(result);
            Assert.AreEqual(place, dummyEvent.Place);
        }
        [Test]
        public void AddExpectedGift_NegativeTest1()
        {
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyGift = new Mock<IGift>();
                dummyEvent.AddExpectedGift(dummyGift.Object);
            }
            catch(GiftCollectionAddException)
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
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGift = new Mock<IGift>();
            dummyGift.Setup(x => x.GetGiftId()).Returns(dummyGiftId);
            var result = dummyEvent.AddExpectedGift(dummyGift.Object);
            Assert.AreEqual(dummyEvent.ExpectedGiftCollection().Count(), 1);
            Assert.IsTrue(result);
        }
        [Test]
        public void AddRecievedGift_NegativeTest1()
        {
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyGift = new Mock<IGift>();
                dummyEvent.AddRecievedGifts(dummyGift.Object);
            }
            catch (GiftCollectionAddException)
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
        public void AddRecievedGift_PositiveTest1()
        {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGift = new Mock<IGift>();
            dummyGift.Setup(x => x.GetGiftId()).Returns(dummyGiftId);
            var result = dummyEvent.AddRecievedGifts(dummyGift.Object);
            Assert.AreEqual(dummyEvent.RecievedGiftCollection().Count(), 1);
            Assert.IsTrue(result);
        }
    }
}

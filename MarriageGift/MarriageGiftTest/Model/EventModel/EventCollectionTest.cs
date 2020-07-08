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
    class EventCollectionTest
    {
        Mock<ILog> mockLog;
        EventCollection eventCollection = new EventCollection();
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
        public void AddEvent_PositiveTest1()
        {
            eventCollection = new EventCollection();
            var event1 = GetEvent();
            var result = eventCollection.AddEvent(event1);
            Assert.IsTrue(result, "Failure in adding Event to collection");
        }
        [Test]
        public void AddEvent_NegativeTest1()
        {
            eventCollection = new EventCollection();
            var event1 = new Mock<IEvent>();
            var result = false;
            var exceptionType = string.Empty;
            try
            {
                eventCollection.AddEvent(event1.Object);
            }
            catch(ArgumentException)
            {
                result = true;
            }
            catch(Exception e)
            {
                exceptionType = e.GetType().ToString();
            }

            Assert.IsTrue(result, string.Format("Expected Exception ArgumentException not found got {0}", exceptionType));
        }        
        [Test]
        public void RemoveEvent_NegativeTest1()
        {
            eventCollection = new EventCollection();
            var event1 = new Mock<IEvent>();
            var result = false;
            var exceptionType = string.Empty;
            try
            {
                eventCollection.RemoveEvent(event1.Object);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception e)
            {
                exceptionType = e.GetType().ToString();
            }

            Assert.IsTrue(result, string.Format("Expected Exception ArgumentException not found got {0}", exceptionType));
        }
        [Test]
        public void RemoveEvent_NegativeTest2()
        {
            eventCollection = new EventCollection();
            var event1 = GetEvent();
            var result = false;
            var exceptionType = string.Empty;
            try
            {
                eventCollection.RemoveEvent(event1);
            }
            catch (EventCollectionRemoveException)
            {
                result = true;
            }
            catch (Exception e)
            {
                exceptionType = e.GetType().ToString();
            }

            Assert.IsTrue(result, string.Format("Expected Exception EventCollectionRemoveException not found got {0}", exceptionType));
        }
        [Test]
        public void AddEvent_NegativeTest2()
        {
            eventCollection = new EventCollection();
            var event1 = GetEvent();
            eventCollection.AddEvent(event1);
            var result = eventCollection.RemoveEvent(event1);
            Assert.IsTrue(result, "Event remove failed");
        }
        
    }
    }

﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using log4net;
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
            place = "testPlace";
            date = new DateTime(2020, 6, 30);
            dummyExpectedGiftCollection = GetDummyGiftCollection();
            dummyRecievedGiftCollection = GetDummyGiftCollection();

        }
        public Event GetEvent()
        {
            var eventId = Guid.NewGuid().ToString();
            return new Event(eventId, mockOccasion.Object, place, date, dummyExpectedGiftCollection, dummyRecievedGiftCollection, dummyCustId, false);
        }
        public GiftCollection GetDummyGiftCollection()
        {
            var giftDict = new Dictionary<string, IGift>();
            var giftCollection = new GiftCollection();
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
            result = eventCollection.RemoveEvent(event1);
            Assert.IsFalse(result, "Eventcollection should return false");
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
        [Test]
        public void GetEventsByCustId_PositiveTest1()
        {
            eventCollection = new EventCollection();
            var event1 = GetEvent();
            eventCollection.AddEvent(event1);
            var custList = eventCollection.GetEventsByCustId(dummyCustId);
            Assert.AreEqual(((EventCollection)custList).Count(),1);
        }
        [Test]
        public void GetEventsByCustId_NegativeTest1()
        {
            eventCollection = new EventCollection();
            var event1 = GetEvent();
            eventCollection.AddEvent(event1);
            var custList = eventCollection.GetEventsByCustId(Guid.NewGuid().ToString());
            Assert.AreEqual(((EventCollection)custList).Count(),0);
        }       
    }
    }

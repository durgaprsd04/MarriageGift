using NUnit.Framework;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.EventModel;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Model.Interfaces;
using Moq;
using log4net;
using System;
namespace MarriageGiftTest.Model.CustomerModel
{
    public class CustomerTest
    {
        private Customer customer;
        private IEventCollection eventCollection;
        private IEvent testEvent;
        Mock<IEvent> mockEvent = new Mock<IEvent>();
        Mock<ILog> mockLog = new Mock<ILog>();
        [SetUp]
        public void Setup()
        {
          
          string name="testname";         
          eventCollection = new EventCollection(mockLog.Object);
          mockEvent = new Mock<IEvent>();

          var mockInviteCollection = new InvitationCollection(mockLog.Object);
          var mockEventCollection = new EventCollection(mockLog.Object);
          customer = new Customer(name,mockInviteCollection, mockEventCollection, mockLog.Object ); 
          
        }
        public Event CreateEmptyEvent(string custId)
        {
          var mockOccasion = new Mock<IOccassion>();
          var place ="test place";
          var date = new DateTime(2020,6,3);         
          var giftCollectionExpected = new Mock<IGiftCollection<IGift>>();
          var giftCollectionRecieved = new Mock<IGiftCollection<IGift>>();
          return  new Event(mockOccasion.Object, place, date, giftCollectionExpected.Object, giftCollectionRecieved.Object, custId, mockLog.Object);

        }
        [Test]
        public void AddMyEvents_NegativeTest1()
        {
            var result = customer.AddMyEvents(mockEvent.Object);
            Assert.IsFalse(result);
        }
        [Test]
        public void AddMyEvents_PositiveTest1()
        {
          var dummyEvent = CreateEmptyEvent(customer.CustId);
          var result = customer.AddMyEvents(dummyEvent);
          Assert.IsTrue(result);
        }
    }
}
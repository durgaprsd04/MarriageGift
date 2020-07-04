using NUnit.Framework;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.EventModel;
using MarriageGift.Model.Interfaces;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.CustomerModel
{
    public class CustomerTest
    {
        private Customer customer;
        private IEventCollection eventCollection;
        private IEvent testEvent;
        Mock<IEvent> mockEvent = new Mock<IEvent>();
        [SetUp]
        public void Setup()
        {
          
           string name="testname";
            var mockLog = new Mock<ILog>();
            eventCollection = new EventCollection(mockLog.Object);
         //   testEvent = new Event();
            var mockInviteCollection = new Mock<IInvitationCollection>();
            var mockEventCollection = new Mock<IEventCollection>();
            customer = new Customer(name,mockInviteCollection.Object, mockEventCollection.Object, mockLog.Object ); 
            mockEventCollection.Setup(t => t.AddEvent(mockEvent.Object)).Returns(true);
        }

        [Test]
        public void AddMyEvents_PositiveTest1()
        {
            var result = customer.AddMyEvents(mockEvent.Object);
            Assert.IsTrue(result);
        }
    }
}
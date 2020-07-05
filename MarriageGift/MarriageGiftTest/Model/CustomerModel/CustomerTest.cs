using System;
using NUnit.Framework;
using Moq;
using log4net;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.EventModel;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Exceptions;
using MarriageGift.Model.Interfaces;

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
        public Event CreateEmptyEventWithGift(string custId, string giftId)
        {
            var dummyEvent = CreateEmptyEvent(custId);
            var mockGift = new Mock<IGift>();
            mockGift.Setup(x => x.GetGiftId()).Returns(giftId);
            dummyEvent.AddExpectedGift(mockGift.Object);
            return dummyEvent;
        }
        public Invitation CreateEmptyInvitation()
        {
            string sender = "dummySender";
            var mockCustCollection = new Mock<ICustomerCollection>();
            var mockEvent = new Mock<IEvent>();
            return  new Invitation(sender, mockEvent.Object, mockCustCollection.Object, mockLog.Object);
        }
        public Invitation CreateEmptyInvitationWithCustomEvent(IEvent dummyEvent)
        {
            string sender = "dummySender";
            var mockCustCollection = new Mock<ICustomerCollection>();
            return new Invitation(sender, dummyEvent, mockCustCollection.Object, mockLog.Object);
        }
        public Invitation CreateEmptyInvitationWithCustomEventCustomGiftId(string custId, string dummyGiftId)
        {
            var dummyEvent= CreateEmptyEventWithGift(custId, dummyGiftId);
            return CreateEmptyInvitationWithCustomEvent(dummyEvent);
        }

        [Test]
        public void AddMyEvents_NegativeTest1()
        {
            var result = customer.AddMyEvents(mockEvent.Object);
            Assert.IsFalse(result,"Expected false for dummy object type");
        }
        [Test]
        public void AddMyEvents_PositiveTest1()
        {
          var dummyEvent = CreateEmptyEvent(customer.CustId);
          var result = customer.AddMyEvents(dummyEvent);
          Assert.IsTrue(result,"Expected true for dummy object type");
        }
        [Test]
        public void AddMyInvitations_NegativeTest1()
        {
            var mockInvite = new Mock<IInvitation>();
            var result = customer.AddMyInvitations(mockInvite.Object);
            Assert.IsFalse(result,"Excpected false for dummy object type");
        }
        [Test]
        public void AddMyInvitations_PositiveTest1()
        {
            var dummyInvite = CreateEmptyInvitation();
            var result = customer.AddMyInvitations(dummyInvite);
            Assert.IsTrue(result, "Excpected true for dummy object type");
        }
        [Test]
        public void CancelEvent_PositiveTest1()
        {
            var dummyEvent = CreateEmptyEvent(customer.CustId);
            var result1= customer.AddMyEvents(dummyEvent);
            var result2 =customer.CancelEvent(dummyEvent.EventId);
            Assert.IsTrue(result1 && result2);
        }
        [Test]
        public void CancelEvent_NegativeTest1()
        {
            var result2 = false;
            var result1 = false;
            var errorType =string.Empty;
            try
            {
                var dummyEvent = CreateEmptyEvent(customer.CustId);
                result1 = customer.AddMyEvents(dummyEvent);
                customer.CancelEvent(Guid.NewGuid().ToString());
            }
            catch(EventNotFoundException)
            {
                result2 = true;
            }
            catch(Exception e)
            {
                errorType = e.GetType().ToString();
            }
            
            Assert.IsTrue(result1 && result2, string.Format("Expected exception EventNotFoundException not found got {0} instead", errorType));
        }
        [Test]
        public void ChangeEventTime_NegativeTest1()
        {
            var result1 = false;
            var result2 = false;
            var errorType = string.Empty;
            try
            {
                var dummyEvent = CreateEmptyEvent(customer.CustId);
                result1 = customer.AddMyEvents(dummyEvent);
                var date = new DateTime(2020, 6, 4);
                customer.ChangeEventTime(Guid.NewGuid().ToString(), date);
            }
            catch(EventNotFoundException)
            {
                result2 = true;
            }
            catch(Exception e)
            {
                errorType = e.GetType().ToString();
            }
            
            Assert.IsTrue(result1&&result2, string.Format("Expected exception EventNotFoundException not found got {0} instead", errorType));
        }
        [Test]
        public void ChangeEventTime_PositiveTest1()
        { 
                var dummyEvent = CreateEmptyEvent(customer.CustId);
                var result1 = customer.AddMyEvents(dummyEvent);
                var date = new DateTime(2020, 6, 4);
                var result2 = customer.ChangeEventTime(dummyEvent.EventId, date);
        }
        [Test]
        public void ChangeEventVenue_PositiveTest1()
        {

            var dummyEvent = CreateEmptyEvent(customer.CustId);
            var result1 = customer.AddMyEvents(dummyEvent);
            var newVenue = "dummyVenue";
            var result2 = customer.ChangeEventVenue(dummyEvent.EventId, newVenue);
            Assert.IsTrue(dummyEvent.Place==newVenue);
        }
        [Test]
        public void ChangeEventVenue_NegativeTest1()
        {
            var result1 = true;
            var result2 = true;
            var errorType = string.Empty;
            try
            {
                var dummyEvent = CreateEmptyEvent(customer.CustId);
                result1 = customer.AddMyEvents(dummyEvent);
                var newVenue = "dummyVenue";
                customer.ChangeEventVenue(Guid.NewGuid().ToString(), newVenue);
            }
            catch (EventNotFoundException)
            {
                result2 = true;
            }
            catch (Exception e)
            {
                errorType = e.GetType().ToString();
            }
            Assert.IsTrue(result1 && result2, string.Format("Expected exception EventNotFoundException not found got {0} instead", errorType));
        }
        [Test]
        public void RespondToInvitation_PositiveTest1()
        {
            var dummyInvite = CreateEmptyInvitation();
            var result1 = customer.AddMyInvitations(dummyInvite);
            var result2 = customer.RespondToInvitation(dummyInvite.InvitationId, false);
            Assert.IsFalse(dummyInvite.IsAccepted);
        }
        [Test]
        public void RespondToInvitation_NegativeTest1()
        {
            var result1 = false;
            var result2 = false;
            var errorType = string.Empty;
            try
            {
                var dummyInvite = CreateEmptyInvitation();
                result1 = customer.AddMyInvitations(dummyInvite);
                customer.RespondToInvitation(Guid.NewGuid().ToString(), false);
            }
            catch(InvitationNotFoundException)
            {
                result2 = true;
            }
            catch(Exception e)
            {
                errorType = e.GetType().ToString();
            }
            Assert.IsTrue(result2&&result1, string.Format("Expected exception EventNotFoundException not found got {0} instead", errorType));
        }
        [Test]
        public void BuyGiftForInvitation_PositiveTest1()
        {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyEvent = CreateEmptyEventWithGift(customer.CustId, dummyGiftId);           
            var dummyInvitation = CreateEmptyInvitationWithCustomEvent(dummyEvent);
            customer.AddMyInvitations(dummyInvitation);
            var result = customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            Assert.IsTrue(dummyEvent.ExpectedGiftCollection().Count() == 0,"Expected gift collection count should become zero");
            Assert.IsTrue(dummyEvent.RecievedGiftCollection().Count() == 1, "Recieved gift collection count should be 1");
            Assert.IsTrue(result, "Function BuyGiftForInvitation should return true");
        }
        [Test]
        public void BuyGiftForInvitation_ExpectsGiftNotFoundException_NegativeTest1()
        {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.CustId, dummyGiftId);
                customer.AddMyInvitations(dummyInvitation);
                customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId2);
            }
            catch(GiftNotFoundException)
            {
                result = true;
            }
            catch(Exception e)
            {
                errorType = e.GetType().ToString();
            }
            Assert.IsTrue(result, string.Format("Expected exception GiftNotFoundException not found got {0} instead", errorType));
        }
        [Test]
        public void BuyGiftForInvitation_ExpectsInvitationNotFoundException_NegativeTest1()
        {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyInviteId2 = Guid.NewGuid().ToString();
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.CustId, dummyGiftId);
                customer.AddMyInvitations(dummyInvitation);
                customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyInviteId2);
            }
            catch (InvitationNotFoundException)
            {
                result = true;
            }
            catch (Exception e)
            {
                errorType = e.GetType().ToString();
            }
            Assert.IsTrue(result, string.Format("Expected exception InvitationNotFoundException not found got {0} instead", errorType));
        }
        [Test]
        public void RemoveGiftForInvitation_PositiveTest1()
        {
            //Arrange
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyEvent = CreateEmptyEventWithGift(customer.CustId, dummyGiftId);
            var dummyInvitation = CreateEmptyInvitationWithCustomEvent(dummyEvent);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            //Act
            var result = customer.RemoveGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            //Assert
            Assert.IsTrue(dummyEvent.ExpectedGiftCollection().Count() == 1, "Expected gift collection count should become zero");
            Assert.IsTrue(dummyEvent.RecievedGiftCollection().Count() == 0, "Recieved gift collection count should be 1");
            Assert.IsTrue(result, "Function BuyGiftForInvitation should return true");
        }
        [Test]
        public void RemoveGiftForInvitation_ExcpectsGiftNotFoundExceptionNegativeTest1()
        {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.CustId, dummyGiftId);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            customer.RemoveGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            
        }
    }
}
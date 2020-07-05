using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using log4net;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.EventModel;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Model.GiftModel;
using MarriageGift.Enums;
using MarriageGift.Exceptions;
using MarriageGift.Model.Interfaces;

namespace MarriageGiftTest.Model.CustomerModel
{
    public class CustomerTest
    {
        private Customer customer;
        private IEventCollection eventCollection;
        Mock<IEvent> mockEvent = new Mock<IEvent>();
        Mock<ILog> mockLog = new Mock<ILog>();
        [SetUp]
        public void Setup()
        {           
          var name ="testCust";  
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
        public GiftCollection GetDummyGiftCollection()
        {
            var giftDict = new Dictionary<string, IGift>();
            var giftCollection = new GiftCollection(giftDict, mockLog.Object);
            return giftCollection;
        }
        public GiftCollection GetDummyGiftCollectionWithGiftId(string giftId)
        {
            var giftCollection = GetDummyGiftCollection();
            var gift = new Gift(giftId, "Pots",GiftItemType.Crockery, 2000,mockLog.Object);
            giftCollection.AddGift(gift);
            return giftCollection;
            
        }
        public Event CreateEventWithGift(string custId, string giftId)
        {
          var mockOccasion = new Mock<IOccassion>();
          var place ="test place";
          var date = new DateTime(2020,6,3);
          var giftCollectionExpected = GetDummyGiftCollectionWithGiftId(giftId);
          var giftCollectionRecieved = GetDummyGiftCollection();
          return  new Event(mockOccasion.Object, place, date, giftCollectionExpected, giftCollectionRecieved, custId, mockLog.Object);
        }

        public Event CreateEmptyEventWithGift(string custId, string giftId)
        {
            var dummyEvent = CreateEventWithGift(custId, giftId);
            return dummyEvent;
        }
        public Event CreateEmptyEventWithTwoGifts(string custId, string giftId1, string giftid2)
        {
            var dummyEvent = CreateEmptyEvent(custId);
            var mockGift1 = new Mock<IGift>();
            mockGift1.Setup(x => x.GetGiftId()).Returns(giftId1);
            var mockGift2 = new Mock<IGift>();
            mockGift2.Setup(x => x.GetGiftId()).Returns(giftid2);
            dummyEvent.AddExpectedGift(mockGift1.Object);
            dummyEvent.AddExpectedGift(mockGift2.Object);
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
        public Invitation CreateEmptyInvitationWithCustomEventCustomGiftId(string custId, string dummyGiftId1, string dummyGiftId2)
        {
            var dummyEvent= CreateEmptyEventWithTwoGifts(custId, dummyGiftId1,dummyGiftId2);
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
          //Arrange
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyEvent = CreateEmptyEventWithGift(customer.CustId, dummyGiftId);           
            var dummyInvitation = CreateEmptyInvitationWithCustomEvent(dummyEvent);
            customer.AddMyInvitations(dummyInvitation);
            //Act
            var result = customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            //Assert
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
        public void BuyGiftForInvitation_ExpectsInvitationNotFoundException_NegativeTest2()
        {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyInviteId2 = Guid.NewGuid().ToString();
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.CustId, dummyGiftId);
                customer.AddMyInvitations(dummyInvitation);
                customer.BuyGiftForInvitation(dummyInviteId2, dummyGiftId);
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
        public void RemoveGiftForInvitation_ExcpectsGiftNotFoundException_NegativeTest1()
        {
          var result =false;
          var errorType=string.Empty;
          try
          {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.CustId, dummyGiftId);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            customer.RemoveGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId2);
          }
          catch(GiftNotFoundException)
          {
              result =true;
          }
          catch(Exception e)
          {
            errorType=e.GetType().ToString();
          }
          Assert.IsTrue(result,string.Format("Expected exception GiftNotFoundException not got but {0} instead ", errorType));
        }

        [Test]
        public void RemoveGiftForInvitation_ExcpectsInvitationNotFoundException_NegativeTest2()
        {
          var result =false;
          var errorType=string.Empty;
          try
          {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.CustId, dummyGiftId);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            customer.RemoveGiftForInvitation(dummyGiftId2, dummyGiftId);
          }
          catch(InvitationNotFoundException)
          {
              result =true;
          }
          catch(Exception e)
          {
            errorType=e.GetType().ToString();
          }
          Assert.IsTrue(result,string.Format("Expected exception InvitationNotFoundException not got but {0} instead ", errorType));
        }
        [Test]
        public void ModifyGiftForInvitation_PositiveTest1()
        {
            //Arrange
            var dummyGiftId1 = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyEvent = CreateEmptyEventWithTwoGifts(customer.CustId, dummyGiftId1, dummyGiftId2);
            var dummyInvitation = CreateEmptyInvitationWithCustomEvent(dummyEvent);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId1);
            //Act
            var result = customer.ModifyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId1,dummyGiftId2);
            //Assert
            Assert.IsTrue(dummyEvent.ExpectedGiftCollection().Count() == 1, "Expected gift collection count should become zero");
            Assert.IsTrue(dummyEvent.RecievedGiftCollection().Count() == 1, "Recieved gift collection count should be 1");
            Assert.IsTrue(result, "Function BuyGiftForInvitation should return true");
        }
        [Test]
        public void ModifyGiftForInvitation_ExcpectsInvitationNotFoundException_NegativeTest1()
        {
          var result =false;
          var errorType=string.Empty;
          try
          {
            var dummyGiftId1= Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.CustId, dummyGiftId1,dummyGiftId2);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId1);
            customer.ModifyGiftForInvitation(dummyInvitation.InvitationId,dummyGiftId1, dummyGiftId1);
          }
          catch(GiftNotFoundException)
          {
              result =true;
          }
          catch(Exception e)
          {
            errorType=e.GetType().ToString();
          }
          Assert.IsTrue(result,string.Format("Expected exception GiftNotFoundException not got but {0} instead ", errorType));
        }
        [Test]
        public void ModifyGiftForInvitation_ExcpectsInvitationNotFoundException_NegativeTest2()
        {
          var result =false;
          var errorType=string.Empty;
          try
          {
            var dummyGiftId1= Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.CustId, dummyGiftId1,dummyGiftId2);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId1);
            customer.ModifyGiftForInvitation(dummyGiftId2,dummyGiftId1, dummyGiftId1);
          }
          catch(InvitationNotFoundException)
          {
              result =true;
          }
          catch(Exception e)
          {
            errorType=e.GetType().ToString();
          }
          Assert.IsTrue(result,string.Format("Expected exception InvitationNotFoundException not got but {0} instead ", errorType));
        }
    }
}
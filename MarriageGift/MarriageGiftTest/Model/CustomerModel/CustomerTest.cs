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
using MarriageGift.Exceptions.CustomerExceptions;
using MarriageGift.Exceptions.GiftExceptions;
using MarriageGift.Exceptions.InvitationExceptions;
using MarriageGift.Model.Interfaces;

namespace MarriageGiftTest.Model.CustomerModel
{
    public class CustomerTest
    {
        private Customer customer;
        private IEventCollection eventCollection= new EventCollection();       
        string name = "testCust";    
        string password ="passWord";   
        IMock<IEvent> mockEvent = new Mock<IEvent>();                  
        EventCollection mockEventCollection = new EventCollection();
        Mock<ILog> mockLog;
        InvitationCollection mockInviteCollection;
        [SetUp]
        public void Setup()
        {
            mockLog = new Mock<ILog>();
            mockInviteCollection = new InvitationCollection();
        }
        public Customer GetCustomer()
        {
            return new Customer(name,password);
        }
        public Customer GetCustomer(InvitationCollection invitationCollection)
        {
            var custId = Guid.NewGuid().ToString();
            return new Customer(custId, name, password,invitationCollection, mockEventCollection);
        }
        public Event CreateEmptyEvent(string custId)
        {
          var mockOccasion = new Mock<IOccassion>();
          var place ="test place";
          var date = new DateTime(2020,6,3);            
          var giftCollectionExpected = new GiftCollection();
          var giftCollectionRecieved = new GiftCollection();
          return  new Event(mockOccasion.Object, place, date,custId);
        }
        public GiftCollection GetDummyGiftCollection()
        {
            var giftDict = new Dictionary<string, IGift>();
            var giftCollection = new GiftCollection();
            return giftCollection;
        }
        public GiftCollection GetDummyGiftCollectionWithGiftId(string giftId)
        {
            var giftCollection = GetDummyGiftCollection();
            var gift = new Gift(giftId, "Pots",GiftItemType.Crockery, 2000);
            giftCollection.AddGift(gift);
            return giftCollection;
            
        }
        public Event CreateEventWithGift(string custId, string giftId)
        {
          var mockOccasion = new Mock<IOccassion>();
          var eventId =Guid.NewGuid().ToString();
          var place ="test place";
          var date = new DateTime(2020,6,3);
          var giftCollectionExpected = GetDummyGiftCollectionWithGiftId(giftId);
          var giftCollectionRecieved = GetDummyGiftCollection();
          return  new Event(eventId ,mockOccasion.Object, place, date, giftCollectionExpected, giftCollectionRecieved, custId,false);
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
            var dummyCustomer = new Customer("testName", "password");
            var mockEvent = new Mock<IEvent>();
            return  new Invitation(dummyCustomer, mockEvent.Object);
        }
        public Invitation CreateEmptyInvitationWithCustomEvent(IEvent dummyEvent)
        {
            var dummyCustomer = new Customer("testName", "password");
            return new Invitation(dummyCustomer, dummyEvent);
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
            var result = false;
            var errorType = string.Empty;
            try
            {
                customer.AddMyEvents(mockEvent.Object);
            }
            catch(ArgumentNullException)
            {
                result = true;
            }
            catch(Exception e)
            {
                errorType = e.GetType().ToString();
            }
            
            Assert.IsFalse(result, string.Format("Expected exception ArgumentNullException not found got  {0} instead", errorType));
        }
        [Test]
        public void AddMyEvents_PositiveTest1()
        {
          customer = GetCustomer();
          var dummyEvent = CreateEmptyEvent(customer.getId());
          var result = customer.AddMyEvents(dummyEvent);
          Assert.IsTrue(result,"Expected true for dummy object type");
        }
        [Test]
        public void AddMyInvitations_NegativeTest1()
        {
            customer = GetCustomer();
            var mockInvite = new Mock<IInvitation>();
            var expectedException =false;
            var errorType=string.Empty;
            try
            {
                 customer.AddMyInvitations(mockInvite.Object);
            }
            catch(ArgumentNullException)
            {
                expectedException=true;
            }
            catch(Exception e)
            {
                errorType=e.GetType().ToString();
            }            
            Assert.IsTrue(expectedException,string.Format("Excpected exception not found, found {0} instead", errorType));
        }
        [Test]
        public void AddMyInvitations_PositiveTest1()
        {
            customer = GetCustomer();
            var dummyInvite = CreateEmptyInvitation();
            var result = customer.AddMyInvitations(dummyInvite);
            Assert.IsTrue(result, "Excpected true for dummy object type");
        }
        [Test]
        public void CancelEvent_PositiveTest1()
        {
            customer = GetCustomer();
            var dummyEvent = CreateEmptyEvent(customer.getId());
            var result1= customer.AddMyEvents(dummyEvent);
            var result2 =customer.CancelEvent(dummyEvent.getId());
            Assert.IsTrue(result1 && result2);
        }
        [Test]
        public void CancelEvent_NegativeTest1()
        {
            customer = GetCustomer();
            var result2 = false;
            var result1 = false;
            var errorType =string.Empty;
            try
            {
                var dummyEvent = CreateEmptyEvent(customer.getId());
                result1 = customer.AddMyEvents(dummyEvent);
                customer.CancelEvent(Guid.NewGuid().ToString());
            }
            catch(CustomerNotFoundException)
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
            customer = GetCustomer();
            var result1 = false;
            var result2 = false;
            var errorType = string.Empty;
            try
            {
                var dummyEvent = CreateEmptyEvent(customer.getId());
                result1 = customer.AddMyEvents(dummyEvent);
                var date = new DateTime(2020, 6, 4);
                customer.ChangeEventTime(Guid.NewGuid().ToString(), date);
            }
            catch(CustomerNotFoundException)
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
            customer = GetCustomer();
            var dummyEvent = CreateEmptyEvent(customer.getId());
            var result1 = customer.AddMyEvents(dummyEvent);
            var date = new DateTime(2020, 6, 4);
            var result2 = customer.ChangeEventTime(dummyEvent.getId(), date);
        }
        [Test]
        public void ChangeEventVenue_PositiveTest1()
        {
            customer = GetCustomer();
            var dummyEvent = CreateEmptyEvent(customer.getId());
            var result1 = customer.AddMyEvents(dummyEvent);
            var newVenue = "dummyVenue";
            var result2 = customer.ChangeEventVenue(dummyEvent.getId(), newVenue);
            Assert.IsTrue(dummyEvent.Place==newVenue);
        }
        [Test]
        public void ChangeEventVenue_NegativeTest1()
        {
            customer = GetCustomer();
            var result1 = true;
            var result2 = true;
            var errorType = string.Empty;
            try
            {
                var dummyEvent = CreateEmptyEvent(customer.getId());
                result1 = customer.AddMyEvents(dummyEvent);
                var newVenue = "dummyVenue";
                customer.ChangeEventVenue(Guid.NewGuid().ToString(), newVenue);
            }
            catch (CustomerNotFoundException)
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
            customer = GetCustomer();
            var dummyInvite = CreateEmptyInvitation();
            var result1 = customer.AddMyInvitations(dummyInvite);
            customer.RespondToInvitation(dummyInvite.getId(), false);
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
            var customer = GetCustomer();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyEvent = CreateEmptyEventWithGift(customer.getId(), dummyGiftId);           
            var dummyInvitation = CreateEmptyInvitationWithCustomEvent(dummyEvent);
            customer.AddMyInvitations(dummyInvitation);
            //Act
            var result = customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            //Assert
            Assert.IsTrue(((GiftCollection)dummyEvent.ExpectedGiftCollection()).Count() == 0,"Expected gift collection count should become zero");
            Assert.IsTrue(((GiftCollection)dummyEvent.RecievedGiftCollection()).Count() == 1, "Recieved gift collection count should be 1");
            Assert.IsTrue(result, "Function BuyGiftForInvitation should return true");
        }
        [Test]
        public void BuyGiftForInvitation_ExpectsGiftNotFoundException_NegativeTest1()
        {
            customer = GetCustomer();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.getId(), dummyGiftId);
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
            customer = GetCustomer();
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyInviteId2 = Guid.NewGuid().ToString();
            var result = false;
            var errorType = string.Empty;
            try
            {
                var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.getId(), dummyGiftId);
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
            customer = GetCustomer();
            var dummyEvent = CreateEmptyEventWithGift(customer.getId(), dummyGiftId);           
            var dummyInvitation = CreateEmptyInvitationWithCustomEvent(dummyEvent);
            customer.AddMyInvitations(dummyInvitation);            
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            //Act
            var result = customer.RemoveGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId);
            //Assert
            Assert.IsTrue(((GiftCollection)dummyEvent.ExpectedGiftCollection()).Count() == 1, "Expected gift collection count should become zero");
            Assert.IsTrue(((GiftCollection)dummyEvent.RecievedGiftCollection()).Count() == 0, "Recieved gift collection count should be 1");
            Assert.IsTrue(result, "Function BuyGiftForInvitation should return true");
        }
        [Test]
        public void RemoveGiftForInvitation_ExcpectsGiftNotFoundException_NegativeTest1()
        {
          customer = GetCustomer();
          var result =false;
          var errorType=string.Empty;
          try
          {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.getId(), dummyGiftId);
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
          customer = GetCustomer();
          var result =false;
          var errorType=string.Empty;
          try
          {
            var dummyGiftId = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.getId(), dummyGiftId);
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
            var customer = GetCustomer();
            var dummyGiftId1 = Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyEvent = CreateEmptyEventWithTwoGifts(customer.getId(), dummyGiftId1, dummyGiftId2);
            var dummyInvitation = CreateEmptyInvitationWithCustomEvent(dummyEvent);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId1);
            //Act
            var result = customer.ModifyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId1,dummyGiftId2);
            //Assert
            Assert.IsTrue(((GiftCollection)dummyEvent.ExpectedGiftCollection()).Count() == 1, "Expected gift collection count should become zero");
            Assert.IsTrue(((GiftCollection)dummyEvent.RecievedGiftCollection()).Count() == 1, "Recieved gift collection count should be 1");
            Assert.IsTrue(result, "Function BuyGiftForInvitation should return true");
        }
        [Test]
        public void ModifyGiftForInvitation_ExcpectsInvitationNotFoundException_NegativeTest1()
        {
            customer = GetCustomer();
            var result =false;
            var errorType=string.Empty;
          
            var dummyGiftId1= Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.getId(), dummyGiftId1,dummyGiftId2);
            customer.AddMyInvitations(dummyInvitation);
            customer.BuyGiftForInvitation(dummyInvitation.InvitationId, dummyGiftId1);
            result =customer.ModifyGiftForInvitation(dummyInvitation.InvitationId,dummyGiftId1, dummyGiftId1);
            Assert.IsTrue(result,string.Format("Expected exception GiftNotFoundException not got but {0} instead ", errorType));
        }
        [Test]
        public void ModifyGiftForInvitation_ExcpectsInvitationNotFoundException_NegativeTest2()
        {
          var result =false;
          var errorType=string.Empty;
          try
          {
            var customer = GetCustomer();
            var dummyGiftId1= Guid.NewGuid().ToString();
            var dummyGiftId2 = Guid.NewGuid().ToString();
            var dummyInvitation = CreateEmptyInvitationWithCustomEventCustomGiftId(customer.getId(), dummyGiftId1,dummyGiftId2);
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
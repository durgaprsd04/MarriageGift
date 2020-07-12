using System;
using System.Collections.Generic;
using System.Text;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.EventModel;
using MarriageGift.Model.GiftModel;
using NUnit.Framework;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.InvitationModel
{
    [TestFixture]
    class InvitationTest
    {
        private Mock<ILog> logger;        
        string custId;
        CustomerCollection customerCollection;
        GiftCollection giftR;
        GiftCollection giftE;
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILog>();
            giftR = new GiftCollection(logger.Object);
            giftE= new GiftCollection(logger.Object);
            custId = Guid.NewGuid().ToString();            
            customerCollection = new CustomerCollection(logger.Object);

        }
        public Event GetEvent(string custId,GiftCollection giftCollectionR, GiftCollection  giftCollectionE)
        {           
            var OccasionMock = new Mock<IOccassion>();
            return new Event(OccasionMock.Object, "thisPlace", new DateTime(2020, 05, 05), giftCollectionE, giftCollectionR, custId, logger.Object);
        }
        public Invitation GetInvitation(GiftCollection giftCollectionR, GiftCollection giftCollectionE)
        {
            return new Invitation("sender", GetEvent(custId, giftCollectionR, giftCollectionE), customerCollection, logger.Object);
        }
        public Invitation GetInvitation(GiftCollection giftCollectionR, GiftCollection giftCollectionE, CustomerCollection customerCollection)
        {
            return new Invitation("sender", GetEvent(custId, giftCollectionR, giftCollectionE), customerCollection, logger.Object);
        }
        [Test]
        public void RespondToInvitation_PositiveTest()
        {           
            var invitation = GetInvitation(giftR, giftE);
            invitation.RespondToInvitation(false);
            Assert.IsFalse(invitation.IsAccepted);
        }
        [Test]
        public void GetExpectedGiftsForEvent_PositiveTest1()
        {
            var giftExGiftId = Guid.NewGuid().ToString(); 
            var giftE1 = new Mock<IGift>();
            giftE1.Setup(x => x.GetGiftId()).Returns(giftExGiftId);
            giftE.AddGift(giftE1.Object);
            var invitation = GetInvitation(giftR, giftE);
            var expectedGifts= invitation.GetExpectedGiftsForEvent();
            Assert.AreEqual(expectedGifts.Count(), 1);
            Assert.IsNotNull(expectedGifts.GetGiftById(giftExGiftId));
        }
        [Test]
        public void GetRecievedGiftsForEvent_PositiveTest1()
        {
            var giftExGiftId = Guid.NewGuid().ToString();
            var giftR1 = new Mock<IGift>();
            giftR1.Setup(x => x.GetGiftId()).Returns(giftExGiftId);
            giftR.AddGift(giftR1.Object);
            var invitation = GetInvitation(giftR, giftE);
            var expectedGifts = invitation.GetRecievedGiftsForEvent();
            Assert.AreEqual(expectedGifts.Count(), 1);
            Assert.IsNotNull(expectedGifts.GetGiftById(giftExGiftId));
        }
        [Test]
        public void GetRecievedGiftsForEvent_NegativeTest1()
        {
            var invitation = GetInvitation(giftR, giftE);
            var expectedGifts = invitation.GetRecievedGiftsForEvent();
            var giftExGiftId = Guid.NewGuid().ToString();
            //Assert
            Assert.AreEqual(expectedGifts.Count(), 0);
            Assert.IsNull(expectedGifts.GetGiftById(giftExGiftId));
        }
        [Test]
        public void GetExpectedGiftsForEvent_NegativeTest1()
        {
            var invitation = GetInvitation(giftR, giftE);
            var expectedGifts = invitation.GetExpectedGiftsForEvent();
            var giftExGiftId = Guid.NewGuid().ToString();
            //Assert
            Assert.AreEqual(expectedGifts.Count(), 0);
            Assert.IsNull(expectedGifts.GetGiftById(giftExGiftId));
        }
        public Customer GetCustomer()
        {
            var eventCollection = new Mock<IEventCollection>();
            var inviteCollection = new Mock<IInvitationCollection>();
            return new Customer("nameTest", inviteCollection.Object, eventCollection.Object, logger.Object);
        }
        [Test]
        public void AddCustomerToListofInvitees_PositiveTest1()
        {
            var custCollection = new CustomerCollection(logger.Object);
            var customer = GetCustomer();
            custCollection.AddCustomer(customer);
            var invitation = GetInvitation(giftR, giftE, custCollection);
            var list = invitation.GetListofInvitees();
            Assert.AreEqual(list.Count(), 1);            
        }
        [Test]
        public void AddCustomerToListofInvitees_NegativeTest1()
        {
            var custCollection = new CustomerCollection(logger.Object);
            var invitation = GetInvitation(giftR, giftE, custCollection);
            var list = invitation.GetListofInvitees();
            Assert.AreEqual(list.Count(), 0);
        }

    }
}

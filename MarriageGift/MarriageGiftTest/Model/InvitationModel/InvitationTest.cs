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
        ICustomer customer, customer1;
        GiftCollection giftR;
        GiftCollection giftE;
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILog>();
            giftR = new GiftCollection();
            giftE= new GiftCollection();
            custId = Guid.NewGuid().ToString();            
            customer = new Customer("userName","password");

            customer1 = new Customer("userName2","password");

        }
        public Event GetEvent(string custId,GiftCollection giftCollectionR, GiftCollection  giftCollectionE)
        {           
            string eventId = Guid.NewGuid().ToString();
            var OccasionMock = new Mock<IOccassion>();
            return new Event(eventId, OccasionMock.Object, "thisPlace", new DateTime(2020, 05, 05), giftCollectionE, giftCollectionR, custId,false);
        }
        public Invitation GetInvitation(GiftCollection giftCollectionR, GiftCollection giftCollectionE)
        {
            return new Invitation(customer, GetEvent(customer1.getId(), giftCollectionR,giftCollectionE));
        }
        public Invitation GetInvitation(GiftCollection giftCollectionR, GiftCollection giftCollectionE, CustomerCollection customerCollection)
        {
            return new Invitation(customer, GetEvent(customer1.getId(), giftCollectionR, giftCollectionE));
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
            Assert.AreEqual(((GiftCollection)expectedGifts).Count(), 1);
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
            Assert.AreEqual(((GiftCollection)expectedGifts).Count(), 1);
            Assert.IsNotNull(expectedGifts.GetGiftById(giftExGiftId));
        }
        [Test]
        public void GetRecievedGiftsForEvent_NegativeTest1()
        {
            var invitation = GetInvitation(giftR, giftE);
            var expectedGifts = invitation.GetRecievedGiftsForEvent();
            var giftExGiftId = Guid.NewGuid().ToString();
            //Assert
            Assert.AreEqual(((GiftCollection)expectedGifts).Count(), 0);
            Assert.IsNull(expectedGifts.GetGiftById(giftExGiftId));
        }
        [Test]
        public void GetExpectedGiftsForEvent_NegativeTest1()
        {
            var invitation = GetInvitation(giftR, giftE);
            var expectedGifts = invitation.GetExpectedGiftsForEvent();
            var giftExGiftId = Guid.NewGuid().ToString();
            //Assert
            Assert.AreEqual(((GiftCollection)expectedGifts).Count(), 0);
            Assert.IsNull(expectedGifts.GetGiftById(giftExGiftId));
        }
        public Customer GetCustomer()
        {
            var eventCollection = new Mock<IEventCollection>();
            var inviteCollection = new Mock<IInvitationCollection>();
            return new Customer("userName", "password");
        }
        [Test]
        public void AddCustomerToListofInvitees_PositiveTest1()
        {
            var custCollection = new CustomerCollection();
            var customer = GetCustomer();
            custCollection.AddCustomer(customer);
            var invitation = GetInvitation(giftR, giftE, custCollection);
            var list = invitation.GetListofInvitees();
            Assert.AreEqual(((CustomerCollection)list).Count(), 1);            
        }
        [Test]
        public void AddCustomerToListofInvitees_NegativeTest1()
        {
            var custCollection = new CustomerCollection();
            var invitation = GetInvitation(giftR, giftE, custCollection);
            var list = invitation.GetListofInvitees();
            Assert.AreEqual(((CustomerCollection)list).Count(), 0);
        }

    }
}

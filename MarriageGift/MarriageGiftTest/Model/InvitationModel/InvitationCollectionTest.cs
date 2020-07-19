using System;
using System.Collections.Generic;
using System.Text;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;
using NUnit.Framework;
using Moq;
using log4net;

namespace MarriageGiftTest.Model.InvitationModel
{
    [TestFixture]
    class InvitationCollectionTest
    {
        private InvitationCollection invitationCollection;
        private IMock<ILog> mockLog;
        
        private Mock<IEvent> dummyEvent;
        private Mock<IEventCollection> eventCollection;
        private ICustomer customer;
        private Invitation invitation;
        [SetUp]
        public void Setup()
        {
            mockLog = new Mock<ILog>();
            dummyEvent = new Mock<IEvent>();
            customer = new Customer("username","password");
            invitation = new Invitation(customer, dummyEvent.Object);
            eventCollection = new Mock<IEventCollection>();            
        }
        public InvitationCollection GetEmptyInviteCollection()
        {
            return new InvitationCollection();
        }
        [Test]
        public void AddInvitation_PositiveTest1()
        {
            invitationCollection = GetEmptyInviteCollection();
            invitationCollection.AddInvitation(invitation, customer);
            Assert.AreEqual(invitationCollection.Count(), 1);    
        }
        [Test]
        public void AddInvitation_NegativeTest1()
        {
            invitationCollection = GetEmptyInviteCollection();
            var invitation = new Mock<IInvitation>();
            var expectedException = false;
            var exceptionType = string.Empty;
            try
            {
                invitationCollection.AddInvitation(invitation.Object, customer);
            }
            catch(ArgumentNullException)
            {
                expectedException = true;
            }
            catch(Exception e)
            {
                exceptionType = e.GetType().ToString();
            }
            
            Assert.IsTrue(expectedException, string.Format("Expected excption not got, got {0} instead", exceptionType));
        }
        [Test]
        public void RemoveInvitation_PostiveTest()
        {
            invitationCollection = GetEmptyInviteCollection();
            invitationCollection.AddInvitation(invitation,customer);
            invitationCollection.RemoveInvitation(invitation);
            Assert.AreEqual(invitationCollection.Count(), 0);
        }
        [Test]
        public void RemoveInvitation_NegativeTest1()
        {
            invitationCollection = GetEmptyInviteCollection();
            var invitation = new Mock<IInvitation>();
            var expectedException = false;
            var exceptionType = string.Empty;
            try
            {
                invitationCollection.AddInvitation(invitation.Object, customer);
            }
            catch (ArgumentNullException)
            {
                expectedException = true;
            }
            catch (Exception e)
            {
                exceptionType = e.GetType().ToString();
            }
            Assert.IsTrue(expectedException, string.Format("Expected excption not got, got {0} instead", exceptionType));
        }
        [Test]
        public void GetInvitationById_PostiveTest()
        {
            invitationCollection = GetEmptyInviteCollection();
            invitationCollection.AddInvitation(invitation, customer);
            var id = invitation.InvitationId;
            var invite = (Invitation)invitationCollection.GetInvitationById(id);
            Assert.AreEqual(id, invite.InvitationId);
        }
        [Test]
        public void GetInvitationById_NegativeTest()
        {
            invitationCollection = GetEmptyInviteCollection();
            invitationCollection.AddInvitation(invitation, customer);
            var id = Guid.NewGuid().ToString();
            var invite = invitationCollection.GetInvitationById(id);
            Assert.IsNull(invite);
        }

    }
}

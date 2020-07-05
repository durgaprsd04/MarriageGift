using System;
using NUnit.Framework;
using Moq;
using log4net;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;

namespace MarriageGiftTest.Model.CustomerModel
{
    [TestFixture]
    class CustomerCollectionTest
    {
        IMock<ILog> mockLogger;
        ICustomerCollection customerCollection;
        [SetUp]
        public void Setup()
        {
            mockLogger = new Mock<ILog>();
            customerCollection = new CustomerCollection(mockLogger.Object);
        }
        [Test]
        public void AddCustomer_NegativeTest1()
        {
            var result = false;
            var errorType = string.Empty;
            try
            {
                var customer = new Mock<ICustomer>();
                result = customerCollection.AddCustomer(customer.Object);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception e)
            {
                errorType = e.GetType().ToString();
            }
            Assert.IsTrue(result, string.Format("The expected Exception ArgumentException was not found got {0} instead", errorType));
        }
        [Test]
        public void AddCustomer_PositiveTest1()
        {
            var invitations = new Mock<IInvitationCollection>();
            var events = new Mock<IEventCollection>();
            var customer = new Customer("testCust", invitations.Object, events.Object, mockLogger.Object);
            var result = customerCollection.AddCustomer(customer);
            Assert.IsTrue(result);
        }
    }
}

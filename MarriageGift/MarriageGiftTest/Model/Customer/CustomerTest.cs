using NUnit.Framework;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.CustomerModel
{
    public class CustomerTest
    {
        [SetUp]
        public void Setup()
        {
          
           string name="testname";
            var mockEvent = new Mock<IEventCollection>();
            var mockInvite = new Mock<IInvitationCollection>();
            var mockLog = new Mock<ILog>();
            Customer  c = new Customer(name,mockInvite.Object, mockEvent.Object, mockLog.Object );
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
using System;
using MarriageGift.Model.OccasionModel;
using MarriageGift.Model.Interfaces;
using NUnit.Framework;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.OccasionalModel
{
    [TestFixture]
    class BirthDayTest
    {
        private Birthday birthDay;
        private Mock<ILog> mockLog=new Mock<ILog>();
        [Test]
        public void modifyOccasion_PositiveTest1()
        {
            birthDay = new Birthday("Harry");
            var birthDayNew = new Birthday("Sammy");
            birthDay.modifyOccasion(birthDayNew);
            Assert.AreEqual(birthDay.Person, "Sammy");
        }
        [Test]
        public void modifyOccasion_NegativeTest1()
        {
            var expectedException = false;
            var exceptionType = string.Empty;
            birthDay = new Birthday("Harry");
            var birthDayNew = new Mock<IOccassion>();
            try
            {
                birthDay.modifyOccasion(birthDayNew.Object);
            }
            catch(ArgumentNullException)
            {
                expectedException = true;
            }
            catch(Exception e)
            {
                exceptionType = e.GetType().ToString();
            }
            Assert.IsTrue(expectedException,string.Format("Expected exception not got,got {0} instead", exceptionType));
        }

    }
}

using System;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.GiftModel;
using MarriageGift.Enums;
using NUnit.Framework;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.GiftModel
{
    [TestFixture]
    class GiftTest
    {
        Gift gift;
        IMock<ILog> logger;
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILog>();
            gift = new Gift("testgift", GiftItemType.Crockery, 200);
        }
        [Test]
        public void ModifyGift_PositiveTest1()
        {
            var dummyGift = new Gift("testgift", GiftItemType.Crockery, 300);
            gift.ModifyGift(dummyGift);            
            Assert.AreEqual(dummyGift.Price, gift.Price);
        }
        [Test]
        public void ModifyGift_NegativeTest1()
        {
            var mockObject = new Mock<IGift>();
            var result = true;
            var exceptionType = string.Empty;
            try
            {
                gift.ModifyGift(mockObject.Object);
            }
            catch(ArgumentNullException)
            {
                result = true; 
            }
            catch(Exception e)
            {
                exceptionType = e.GetType().ToString();
            }
            Assert.IsTrue(result, string.Format("Expected exception ArgumentNullException not got, got {0} instead ", exceptionType));
        }
        [Test]
        public void CompareTo_PositiveTest()
        {
            var result = gift.CompareTo(gift);
            Assert.AreEqual(result, 0);
        }
        [Test]
        public void CompareTo_NegativeTest()
        {
            var dummyGift = new Gift("testgift", GiftItemType.Crockery, 300);
            var result = gift.CompareTo(dummyGift);
            Assert.AreEqual(result, string.Compare(dummyGift.GetGiftId(), gift.GetGiftId()));
        }

    }
}

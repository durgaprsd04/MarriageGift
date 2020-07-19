using System;
using System.Collections.Generic;
using MarriageGift.Model.GiftModel;
using MarriageGift.Model.Interfaces;
using NUnit.Framework;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.GiftModel
{
    [TestFixture]
    class GiftCollectionTest
    {
        GiftCollection giftCollection;
        Mock<ILog> logger;
        private Dictionary<string, IGift> giftDummyCollection;
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILog>();
            giftDummyCollection = new Dictionary<string, IGift>();
        }
        public GiftCollection GetGiftCollection()
        {
            return new GiftCollection();
        }
        [Test]
        public void AddGift_PositiveTest1()
        {
            var giftId = Guid.NewGuid().ToString();
            var mockGift = new Mock<IGift>();
            var giftCollection = GetGiftCollection();
            mockGift.Setup(x => x.GetGiftId()).Returns(giftId);
            giftCollection.AddGift(mockGift.Object);
            Assert.AreEqual(giftCollection.Count(), 1);
        }
        [Test]
        public void RemoveGift_PostiveTest1()
        {
            var giftId = Guid.NewGuid().ToString();
            var mockGift = new Mock<IGift>();
            var giftCollection = GetGiftCollection();
            mockGift.Setup(x => x.GetGiftId()).Returns(giftId);
            giftCollection.AddGift(mockGift.Object);
            giftCollection.RemoveGift(mockGift.Object);
            Assert.AreEqual(giftCollection.Count(), 0);
        }
        [Test]
        public void RemoveGift_NegativeTest1()
        {
            var giftId = Guid.NewGuid().ToString();
            var mockGift = new Mock<IGift>();
            var giftCollection = GetGiftCollection();
            mockGift.Setup(x => x.GetGiftId()).Returns(giftId);
            var result= giftCollection.RemoveGift(mockGift.Object);
            Assert.IsFalse(result);
        }
        [Test]
        public void GetGiftById_PositiveTest1()
        {
            var giftId = Guid.NewGuid().ToString();
            var mockGift = new Mock<IGift>();
            var giftCollection = GetGiftCollection();
            mockGift.Setup(x => x.GetGiftId()).Returns(giftId);
            giftCollection.AddGift(mockGift.Object);
            var result = giftCollection.GetGiftById(giftId);
            Assert.AreEqual(result.GetGiftId(), giftId);
        }
        [Test]
        public void GetGiftById_NegativeTest1()
        {
            var giftId = Guid.NewGuid().ToString();
            var mockGift = new Mock<IGift>();
            var giftCollection = GetGiftCollection();
            mockGift.Setup(x => x.GetGiftId()).Returns(giftId);            
            var result = giftCollection.GetGiftById(giftId);
            Assert.IsNull(result);
        }
    }
}

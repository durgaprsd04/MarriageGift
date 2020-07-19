using System;
using System.Collections.Generic;
using System.Text;
using MarriageGift.Enums;
using MarriageGift.Model.GiftModel;
using NUnit.Framework;
using Moq;
using log4net;
namespace MarriageGiftTest.Model.GiftModel
{
    [TestFixture]
    class PresentableGiftTest
    {
        PresentableGift presentableGift;        
        [Test]
        public void PresentableGift_PositivetTest1()
        {
            var mockLog = new Mock<ILog>();
            var gift = new Gift("testGift", GiftItemType.Furniture, 200);
            presentableGift = new PresentableGift("newGuy", gift);
            Assert.AreEqual(presentableGift.Presenter, "newGuy");
        }
    }
}

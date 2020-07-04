using System;
using System.Collections.Generic;
using System.Text;
using MarriageGift.Model.Interfaces;
namespace MarriageGift.Model
{
    class GiftCollection : IGiftCollection
    {
        public readonly IDictionary<string, IGift> giftCollection;

        public GiftCollection(IDictionary<string, IGift> giftCollection)
        {
            this.giftCollection = giftCollection;
        }

        public bool AddGift(IGift giftItem)
        {
            var successFlag = false;
            var gift = giftItem as Gift;
            if (gift == null)
                throw new ArgumentException("giftItem");
            try
            {
                giftCollection.Add(gift.GiftId, gift);
                successFlag = true;
            }
            catch(Exception e)
            {

                //ignore now 
            }
            return successFlag;
        }

        public bool RemoveGift(IGift giftItem)
        {
            var successFlag = false;
            var gift = giftItem as Gift;
            if (gift == null)
                throw new ArgumentException("giftItem");
            try
            {
                giftCollection.Remove(gift.GiftId);
                successFlag = true;
            }
            catch(Exception e)
            {
                //ignore now
            }
            return successFlag;
        }
    }
}

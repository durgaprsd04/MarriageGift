using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;
namespace MarriageGift.Model.GiftModel
{
   public class GiftCollection : IGiftCollection<IGift>
    {
        public readonly IDictionary<string, IGift> giftCollection;

        public GiftCollection(IDictionary<string, IGift> giftCollection)
        {
            this.giftCollection = giftCollection;
        }

        public bool AddGift(IGift gift)
        {
            var successFlag = false;
            try
            {
                giftCollection.Add(gift.GetGiftId(), gift);
                successFlag = true;
            }
            catch(Exception e)
            {

                //ignore now 
            }
            return successFlag;
        }

        public IGift GetGiftById(string giftId)
        {
            if(giftCollection.ContainsKey(giftId))
            {
                return giftCollection[giftId];
            }
            return null;
        }

        public bool RemoveGift(IGift gift)
        {
            var successFlag = false;
            try
            {
                giftCollection.Remove(gift.GetGiftId());
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

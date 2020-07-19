using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;
namespace MarriageGift.Model.GiftModel
{
   public class GiftCollection : GenericCollection,  IGiftCollection<IGift>
    {       
        public GiftCollection()
        :base()
        {
            
        }
        public bool AddGift(IGift gift)
        {
            if (!(gift is Gift giftItem))
                throw new ArgumentNullException("eventItem");
            return Add(giftItem);
        }       

        public IGift GetGiftById(string giftId)
        {
            return (IGift)GetItem(giftId);
        }

        public bool RemoveGift(IGift gift)
        {
            if (!(gift is Gift giftItem))
                throw new ArgumentNullException("eventItem");
            return Remove(gift);
        }        
    }
}

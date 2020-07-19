using log4net;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;
namespace MarriageGift.Model.GiftModel
{
   public class GiftCollection : GenericCollection,  IGiftCollection<IGift>
    {
        private readonly IDictionary<string, IGift> giftCollection;
        public GiftCollection(IDictionary<string, IGift> giftCollection, ILog logger)
        {
            this.giftCollection = giftCollection;
        }
        public GiftCollection( ILog logger)
        {
            giftCollection = new Dictionary<string, IGift>();
        }
        public GiftCollection()
        {
            giftCollection = new Dictionary<string, IGift>();
        }
        public bool AddGift(IGift gift)
        {
            return Add(gift);
        }

        public int Count()
        {
            return giftCollection.Count;
        }

        public IGift GetGiftById(string giftId)
        {
            IGift gift=null;
            if(giftCollection.ContainsKey(giftId))
            {
                gift = giftCollection[giftId];
            }                        
            return gift;
        }

        public bool RemoveGift(IGift gift)
        {
            var successFlag = false;
            if (giftCollection.ContainsKey(gift.GetGiftId()))
            {
                giftCollection.Remove(gift.GetGiftId());
                successFlag = true;
            }
            return successFlag;
        }
        
    }
}

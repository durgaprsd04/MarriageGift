using System;
using log4net;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;
using MarriageGift.Exceptions;
namespace MarriageGift.Model.GiftModel
{
   public class GiftCollection : IGiftCollection<IGift>
    {
        private readonly IDictionary<string, IGift> giftCollection;
        private readonly ILog logger;
        public GiftCollection(IDictionary<string, IGift> giftCollection, ILog logger)
        {
            this.giftCollection = giftCollection;
            this.logger= logger;
        }

        public bool AddGift(IGift gift)
        {
            var result = false;
            try
            {
                giftCollection.Add(gift.GetGiftId(), gift);
                result = true;
            }
            catch(Exception e)
            {
                throw new GiftCollectionAddException(e.Message);
            }
            return result;
        }

        public int Count()
        {
            return giftCollection.Count;
        }

        public IGift GetGiftById(string giftId)
        {
            IGift gift=null;
            try
            {
                if(giftCollection.ContainsKey(giftId))
                {
                    gift = giftCollection[giftId];
                }
            }
            catch(Exception e)
            {
                logger.Error("Exception occured while fetching gift from collection "+e.Message);
                logger.Error(e.Message);
            }            
            return gift;
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
                logger.Error("Exception occured while removing gift"+e.Message);
                logger.Error(e.Message);
            }
            return successFlag;
        }
        
    }
}

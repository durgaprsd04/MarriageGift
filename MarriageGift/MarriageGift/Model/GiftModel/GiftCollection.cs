﻿using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;
using log4net;
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
            var successFlag = false;
            try
            {
                giftCollection.Add(gift.GetGiftId(), gift);
                successFlag = true;
            }
            catch(Exception e)
            {
                logger.Error("Exception occured while adding gift to collection "+e.Message);
                logger.Error(e.Message);
            }
            return successFlag;
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

using System;
using System.Collections.Generic;
using log4net;
﻿using MarriageGift.DAO.Interfaces;
using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.DAO.Wrappers
{
    public class GiftDaoWrapper:IGiftDao
    {
        private readonly ILog logger;
        public GiftDaoWrapper(ILog logger)
        {
            this.logger= logger;
        }
        public void Delete(string id)
        {
            GiftDao.Delete(id);
        }

        public IDictionary<string, string> GetAllGifts()
        {
            return GiftDao.GetAllGifts(logger);
        }

        public IGenericCollection<IBaseObject> GetListOfObjectsByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(IBaseObject baseObject)
        {
            GiftDao.Insert(baseObject);
        }

        public IBaseObject Read(string id)
        {
          IGift gift=null;
          try
          {
            logger.InfoFormat("Getting gift from database for Id {0}", id);
            gift= (IGift)GiftDao.Read(id);
          }
          catch(Exception e)
          {
            logger.ErrorFormat("Error {1} occured while calling getting gift for id {0} ",id, e.Message);
          }
          return gift;
        }

        public void Update(IBaseObject baseObject)
        {
            GiftDao.Update(baseObject);
        }
        public void AddGiftToExpectedGifts(IGift gift, string eventId )
        {
          logger.InfoFormat("Adding gift with id {0} to exepcted gift list {1}", gift.getId(), eventId);
          GiftDao.AddGiftToExpectedGifts(gift, eventId);
        }
    }
}

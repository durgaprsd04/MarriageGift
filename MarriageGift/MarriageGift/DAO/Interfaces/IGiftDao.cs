using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.DAO.Interfaces
{
    public interface IGiftDao:IGenericDao
    {
        IDictionary<string, string> GetAllGifts();
        void AddGiftToExpectedGifts(IGift gifts, string eventId );
    }
}

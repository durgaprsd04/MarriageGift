using System;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model;

namespace MarriageGift.DAO.DAOS
{
    class GiftDao
    {
        internal static void Delete(string id)
        {
            throw new NotImplementedException();
        }

        internal static void Insert(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }

        internal static void Read(string id)
        {
            throw new NotImplementedException();
        }

        internal static void Update(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }
        internal static IGiftCollection<IGift> GetExpectedGiftsForEvent(string eventId)
        {
            throw new NotImplementedException();
        }
        internal static IGiftCollection<IGift> GetRecievedGiftsForEvent(string eventId)
        {
            throw new NotImplementedException();
        }
    }
}

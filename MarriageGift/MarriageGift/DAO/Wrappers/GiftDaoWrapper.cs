using MarriageGift.DAO.Interfaces;
using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using log4net;
using System.Collections.Generic;

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
            return GiftDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            GiftDao.Update(baseObject);
        }
    }
}

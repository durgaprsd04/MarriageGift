using log4net;
using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.DAO.Interfaces;
using System.Collections.Generic;

namespace MarriageGift.DAO.Wrappers
{
    public class OccasionDaoWrapper :IOccassionDao
    {
        private ILog logger;
        public OccasionDaoWrapper(ILog logger)
        {
            this.logger=logger;
        }
        public void Delete(string id)
        {
            OccassionDao.Delete(id);
        }

        public IGenericCollection<IBaseObject> GetListOfObjectsByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, string> GetOcccasionTypes()
        {
            return OccassionDao.GetOcccasionTypes(logger);
        }

        public void Insert(IBaseObject baseObject)
        {
            OccassionDao.Insert(baseObject);
        }

        public IBaseObject Read(string id)
        {
            return OccassionDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            OccassionDao.Update(baseObject);
        }
    }
}

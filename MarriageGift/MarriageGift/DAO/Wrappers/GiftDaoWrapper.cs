using MarriageGift.DAO.Interfaces;
using MarriageGift.DAO.DAOS;
using MarriageGift.Model;

namespace MarriageGift.DAO.Wrappers
{
    public class GiftDaoWrapper:IGiftDao
    {
        public void Delete(string id)
        {
            GiftDao.Delete(id);
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

using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.DAO.Interfaces;
namespace MarriageGift.DAO.Wrappers
{
    public class OccasionDaoWrapper :IOccassionDao
    {
        public void Delete(string id)
        {
            OccassionDao.Delete(id);
        }

        public IGenericCollection<IBaseObject> GetListOfObjectsByName(string name)
        {
            throw new System.NotImplementedException();
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

using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.DAO.Interfaces;
namespace MarriageGift.DAO.Wrappers
{
    class OccasionDaoWrapper :IGenericDao
    {
        public void Delete(string id)
        {
            OccassionDao.Delete(id);
        }

        public void Insert(IBaseObject baseObject)
        {
            OccassionDao.Insert(baseObject);
        }

        public void Read(string id)
        {
            OccassionDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            OccassionDao.Update(baseObject);
        }
    }
}

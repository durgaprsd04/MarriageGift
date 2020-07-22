using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.DAO.Interfaces;
namespace MarriageGift.DAO.Wrappers
{
    class EventDaoWrapper : IGenericDao
    {
        public void Delete(string id)
        {
            EventDao.Delete(id);
        }

        public void Insert(IBaseObject baseObject)
        {
            EventDao.Insert(baseObject);
        }

        public IBaseObject Read(string id)
        {
            return EventDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            EventDao.Update(baseObject);
        }
    }
}

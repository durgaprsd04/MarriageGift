using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.DAO.Interfaces;
namespace MarriageGift.DAO.Wrappers
{
    public class EventDaoWrapper : IEventDao
    {
        public void Delete(string id)
        {
            EventDao.Delete(id);
        }

        public IGenericCollection<IBaseObject> GetListOfObjectsByName(string name)
        {
            throw new System.NotImplementedException();
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

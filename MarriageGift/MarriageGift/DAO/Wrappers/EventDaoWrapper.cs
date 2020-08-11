using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.DAO.Interfaces;
using log4net;
namespace MarriageGift.DAO.Wrappers
{
    public class EventDaoWrapper : IEventDao
    {
        private readonly ILog logger;
        public EventDaoWrapper(ILog logger)
        {
          this.logger = logger;
        }
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
        public IEventCollection GetEventsByCustomerId(string custId)
        {
          IEventCollection result = new EventCollection();
          logger.Info("Getting all events for customer id {0}", custId);
          try
          {
            var result =  EventDao.GetEventsForCustId(custId);
          }
          catch(Exception e)
          {
            logger.ErrorFormat("Error while fetching result for customer id {0}",custId);
          }
          return result;
        }
    }
}

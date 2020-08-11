using MarriageGift.DAO.DAOS;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Model;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.DAO.Wrappers
{
    public class CustomerDaoWrapper : ICustomerDao
    {
        private readonly ILog logger;
        public CustomerDaoWrapper(ILog logger)
        {
            this.logger=logger;
        }
        public void Delete(string id)
        {
            CustomerDao.Delete(id);
        }

        public IGenericCollection<IBaseObject> GetListOfObjectsByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(IBaseObject baseObject)
        {
            CustomerDao.Insert(baseObject);
        }

        public ICustomer  Login(string username, string password)
        {
            return CustomerDao.Login(username, password, logger);
        }

        public IBaseObject Read(string id)
        {
            return CustomerDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            CustomerDao.Update(baseObject);
        }
    }
}

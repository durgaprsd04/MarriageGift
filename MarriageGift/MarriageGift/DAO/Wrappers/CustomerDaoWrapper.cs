using MarriageGift.DAO.DAOS;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Model;

namespace MarriageGift.DAO.Wrappers
{
    public class CustomerDaoWrapper : ICustomerDao
    {
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

        public bool Login(string username, string password)
        {
            return CustomerDao.Login(username, password);
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

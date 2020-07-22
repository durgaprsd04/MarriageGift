using MarriageGift.DAO.DAOS;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Model;

namespace MarriageGift.DAO.Wrappers
{
    class CustomerDaoWrapper : IGenericDao
    {
        public void Delete(string id)
        {
            CustomerDao.Delete(id);
        }

        public void Insert(IBaseObject baseObject)
        {
            CustomerDao.Insert(baseObject);
        }

        public void Read(string id)
        {
            CustomerDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            CustomerDao.Update(baseObject);
        }
    }
}

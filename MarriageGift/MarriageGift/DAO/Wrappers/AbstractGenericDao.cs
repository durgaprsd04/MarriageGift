using MarriageGift.DAO.Interfaces;
using MarriageGift.DAO.DAOS;
using MarriageGift.Model;

namespace MarriageGift.DAO.Wrappers
{
    class AbstractGenericDao : IGenericDao
    {
        private string daoType = string.Empty;
        public AbstractGenericDao(string daoType)
        {
            this.daoType = daoType;
        }

        public void Delete(string id)
        {
            if (daoType == "Customer")
                CustomerDao.Delete(id);
            else if (daoType == "Event")
                EventDao.Delete(id);
            else if (daoType == "Inivitation")
                InvitationDao.Delete(id);
            else if (daoType == "Gift")
                GiftDao.Delete(id);
        }

        public void Insert(IBaseObject baseObject)
        {
            if (daoType == "Customer")
                CustomerDao.Insert(baseObject);
            else if (daoType == "Event")
                EventDao.Insert(baseObject);
            else if (daoType == "Inivitation")
                InvitationDao.Insert(baseObject);
            else if (daoType == "Gift")
                GiftDao.Insert(baseObject);
        }

        public void Read(string id)
        {
            if (daoType == "Customer")
                CustomerDao.Read(id);
            else if (daoType == "Event")
                EventDao.Read(id);
            else if (daoType == "Inivitation")
                InvitationDao.Read(id);
            else if (daoType == "Gift")
                GiftDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            if (daoType == "Customer")
                CustomerDao.Update(baseObject);
            else if (daoType == "Event")
                EventDao.Update(baseObject);
            else if (daoType == "Inivitation")
                InvitationDao.Update(baseObject);
            else if (daoType == "Gift")
                GiftDao.Update(baseObject);
        }
    }
}

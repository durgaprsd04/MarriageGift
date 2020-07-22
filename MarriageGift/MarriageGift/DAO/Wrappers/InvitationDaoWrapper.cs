using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.DAO.Interfaces;
namespace MarriageGift.DAO.Wrappers
{
    class InvitationDaoWrapper : IGenericDao
    {
        public void Delete(string id)
        {
            InvitationDao.Delete(id);
        }

        public void Insert(IBaseObject baseObject)
        {
            InvitationDao.Insert(baseObject);
        }

        public void Read(string id)
        {
            InvitationDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            InvitationDao.Update(baseObject);
        }
    }
}

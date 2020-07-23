using MarriageGift.DAO.DAOS;
using MarriageGift.Model;
using MarriageGift.DAO.Interfaces;
namespace MarriageGift.DAO.Wrappers
{
    public class InvitationDaoWrapper : IInvitationDao
    {
        public void Delete(string id)
        {
            InvitationDao.Delete(id);
        }

        public void Insert(IBaseObject baseObject)
        {
            InvitationDao.Insert(baseObject);
        }

        public IBaseObject Read(string id)
        {
            return InvitationDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            InvitationDao.Update(baseObject);
        }
    }
}

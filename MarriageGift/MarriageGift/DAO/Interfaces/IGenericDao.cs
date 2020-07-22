using MarriageGift.Model;

namespace MarriageGift.DAO.Interfaces
{
    public interface IGenericDao
    {       
        void Delete(string id);
        void Insert(IBaseObject baseObject);
        IBaseObject Read(string id);
        void Update(IBaseObject baseObject);
    }
}

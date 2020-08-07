using MarriageGift.Model.Interfaces;
namespace MarriageGift.DAO.Interfaces
{
    public interface ICustomerDao : IGenericDao
    {
        string Login(string username, string password);        
    }
}

using MarriageGift.Model.Interfaces;
namespace MarriageGift.DAO.Interfaces
{
    public interface ICustomerDao : IGenericDao
    {
        ICustomer Login(string username, string password);        
    }
}

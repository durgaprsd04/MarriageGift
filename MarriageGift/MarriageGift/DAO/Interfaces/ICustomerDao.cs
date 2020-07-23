namespace MarriageGift.DAO.Interfaces
{
    public interface ICustomerDao : IGenericDao
    {
        bool Login(string username, string password);
    }
}

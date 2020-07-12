namespace MarriageGift.Model.Interfaces
{
    public interface ICustomerCollection
    {
        bool AddCustomer(ICustomer customer);
        bool RemoveCustomer(ICustomer customer);
        int Count();
    }
}

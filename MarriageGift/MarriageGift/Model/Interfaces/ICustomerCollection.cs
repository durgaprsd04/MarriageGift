namespace MarriageGift.Model.Interfaces
{
    public interface ICustomerCollection :IGenericCollection<IBaseObject> 
    {
        bool AddCustomer(ICustomer customer);
        bool RemoveCustomer(ICustomer customer);
        ICustomer GetCustomer(string custId);
    }
}

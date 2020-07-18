using System;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.CustomerModel
{
    public class CustomerCollection : GenericCollection, ICustomerCollection
    {        
        public bool AddCustomer(ICustomer customer)
        {
            if (!(customer is Customer cust))
                throw new ArgumentException("Customer");
            return Add(cust);
        }
        public bool RemoveCustomer(ICustomer customer)
        {
            if (!(customer is Customer cust))
                throw new ArgumentException("Customer");
            return Remove(customer);
        }
        public ICustomer GetCustomer(string custId)
        {
            return (ICustomer)GetItem(custId);
        }       
    }
}

using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;
using MarriageGift.Exceptions.CustomerExceptions;
using log4net;

namespace MarriageGift.Model.CustomerModel
{
    public class CustomerCollection : ICustomerCollection
    {
        private readonly Dictionary<string, ICustomer> customerCollection;        
        public CustomerCollection( ILog logger)
        {
            customerCollection = new Dictionary<string, ICustomer>();
        }

        public bool AddCustomer(ICustomer customer)
        {
            var cust = customer as Customer;
            if (cust == null)
                throw new ArgumentException("Customer");
            customerCollection.Add(cust.CustId, cust);
            return true;
        }
        public bool RemoveCustomer(ICustomer customer)
        {
            var succesFlag = false;
            var cust = customer as Customer;
            if (cust == null)
                throw new ArgumentException("Customer");
            if (customerCollection.ContainsKey(cust.CustId))
            {
                customerCollection.Remove(cust.CustId);
                succesFlag = true;
            }
            return succesFlag;
        }
        public int Count()
        {
            return customerCollection.Count;
        }
    }
}

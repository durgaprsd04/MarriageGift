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
            var succesFlag = false;
            var cust = customer as Customer;
            if (cust == null)
                throw new ArgumentException("Customer");
            if (customerCollection.ContainsKey(cust.CustId))
            {
                try
                {
                    customerCollection.Add(cust.CustId, cust);
                    succesFlag = true;
                }
                catch(Exception e)
                {
                    throw new CustomerCollectionAddException(e.Message);
                }
            }
            return succesFlag;
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

using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;
using log4net;

namespace MarriageGift.Model.CustomerModel
{
    public class CustomerCollection : ICustomerCollection
    {

        private readonly Dictionary<string, ICustomer> customerCollection;
        private readonly ILog logger; 

        public CustomerCollection( ILog logger)
        {
            customerCollection = new Dictionary<string, ICustomer>();
            this.logger = logger;
        }

        public bool AddCustomer(ICustomer customer)
        {
            var cust = customer as Customer;
            if (cust == null)
                throw new ArgumentException("Customer");
            customerCollection.Add(cust.CustId, cust);
            return true;
        }
        public int Count()
        {
            return customerCollection.Count;
        }
    }
}

using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.CustomerModel
{
    public class CustomerCollection : ICustomerCollection
    {
        readonly Dictionary<string, ICustomer> customerCollection = new Dictionary<string, ICustomer>();
        public bool AddCustomer( ICustomer customer)
        {
            var result = false;
            var cust = customer as Customer;
            if (cust == null)
                throw new ArgumentException("Customer");
            try
            {
                customerCollection.Add(cust.CustId, cust);
                return true;
            }
            catch(Exception e)
            {
                //ignored
            }
            return result;
        }
    }
}

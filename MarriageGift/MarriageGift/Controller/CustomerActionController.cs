using System;
using System.Collections.Generic;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.GiftModel;
using log4net;

namespace MarriageGift.Controller
{
    public class CustomerActionController
    {
        public IGiftCollection<IGift> GetAvailableGiftCollection(ILog logger)
        {
            return new GiftCollection(); 
        }
        public ICustomerCollection GetAllCustomers(string regex)
        {
            return new CustomerCollection();
        }
    }
}

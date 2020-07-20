using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.GiftModel;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Exceptions.CustomerExceptions;
using MarriageGift.FAO.Interfaces;
using System;
using log4net;

namespace MarriageGift.Controller
{
    public class CustomerActionController
    {
        private readonly ICustomer customer;
        private readonly  IGenericDao genericaDao;
        private readonly ISaveToFileFao saveToFileFAO;
        private readonly ILog logger;

        public CustomerActionController(ICustomer customer, IGenericDao genericaDao, ISaveToFileFao saveToFileFAO, ILog logger )
        {
            this.customer = customer;
            this.genericaDao = genericaDao;
            this.saveToFileFAO = saveToFileFAO;
            this.logger = logger;
        }
        public void SaveCustomer()
        {
            try
            {
                genericaDao.Insert(customer);
            }
           catch(Exception e)
            {
                logger.Error(e.StackTrace);
                throw new CustomerCollectionAddException(e.Message);
            }
        }
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

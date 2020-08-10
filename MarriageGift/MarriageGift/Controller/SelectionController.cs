using System.Collections.Generic;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Controller.Interfaces;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.OccasionModel;
using MarriageGift.Enums;
using MarriageGift.Model.GiftModel;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Controller
{
    public class SelectionController: ISelectionController
    {
        private readonly ICustomerDao customerDao;
        private readonly IEventDao eventDao;
        private readonly IOccassionDao occassionDao;
        private readonly IInvitationDao invitationDao;
        private readonly IGiftDao giftDao;
        private readonly ILog logger;
        public SelectionController(ICustomerDao customerDao, IEventDao eventDao, IInvitationDao invitationDao, IOccassionDao occassionDao,IGiftDao giftDao, ILog logger)
        {
            this.customerDao = customerDao;
            this.eventDao = eventDao;
            this.occassionDao = occassionDao;
            this.invitationDao = invitationDao;
            this.giftDao = giftDao;
            this.logger = logger;
        }
        public Dictionary<int, string> GetOccasionTypes()
        {
            return occassionDao.GetOcccasionTypes();
        }

        public IDictionary<string, string> GetAllGifts()
        {
           return  giftDao.GetAllGifts();
        }
        public string GetCustomerById(string customerId)
        {
            if(string.IsNullOrWhiteSpace(customerId))
                return new Customer("invalid", "invalid").ToString();
            return customerDao.Read(customerId).ToString();
        }
        public IGiftCollection<IGift> GetGiftsForGiftIds(string [] giftIdList)
        {
            var giftCollection = new GiftCollection();
            foreach(var giftId in giftIdList)
            {
              var gift =(IGift)giftDao.Read(giftId);
              logger.InfoFormat("Gift with id {0} added", gift.getId());
                giftCollection.Add(gift);
            }

            return giftCollection;
        }
        public IOccassion GetOccassion(Occasion occassion, string person1, string person2)
        {
            IOccassion occassionInQ=null ;
            switch(occassion)
            {
                case Occasion.Marriage:
                    occassionInQ = new Marriage(person1,person2);
                    break;
                case Occasion.Birthday:
                    occassionInQ = new Birthday(person2);
                    break;
                case Occasion.HouseWarming:
                    occassionInQ = new HouseWarming(person2);
                    break;
            }
            return occassionInQ;
        }
    }
}

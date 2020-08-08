using System.Collections.Generic;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Controller.Interfaces;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.OccasionModel;
using MarriageGift.Enums;
using MarriageGift.Model.GiftModel;
using MarriageGift.Model.Interfaces;
namespace MarriageGift.Controller
{
    public class SelectionController: ISelectionController
    {
        private readonly ICustomerDao customerDao;
        private readonly IEventDao eventDao;
        private readonly IOccassionDao occassionDao;
        private readonly IInvitationDao invitationDao;
        private readonly IGiftDao giftDao;
        public SelectionController(ICustomerDao customerDao, IEventDao eventDao, IInvitationDao invitationDao, IOccassionDao occassionDao,IGiftDao giftDao)
        {
            this.customerDao = customerDao;
            this.eventDao = eventDao;
            this.occassionDao = occassionDao;
            this.invitationDao = invitationDao;
            this.giftDao = giftDao;
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
                giftCollection.Add((IGift)giftDao.Read(giftId));
            return giftCollection;
        }
        public IOccassion GetDummyOccassion(Occasion occassion )
        {
            IOccassion occassionInQ=null ;
            switch(occassion)
            {
                case Occasion.Marriage:
                    occassionInQ = new Marriage("dummybride","dummyGroom");
                    break;
                case Occasion.Birthday:
                    occassionInQ = new Birthday("dummyperson");
                    break;
                case Occasion.HouseWarming:
                    occassionInQ = new HouseWarming("dummyperson");
                    break;                    
            }
            return occassionInQ;
        }
    }
}
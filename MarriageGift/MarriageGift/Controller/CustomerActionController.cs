using MarriageGift.Model.Interfaces;
using MarriageGift.Model.EventModel;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Exceptions.CustomerExceptions;
using MarriageGift.FAO.Interfaces;
using MarriageGift.Controller.Interfaces;
using System;
using log4net;
using MarriageGift.Model.InvitationModel;

namespace MarriageGift.Controller
{
    public class CustomerActionController :ICustomerController
    {
        private readonly ICustomer customer;
        private readonly ICustomerDao customerDao;
        private readonly IEventDao eventDao;
        private readonly IOccassionDao occassionDao;
        private readonly IInvitationDao invitationDao;
        private readonly IGiftDao giftDao;
        private readonly ISaveToFileFao saveToFileFAO;
        private readonly ILog logger;

        public CustomerActionController(ICustomer customer, ICustomerDao customerDao, IEventDao eventDao, IInvitationDao invitationDao, IOccassionDao occassionDao,IGiftDao giftDao, ISaveToFileFao saveToFileFAO, ILog logger )
        {
            this.customer = customer;
            this.customerDao = customerDao;
            this.eventDao = eventDao;
            this.occassionDao = occassionDao;
            this.invitationDao = invitationDao;
            this.giftDao = giftDao;
            this.saveToFileFAO = saveToFileFAO;
            this.logger = logger;
        }
        public void CustomerController()
        {
            Console.WriteLine("test hello world  ");
            Console.ReadKey();
        }
        public void SaveCustomer()
        {
            try
            {
                customerDao.Update(customer);
            }
           catch(Exception e)
            {
                logger.Error(e.StackTrace);
                throw new CustomerCollectionAddException(e.Message);
            }
        }

        public bool Login(string username, string password)
        {
            var result = customerDao.Login(username, password);
            if (result)
                logger.Info("Login successful");
            else
                logger.Info("login unsuccessful");
            return result;
        }

        public bool CreateEvent(IOccassion occassion, string place, DateTime date, IGiftCollection<IGift> giftE, IGiftCollection<IGift> giftR)
        {
            var result = false;
            try
            {
                var newEvent = new Event(occassion, place, date, customer.getId());
                newEvent.AddExpectedGifts(giftE);
                eventDao.Insert(newEvent);
                result= true;
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
                throw new Exceptions.EventExceptions.EventCollectionAddException(e.Message);
            }
            return result;
        }

        public bool InvitePerson(IEvent eventInQ)
        {
            var result = false;
            try
            {
                var invite = new Invitation(customer, eventInQ);
                invitationDao.Insert(invite);
                result=true;
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
                throw new Exceptions.InvitationExceptions.InvitationCollectionAddException(e.Message);
            }
            return result;
        }

        public bool BuyGiftForEvent(IInvitation invite, string giftId)
        {
            var result = false;
            try
            {
                var gift = (IGift)giftDao.Read(giftId);
                invite.AddGiftForEvent(gift);
                var eventInQuestion = invite.GetEvent();
                eventDao.Update(eventInQuestion);
                invitationDao.Update(invite);
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
                throw new Exceptions.GiftExceptions.GiftCollectionAddException(e.Message);
            }
            return result;
        }

        public bool RemoveGiftForEvent(IInvitation invite, string giftId)
        {
            var result = false;
            try
            {
                var gift = (IGift)giftDao.Read(giftId);
                invite.RemoveGiftForEvent(gift);
                var eventInQuestion = invite.GetEvent();
                eventDao.Update(eventInQuestion);
                invitationDao.Update(invite);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exceptions.GiftExceptions.GiftCollectionRemoveException(e.Message);
            }
            return result;
        }

        public bool RespondToInvite(string inviteId, bool response)
        {
            var result = false;
            try
            {
                var invite = (Invitation)invitationDao.Read(inviteId);
                invite.RespondToInvitation(response);
                invitationDao.Update(invite);
                result = true;
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
                throw new Exceptions.InvitationExceptions.InvitationNotFoundException(e.Message);
            }
            return result;
        }

        public bool ModifEvent(IEvent eventInQ)
        {
            var result = false;
            try
            {               
                eventDao.Update(eventInQ);
                result = true;
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
                throw new Exceptions.EventExceptions.EventNotFoundException(e.Message);
            }
            return result;
        }

        public bool SaveCustomerToFile()
        {
            var result = false;
            try
            {
                saveToFileFAO.SaveObject(customer);
                result = true;
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
                throw new CustomerNotFoundException(e.Message);
            }
            return result;
        }

        public bool ChangePassword(string username, string password)
        {

            //todo not implementted.
            return true;
        }

        public string GetCustomerId()
        {
            return customer.getId();
        }
    }
}

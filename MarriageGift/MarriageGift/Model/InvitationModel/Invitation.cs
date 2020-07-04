using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.InvitationModel
{

    public class Invitation : IInvitation
    {
        private readonly string invitationId;
        private string sender;
        private IEvent mainEvent;
        private bool isAccepted;
        private ICustomerCollection customerCollection;
        private readonly ILog  logger;
        public Invitation(string sender, IEvent mainEvent, ICustomerCollection customerCollection, ILog logger)
        {
            invitationId = Guid.NewGuid().ToString();
            this.sender =sender;
            this.mainEvent = mainEvent;
            this.customerCollection = customerCollection;
            this.logger = logger;
        }
        
        public string InvitationId => invitationId;

        public bool RespondToInvitation(bool response)
        {
            var successFlag =false;
            try
            {
                isAccepted=response;
                successFlag = response;
            }
            catch(Exception e)
            {
                logger.Error("Error occured while accepting invitiation "+e.Message);
                logger.Error(e.Message);
            }
            return successFlag;
        }
        public IGiftCollection<IGift> GetGiftsForEvent()
        {
            return mainEvent.ExpectedGiftCollection();
        }

        public bool AddGiftForEvent(IGift gift)
        {
            return mainEvent.AddRecievedGifts(gift);
        }

        public bool RemoveGiftForEvent(IGift giftId)
        {
            return mainEvent.RemoveRecievedGifts(giftId);
        }

        public bool AddCustomerToListofInvitees(ICustomer customer)
        {
            return customerCollection.AddCustomer(customer);
        }

        public ICustomerCollection GetListofInvitees()
        {
            return customerCollection;
        }
    }
}
    
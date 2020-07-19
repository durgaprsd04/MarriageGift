using System;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.CustomerModel;
namespace MarriageGift.Model.InvitationModel
{

    public class Invitation :BaseObject,  IInvitation
    {
        private readonly string invitationId;
        private ICustomer sender;
        private IEvent mainEvent;
        private bool isAccepted;
        private ICustomerCollection customerCollection = new CustomerCollection();
        
        public Invitation(ICustomer sender, IEvent mainEvent)
        :base()
        {           
            this.sender =sender;
            this.mainEvent = mainEvent;                       
        }
         public Invitation(string inviteId, ICustomer sender, IEvent mainEvent)
        :base(inviteId)
        {           
            this.sender =sender;
            this.mainEvent = mainEvent;                       
        }
        
        public string InvitationId => invitationId;

        public bool IsAccepted { get => isAccepted;}

        public void RespondToInvitation(bool response)
        {
            isAccepted=response;
        }
        public IGiftCollection<IGift> GetRecievedGiftsForEvent()
        {
            return mainEvent.RecievedGiftCollection();
        }
        public IGiftCollection<IGift> GetExpectedGiftsForEvent()
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
    
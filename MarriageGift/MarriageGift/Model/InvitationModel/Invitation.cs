using System;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.InvitationModel
{

    public class Invitation : IInvitation
    {
        private readonly string invitationId;
        private string sender;
        private IEvent mainEvent;
        private bool isAccepted;
        private ICustomerCollection customerCollection;
        public Invitation(string sender, IEvent mainEvent, ICustomerCollection customerCollection)
        {
            invitationId = Guid.NewGuid().ToString();
            this.sender =sender;
            this.mainEvent = mainEvent;
            this.customerCollection = customerCollection;
        }
        
        public string InvitationId => invitationId;

        public bool RespondToInvitation(bool response)
        {
            isAccepted=response;
            return response;
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
    
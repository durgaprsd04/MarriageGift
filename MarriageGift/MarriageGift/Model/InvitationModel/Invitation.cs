using System;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.InvitationModel
{

    public class Invitation : IInvitation
    {
        private string invitationId;
        private string sender;
        private IEvent mainEvent;
        private bool isAccepted;
        private ICustomerCollection customerCollection;
        public Invitation(string sender, IEvent mainEvent)
        {
            InvitationId = Guid.NewGuid().ToString();
            this.sender =sender;
            this.mainEvent = mainEvent;
        }

        public string InvitationId { get => invitationId; set => invitationId = value; }

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
    }
}
    
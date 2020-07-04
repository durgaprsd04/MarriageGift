using System;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.GiftModel;
namespace MarriageGift.Model.CustomerModel
{
    class Customer:ICustomer
    {
        private readonly string custId;
        private string userName;
        private readonly IInvitationCollection invitations;
        private readonly IEventCollection events;

        public Customer(string userName, IInvitationCollection invitations, IEventCollection events)
        {
            custId = Guid.NewGuid().ToString();
            this.userName = userName;
            this.invitations = invitations;
            this.events = events;
        }
        public bool AddMyEvents(IEvent myEvent)
        {
            var result = events.AddEvent(myEvent);
            return result;
        }

        public bool AddMyInvitations(IInvitation invitation)
        {
            var result = invitations.AddInvitation(invitation);
            return result;
        }        
        public bool CancelEvent(string eventId)
        {
            var eventInQuestion = events.GetEventById( eventId);
            var result = eventInQuestion.Cancel();
            return result;

        }

        public bool ChangeEventTime(string eventId, DateTime date)
        {
            var eventInQuestion = events.GetEventById( eventId);
            var result = eventInQuestion.ModifyDate(date);
            return result;
        }

        public bool ChangeEventVenue(string eventId, string place)
        {
            var eventInQuestion = events.GetEventById( eventId);
            var result = eventInQuestion.ModifyPlace(place);
            return result;
        }

        public bool RespondToInvitation(string invitationId, bool response)
        {
            var inviteInQuestion = invitations.GetInvitationById( invitationId);
            var result = inviteInQuestion.RespondToInvitation(response);
            return result;
        }

        public bool BuyGiftForInvitation(string invitationId, string giftId)
        {
            var inviteInQuestion = invitations.GetInvitationById( invitationId);
            var gift = inviteInQuestion.GetGiftsForEvent().GetGiftById(giftId);
            return inviteInQuestion.AddGiftForEvent(new PresentableGift(userName, gift));
        }

        public bool ModifyGiftForInvitation(string invitationId, string giftIdToBeRemoved, string newGiftId)
        {
            var isRemoved = RemoveGiftForInvitation(invitationId, giftIdToBeRemoved);
            var isAdded = BuyGiftForInvitation(invitationId, newGiftId);
            return isRemoved && isAdded;
        }

        public bool RemoveGiftForInvitation(string invitationId, string giftId)
        {
            var inviteInQuestion = invitations.GetInvitationById(invitationId);
            var gift = inviteInQuestion.GetGiftsForEvent().GetGiftById(giftId);
            return inviteInQuestion.RemoveGiftForEvent(gift);
        }
    }
}

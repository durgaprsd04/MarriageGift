using System;
using log4net;
using MarriageGift.Controller;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Model.EventModel;
using MarriageGift.Controller.Interfaces;
using MarriageGift.Exceptions;
namespace MarriageGift.Model.CustomerModel
{
   public class Customer: BaseObject, ICustomer
    {         
        private readonly string passWord;
        private string userName;
        private readonly IInvitationCollection invitations;
        private readonly IEventCollection events;
        private readonly ILog logger;
        private readonly ISaveToFileController saveToFileController;      

       
        public Customer(string userName, string passWord)
        :base()
        {   
            this.userName  =userName;
            this.passWord = passWord;
            this.invitations = new InvitationCollection();
            this.events = new EventCollection();
        }
        public Customer(string custId, string userName, string passWord, IInvitationCollection invitations, IEventCollection events)
        :base(custId)
        {   
            this.userName  =userName;
            this.passWord = passWord;
            this.invitations = new InvitationCollection();
            this.events = new EventCollection(); 
        }
        public bool AddMyEvents(IEvent myEvent)
        {
            var result = events.AddEvent(myEvent);
            return result;
        }

        public  bool AddMyInvitations(IInvitation invitation)
        {
            var result = invitations.AddInvitation(invitation, this);
            return result;
        }        
        public  bool CancelEvent(string eventId)
        {
            var result = false;
            var eventInQuestion = events.GetEvent( eventId);
            if (eventInQuestion == null)
                throw new CustomerNotFoundException(eventId);
            result = eventInQuestion.Cancel(true);            
            return result;
        }

        public  bool ChangeEventTime(string eventId, DateTime date)
        {
            var result = false;
            var eventInQuestion = events.GetEvent(eventId);
            if (eventInQuestion == null)
               throw new CustomerNotFoundException(eventId);
            result = eventInQuestion.ModifyDate(date);                        
            return result;
        }

        public bool ChangeEventVenue(string eventId, string place)
        {
            var result = false;
            var eventInQuestion = events.GetEvent(eventId);
            if (eventInQuestion == null)
                throw new CustomerNotFoundException(eventId);
            result = eventInQuestion.ModifyPlace(place);
            return result;
        }

        public bool RespondToInvitation(string invitationId, bool response)
        {
            var result = false;
            var inviteInQuestion = invitations.GetInvitationById( invitationId);
            if (inviteInQuestion == null)
                throw new InvitationNotFoundException(invitationId);
            result = inviteInQuestion.RespondToInvitation(response);
            return result;
        }

        public bool BuyGiftForInvitation(string invitationId, string giftId)
        {
            var result = false;
            var inviteInQuestion = invitations.GetInvitationById( invitationId);
            if (inviteInQuestion == null)
                throw new InvitationNotFoundException(invitationId);
            var gift = inviteInQuestion.GetExpectedGiftsForEvent().GetGift(giftId);
            if (gift == null)
                throw new GiftNotFoundException(giftId);
            result = inviteInQuestion.AddGiftForEvent(gift);
            return result;
        }

        public bool ModifyGiftForInvitation(string invitationId, string giftIdToBeRemoved, string newGiftId)
        {
            var isRemoved = RemoveGiftForInvitation(invitationId, giftIdToBeRemoved);
            var isAdded = BuyGiftForInvitation(invitationId, newGiftId);
            return isRemoved && isAdded;
        }

        public bool RemoveGiftForInvitation(string invitationId, string giftId)
        {
            var result = false;
            var inviteInQuestion = invitations.GetInvitationById(invitationId);
            if (inviteInQuestion == null)
                throw new InvitationNotFoundException(invitationId);
            var gift = inviteInQuestion.GetRecievedGiftsForEvent().GetGift(giftId);
            if (gift == null)
                throw new GiftNotFoundException(giftId);
            result =inviteInQuestion.RemoveGiftForEvent(gift);
            return result;
        }
        public void SaveToFile()
        {
            saveToFileController.SaveCustomer(this);
        }
        public override string ToString()
        {
            return base.getId() + "|" + userName;
        }

        public IGenericCollection<IBaseObject> GetMyEvents()
        {
            return events;
        }
    }
}

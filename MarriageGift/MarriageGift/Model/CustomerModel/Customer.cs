using System;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Model.EventModel;
using MarriageGift.Exceptions.CustomerExceptions;
using MarriageGift.Exceptions.InvitationExceptions;
using MarriageGift.Exceptions.GiftExceptions;
using MarriageGift.Exceptions.EventExceptions;
namespace MarriageGift.Model.CustomerModel
{
   public class Customer: BaseObject, ICustomer
    {         
        private readonly string passWord;
        private readonly string userName;
        private readonly IInvitationCollection invitations= new InvitationCollection();
        private readonly IEventCollection events= new EventCollection();

       
        public Customer(string userName, string passWord)
        :base()
        {   
            this.userName  =userName;
            this.passWord = passWord;           
        }
        public Customer(string custId, string userName, string passWord, IInvitationCollection invitations, IEventCollection events)
        :base(custId)
        {   
            this.userName  =userName;
            this.passWord = passWord;
            this.invitations =invitations;
            this.events = events;
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

        public void RespondToInvitation(string invitationId, bool response)
        {          
            var inviteInQuestion = invitations.GetInvitationById( invitationId);
            if (inviteInQuestion == null)
                throw new InvitationNotFoundException(invitationId);
            inviteInQuestion.RespondToInvitation(response);            
        }

        public bool BuyGiftForInvitation(string invitationId, string giftId)
        {
            var result = false;
            var inviteInQuestion = invitations.GetInvitationById( invitationId);
            if (inviteInQuestion == null)
                throw new InvitationNotFoundException(invitationId);
            var gift = inviteInQuestion.GetExpectedGiftsForEvent().GetGiftById(giftId);
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
            var gift = inviteInQuestion.GetRecievedGiftsForEvent().GetGiftById(giftId);
            if (gift == null)
                throw new GiftNotFoundException(giftId);
            result =inviteInQuestion.RemoveGiftForEvent(gift);
            return result;
        }       
        public override string ToString()
        {
            return base.getId() + "|" + userName;
        }

        public IGenericCollection<IBaseObject> GetMyEvents()
        {
            return events;
        }
        public string GetUserName() => userName;

        public string GetPassword() => passWord;


        public bool InviteForEvent(string eventId, ICustomer customer)
        {
            var eventInQuestion = events.GetEvent(eventId);
            if(eventInQuestion==null)
                throw new EventNotFoundException(eventId);
            var invite = new Invitation(this, eventInQuestion);           
            invite.AddCustomerToListofInvitees(customer);
            customer.AddMyInvitations(invite);
            return true;
        }
    }
}

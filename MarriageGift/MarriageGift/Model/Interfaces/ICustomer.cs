using System;
using System.Runtime.Serialization;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
    public interface ICustomer:IBaseObject, ISerializable
    {       
         bool AddMyEvents(IEvent myEvent);
         bool AddMyInvitations(IInvitation invitation);
        string GetUserName();
        string GetPassword();
         IGenericCollection<IBaseObject> GetMyEvents();
            IGenericCollection<IBaseObject> GetMyInvitations();
         bool CancelEvent(string eventId);
         bool ChangeEventTime(string eventId, DateTime date);
         bool ChangeEventVenue(string eventId, string place);
         bool InviteForEvent(string eventId, ICustomer customer);
         void RespondToInvitation(string invitationId, bool response);
         bool BuyGiftForInvitation(string invitationId, string giftId);
         bool ModifyGiftForInvitation(string invitationId, string giftIdToBeRemoved, string newGiftId);
         bool RemoveGiftForInvitation(string invitationId, string giftId);         
    }
   
}

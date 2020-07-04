using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
    public interface ICustomer
    {
        bool AddMyEvents(IEvent myEvent);
        bool AddMyInvitations(IInvitation invitation);
        bool CancelEvent(string eventId);
        bool ChangeEventTime(string eventId, DateTime date);
        bool ChangeEventVenue(string eventId, string place);
        bool RespondToInvitation(string invitationId, bool response);
        bool BuyGiftForInvitation(string invitationId, string giftId);
        bool ModifyGiftForInvitation(string invitationId, string giftIdToBeRemoved, string newGiftId);
        bool RemoveGiftForInvitation(string invitationId, string giftId);

    }
   
}

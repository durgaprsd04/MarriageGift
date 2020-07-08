using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
   public interface IInvitation
    {
        bool RespondToInvitation(bool response);
        IGiftCollection<IGift> GetRecievedGiftsForEvent();
        IGiftCollection<IGift> GetExpectedGiftsForEvent();
        bool AddGiftForEvent(IGift gift);
        bool AddCustomerToListofInvitees(ICustomer customer);
        ICustomerCollection GetListofInvitees();
        bool RemoveGiftForEvent(IGift giftId);
    }
}

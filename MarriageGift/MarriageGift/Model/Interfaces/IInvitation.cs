using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
   public interface IInvitation
    {
        bool RespondToInvitation(bool response);
        IGiftCollection<IGift> GetGiftsForEvent();
        bool AddGiftForEvent(IGift gift);
        bool RemoveGiftForEvent(IGift giftId);
    }
}

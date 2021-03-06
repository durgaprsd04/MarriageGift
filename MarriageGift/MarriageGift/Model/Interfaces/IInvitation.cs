﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
   public interface IInvitation :IBaseObject
    {
        void RespondToInvitation(bool response);
        IGiftCollection<IGift> GetRecievedGiftsForEvent();
        IGiftCollection<IGift> GetExpectedGiftsForEvent();
        bool AddGiftForEvent(IGift gift);
        IEvent GetEvent();
        bool AddCustomerToListofInvitees(ICustomer customer);
        ICustomerCollection GetListofInvitees();
        bool RemoveGiftForEvent(IGift giftId);
    }
}

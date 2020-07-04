﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
   public interface IInvitation
    {
        bool RespondToInvitation(bool response);
        IGiftCollection GetGiftsForEvent();

    }
}

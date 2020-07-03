using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
    public interface IEvent
    {
        bool ModifyPlace();
        bool ModifyDate();
        bool Cancel();
    }
}

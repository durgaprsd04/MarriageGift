using System;
using System.Collections.Generic;
using System.Text;
using MarriageGift.Model.Event

namespace MarriageGift.Model.Interfaces
{
    public interface IOccassion:IBaseObject
    {
        bool modifyOccasion(IOccassion occassion);

    }
}

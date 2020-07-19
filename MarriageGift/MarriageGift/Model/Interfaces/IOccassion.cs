using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
    public interface IOccassion:IBaseObject
    {
        bool modifyOccasion(IOccassion occassion);

    }
}

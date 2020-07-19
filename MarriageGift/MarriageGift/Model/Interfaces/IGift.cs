using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
    public interface IGift : IComparable, IBaseObject
    {
        void ModifyGift(IGift gift);
        string GetGiftId();
    }
}

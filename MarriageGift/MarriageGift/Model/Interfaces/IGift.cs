using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
    public interface IGift : IComparable
    {
        bool ModifyGift(IGift gift);
        string GetGiftId();
    }
}

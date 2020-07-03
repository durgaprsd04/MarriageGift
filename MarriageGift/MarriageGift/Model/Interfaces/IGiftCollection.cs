using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
    public interface IGiftCollection
    {
        bool AddGift(IGift gift);
        bool RemoveGift(IGift gift);
    }
}

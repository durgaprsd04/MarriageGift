using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model.Interfaces
{
    public interface IEvent
    {
        bool ModifyPlace(string place);
        bool ModifyDate(DateTime date);
        bool Cancel();
        bool AddExpectedGift(IGift gift);
        bool RemoveExpectedGift(IGift gift);
        bool AddRecievedGifts(IGift gift);
        bool RemoveRecievedGifts(IGift gift);
        IGiftCollection<IGift> ExpectedGiftCollection();

    }
}

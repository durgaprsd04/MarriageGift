using System.Collections.Generic;
using MarriageGift.Model.Interfaces;
using MarriageGift.Enums;

namespace MarriageGift.Controller.Interfaces
{
    public interface ISelectionController
    {
        Dictionary<int,string> GetOccasionTypes();
        IDictionary<string,string> GetAllGifts();
        string GetCustomerById(string customerId);
        IGiftCollection<IGift> GetGiftsForGiftIds(string [] ids);
        IOccassion GetDummyOccassion(Occasion type);
    }
}
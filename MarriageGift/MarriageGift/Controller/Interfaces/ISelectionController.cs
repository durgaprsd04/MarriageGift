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
        IOccassion GetOccassion(Occasion occassion, string person1, string person2);
    }
}

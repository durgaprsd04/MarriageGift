using System.Collections.Generic;

namespace MarriageGift.Controller.Interfaces
{
    public interface ISelectionController
    {
        Dictionary<int,string> GetOccasionTypes();
        IDictionary<string,string> GetAllGifts();
    }
}
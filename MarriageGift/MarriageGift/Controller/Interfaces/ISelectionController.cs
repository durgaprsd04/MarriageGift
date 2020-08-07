using System.Collections.Generic;
using MarriageGift.Model.CustomerModel;

namespace MarriageGift.Controller.Interfaces
{
    public interface ISelectionController
    {
        Dictionary<int,string> GetOccasionTypes();
        IDictionary<string,string> GetAllGifts();
        string GetCustomerById(string customerId);
    }
}
using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Controller.Interfaces
{
    public interface ICustomerController
    {        
        string Login(string username, string password);
        bool ChangePassword(string username, string password);
        string CreateEvent(IOccassion occassion, string place,DateTime date,IGiftCollection<IGift> giftE, IGiftCollection<IGift> giftR);
        bool InvitePerson(IEvent eventInQuestion, ICustomer customer);
        bool BuyGiftForEvent(IInvitation invitation, string giftId);
        bool RemoveGiftForEvent(IInvitation invitation, string giftId);
        bool RespondToInvite(string inviteId, bool response);
        bool ModifEvent(IEvent eventInQ);
        bool SaveCustomerToFile();
        string GetCustomerId();
        void  CreateOccassion(IOccassion occassion1);
    }
}

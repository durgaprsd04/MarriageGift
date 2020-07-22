using System;
using MarriageGift.Model.Interfaces;
using System.Text;

namespace MarriageGift.Controller.Interfaces
{
    public interface ICustomerController
    {
        bool login(string username, string password);
        bool CreateEvent(IOccassion occassion, string place,DateTime date,IGiftCollection<IGift> giftE, IGiftCollection<IGift> giftR);
        bool InvitePerson(string custId);
        bool BuyGiftForEvent(string eventId, string giftId);
        bool RemoveGiftForEvent(string eventId, string giftId);
        bool RespondToInvite(string inviteId);
    }
}

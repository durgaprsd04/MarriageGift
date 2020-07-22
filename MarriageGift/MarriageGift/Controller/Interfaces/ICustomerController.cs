using System;
using MarriageGift.Model.Interfaces;
using System.Text;

namespace MarriageGift.Controller.Interfaces
{
    public interface ICustomerController
    {
        bool login(string username, string password);
        bool CreateEvent(IOccassion occassion, string place,DateTime date,IGiftCollection<IGift> giftE, IGiftCollection<IGift> giftR);
        bool InvitePerson(IEvent eventInQuestion);
        bool BuyGiftForEvent(IInvitation invitation, string giftId);
        bool RemoveGiftForEvent(IInvitation invitation, string giftId);
        bool RespondToInvite(string inviteId, bool response);
    }
}

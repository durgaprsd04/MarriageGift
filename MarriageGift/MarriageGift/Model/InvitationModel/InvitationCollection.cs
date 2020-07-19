using System;
using MarriageGift.Model.Interfaces;
namespace MarriageGift.Model.InvitationModel 
{
    public class InvitationCollection : GenericCollection, IInvitationCollection
    { 
        public InvitationCollection()
        :base()
        {
           
        }
        public bool AddInvitation(IInvitation inviteItem, ICustomer customer)
        {              
            if(!(inviteItem is Invitation inviteGen))
                throw new ArgumentNullException("inviteItem");
            return Add(inviteGen);             
        }
        public bool RemoveInvitation(IInvitation inviteItem)
        {           
            if(!(inviteItem is Invitation inviteGen))
                throw new ArgumentNullException("inviteItem");
            return Add(inviteGen);
        }
        public IInvitation GetInvitationById(string invitationId)
        {
            return (IInvitation)GetItem(invitationId);
        }        
    }
}

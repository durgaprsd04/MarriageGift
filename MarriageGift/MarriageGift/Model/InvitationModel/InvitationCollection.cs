using System;
using MarriageGift.Model.Interfaces;
using System.Collections.Generic;
using log4net;

namespace MarriageGift.Model.InvitationModel
{
    public class InvitationCollection : IInvitationCollection
    {
        private Dictionary<string, IInvitation>    invitationCollection;
        public InvitationCollection()
        {
            invitationCollection = new Dictionary<string, IInvitation>();
        }
        public bool AddInvitation(IInvitation inviteItem, ICustomer customer)
        {              
            var inviteGen = inviteItem as Invitation;
            if(inviteGen==null)
                throw new ArgumentNullException("inviteItem");
           
            inviteGen.AddCustomerToListofInvitees(customer);
            invitationCollection.Add(inviteGen.InvitationId, inviteGen); 
            return true;
        }

        public bool RemoveInvitation(IInvitation inviteItem)
        {
            var succesFlag = false;
            var inviteGen = inviteItem as Invitation;
            if(inviteGen == null)
                throw new ArgumentNullException("inviteItem");

            if (invitationCollection.ContainsKey(inviteGen.InvitationId))
            {
                invitationCollection.Remove(inviteGen.InvitationId);
                succesFlag = true;
            }
            return succesFlag;
        }
        public IInvitation GetInvitationById(string invitationId)
        {
            IInvitation invitation=null;
            if(invitationCollection.ContainsKey(invitationId))
                   invitation= invitationCollection[invitationId];
            return invitation;
        }
        public int Count()
        {
            return invitationCollection.Count;
        }
    }
}

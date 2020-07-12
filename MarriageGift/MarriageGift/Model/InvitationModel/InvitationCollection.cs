using System;
using MarriageGift.Model.Interfaces;
using System.Collections.Generic;
using log4net;

namespace MarriageGift.Model.InvitationModel
{
    public class InvitationCollection : IInvitationCollection
    {
        private Dictionary<string, IInvitation>    invitationCollection;
        private readonly ILog logger;        
        public InvitationCollection(ILog logger)
        {
            this.logger=logger;
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
            var inviteGen = inviteItem as Invitation;
            if(inviteGen==null)
                throw new ArgumentNullException("inviteItem");
                
            invitationCollection.Remove(inviteGen.InvitationId);
            return true;
        }
        public IInvitation GetInvitationById(string invitationId)
        {
            IInvitation invitation=null;
            try
            {
                if(invitationCollection.ContainsKey(invitationId))
                    invitation= invitationCollection[invitationId];
            }
            catch(Exception e)
            {
                logger.Error("Error in getting invitation by Id"+e.Message);
                logger.Error(e.Message);
            }
            return invitation;
        }
        public int Count()
        {
            return invitationCollection.Count;
        }
    }
}

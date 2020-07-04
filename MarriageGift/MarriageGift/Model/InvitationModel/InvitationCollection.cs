using System;
using MarriageGift.Model.Interfaces;
using System.Collections.Generic;


namespace MarriageGift.Model.InvitationModel
{
    public class InvitationCollection : IInvitationCollection
    {
        private Dictionary<string, IInvitation> invitationCollection = new Dictionary<string, IInvitation>();
        public bool AddInvitation(IInvitation inviteItem)
        {
            var successFlag =false;
            var inviteGen = inviteItem as Invitation;
            if(inviteGen==null)
                throw new ArgumentException("inviteItem");
            try
            {
                invitationCollection.Add(inviteGen.InvitationId, inviteGen);
                successFlag=true;
            }
            catch(Exception e)
            {
                //
            }
            return successFlag;
        }

        public bool RemoveInvitation(IInvitation inviteItem)
        {
            var successFlag =false;
            var inviteGen = inviteItem as Invitation;
            if(inviteGen==null)
                throw new ArgumentException("inviteItem");
            try
            {
                invitationCollection.Remove(inviteGen.InvitationId);
                successFlag=true;
            }
            catch(Exception e)
            {
                //
            }
            return successFlag;
        }
    }
}

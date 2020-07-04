using System;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.InvitationModel
{

    public class Invitation : IInvitation
    {
        private string invitationId;
        private string sender;
        private IEvent mainEvent;
        private bool isAccepted;
        public Invitation(string sender, IEvent mainEvent)
        {
            InvitationId = Guid.NewGuid().ToString();
            this.sender =sender;
            this.mainEvent = mainEvent;
        }

        public string InvitationId { get => invitationId; set => invitationId = value; }

        public bool respondToInvitation(bool response)
        {
            isAccepted=response;
            return response;
        }

        
    }
}
    
using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model
{
    class Customer
    {
        private readonly string custId;
        private string userName;
        private readonly IInvitationCollection invitations;
        private readonly IEventCollection events;

        public Customer(string userName, IInvitationCollection invitations, IEventCollection events)
        {
            custId = Guid.NewGuid().ToString();
            this.userName = userName;
            this.invitations = invitations;
            this.events = events;
        }
    }
}

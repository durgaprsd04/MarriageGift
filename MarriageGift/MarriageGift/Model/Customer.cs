using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model
{
    class Customer
    {
        private readonly string userName;
        private readonly IInvitation invitations;
        private readonly IEvent events;

        public Customer(string userName, IInvitation invitations, IEvent events)
        {
            this.userName = userName;
            this.invitations = invitations;
            this.events = events;
        }
    }
}

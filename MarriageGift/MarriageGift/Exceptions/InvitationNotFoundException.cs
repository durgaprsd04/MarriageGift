using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Exceptions
{
    public class InvitationNotFoundException:Exception
    {
        public InvitationNotFoundException(string invitationId) 
            : base(string.Format("Event not found for id {0}", invitationId))
        {

        }
    }
}

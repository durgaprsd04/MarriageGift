using System;

namespace MarriageGift.Exceptions
{
    public class CustomerNotFoundException:Exception
    {
        public CustomerNotFoundException(string eventId):base(string.Format("Event not found for id {0}",eventId))
        {

        }
    }
}

﻿using System;

namespace MarriageGift.Exceptions
{
    public class EventNotFoundException:Exception
    {
        public EventNotFoundException(string eventId):base(string.Format("Event not found for id {0}",eventId))
        {

        }
    }
}

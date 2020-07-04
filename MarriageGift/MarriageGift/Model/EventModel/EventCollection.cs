using System;
using MarriageGift.Model.Interfaces;
using System.Collections.Generic;


namespace MarriageGift.Model.EventModel
{
    public class EventCollection : IEventCollection
    {
        private Dictionary<string, IEvent> eventCollection = new Dictionary<string, IEvent>();
        public bool AddEvent(IEvent eventItem)
        {
            var successFlag =false;
            var eventGen = eventItem as Event;
            if(eventGen==null)
                throw new ArgumentException("eventItem");
            try
            {
                eventCollection.Add(eventGen.EventId, eventGen);
                successFlag=true;
            }
            catch(Exception e)
            {
                //
            }
            return successFlag;
        }

        public bool RemoveEvent(IEvent eventItem)
        {
             var successFlag =false;
            var eventGen = eventItem as Event;
            if(eventGen==null)
                throw new ArgumentException("eventItem");
            try
            {
                eventCollection.Remove(eventGen.EventId);
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

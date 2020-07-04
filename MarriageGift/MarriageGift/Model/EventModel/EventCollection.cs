using System;
using System.Linq;
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
        public IEventCollection AddEventsToCollection(IEnumerable<IEvent> eventCollection)
        {
            var eventCollection1 = new EventCollection();
            foreach(var eventElement in eventCollection)
            {   
                var ev = eventElement as Event;
                if(ev!=null)
                    eventCollection1.AddEvent(ev);
            }
            return eventCollection1;
        }
        public IEvent GetEventById(string eventId)
        {
            if(eventCollection.ContainsKey(eventId))
            {
                return eventCollection[eventId];
            }
            return null;
        }
        public IEventCollection GetEventsByCustId(string custId)
        {
            var eventCollection1 = new EventCollection();
            try{
                var eventListForCustomer = from  x  in eventCollection 
                                        where ((Event)x.Value).CustId==custId
                                        select x.Value;
            eventCollection1 = (EventCollection)AddEventsToCollection(eventListForCustomer);
            }
            catch(Exception e)
            {
                //commented for now.
            }
            return eventCollection1;
        }
    }
}

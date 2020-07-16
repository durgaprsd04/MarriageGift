using System;
using System.Linq;
using MarriageGift.Model.Interfaces;
using System.Collections.Generic;

namespace MarriageGift.Model.EventModel
{
    public class EventCollection : IEventCollection
    {
        private readonly Dictionary<string, IEvent> eventCollection;
        public EventCollection()
        {          
            eventCollection = new Dictionary<string, IEvent>();
        }       
        public bool AddEvent(IEvent eventItem)
        {
            var eventGen = eventItem as Event;  
            if(eventGen==null)
                throw new ArgumentException("eventItem");
            eventCollection.Add(eventGen.EventId, eventGen);            
            return true;
        }

        public bool RemoveEvent(IEvent eventItem)
        {
            var succesFlag = false;
            var eventGen = eventItem as Event;
            if (eventGen == null)
                throw new ArgumentException("eventItem");
            if (eventCollection.ContainsKey(eventGen.EventId))
            {
                eventCollection.Remove(eventGen.EventId);
                succesFlag = true;
            }
            return succesFlag;                
        }
        public IEventCollection AddEventsToCollection(IEnumerable<IEvent> eventCollection)
        {
            var eventCollection1 = new EventCollection();
            foreach (var eventElement in eventCollection)
            {
                eventCollection1.AddEvent(eventElement);
            }            
            return eventCollection1;
        }
        public IEvent GetEventById(string eventId)
        {
            IEvent event1=null;
            if (eventCollection.ContainsKey(eventId))
            {
                event1= eventCollection[eventId];
            }            
            return event1;
        }
        public IEventCollection GetEventsByCustId(string custId)
        {
            var eventCollection1 = new EventCollection();
            var eventListForCustomer = from  x  in eventCollection 
                                    where ((Event)x.Value).CustId==custId
                                    select x.Value;
            eventCollection1 = (EventCollection)AddEventsToCollection(eventListForCustomer);
            return eventCollection1;
        }
        public int Count()
        {
            return eventCollection.Count;
        }
    }
}

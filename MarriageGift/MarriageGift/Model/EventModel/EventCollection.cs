using System;
using System.Linq;
using MarriageGift.Model.Interfaces;
using System.Collections.Generic;

namespace MarriageGift.Model.EventModel
{
    public class EventCollection : GenericCollection, IEventCollection
    {
         public bool AddEvent(IEvent eventItem)
        {
            if(!(eventItem is Event eventGen))
                throw new ArgumentException("eventItem");
            return Add(eventGen);
        }

        public bool RemoveEvent(IEvent eventItem)
        {            
           if (!(eventItem is Event eventGen))
                throw new ArgumentException("eventItem");
            return Remove(eventGen);         
        }
        public IEvent GetEvent(string eventId)
        {
            return (IEvent)GetItem(eventId);
        }
        public IEventCollection AddEventsToCollection(IEnumerable<IBaseObject> eventCollection)
        {
            var eventCollection1 = new EventCollection();
            foreach (var eventElement in eventCollection)
            {
                eventCollection1.AddEvent((IEvent)eventElement);
            }            
            return eventCollection1;
        }       
        public IEventCollection GetEventsByCustId(string custId)
        {
            var eventCollection1 = new EventCollection();
            var eventListForCustomer = from  x  in underlyingCollection 
                                    where ((Event)x.Value).CustId==custId
                                    select x.Value;
            eventCollection1 = (EventCollection)AddEventsToCollection(eventListForCustomer);
            return eventCollection1;
        }       
    }
}

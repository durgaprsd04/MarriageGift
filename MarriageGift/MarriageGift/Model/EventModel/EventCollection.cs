using System;
using System.Linq;
using MarriageGift.Model.Interfaces;
using MarriageGift.Exceptions;
using System.Collections.Generic;
using log4net;

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
            var successFlag =false;
            var eventGen = eventItem as Event;
            if(eventGen==null)
                throw new ArgumentException("eventItem");
            try
            {
                eventCollection.Add(eventGen.EventId, eventGen);
                successFlag = true;
            }
            catch(Exception e)
            {
                throw new EventCollectionAddException(e.Message);
            }
            return successFlag;
        }

        public bool RemoveEvent(IEvent eventItem)
        {
            var successFlag =false;
            var eventGen = eventItem as Event;
            if (eventGen == null)
                throw new ArgumentException("eventItem");

            if (!eventCollection.ContainsKey(eventGen.EventId))
            {
                throw new EventCollectionRemoveException("Key not found in collecton");
            }
            else
            {
                eventCollection.Remove(eventGen.EventId);
                successFlag = true;
            }
            return successFlag;
        }
        public IEventCollection AddEventsToCollection(IEnumerable<IEvent> eventCollection)
        {
            var eventCollection1 = new EventCollection();
            try
            {
                foreach (var eventElement in eventCollection)
                {
                    var ev = eventElement as Event;
                    if (ev != null && ev.IsCanceled)
                        eventCollection1.AddEvent(ev);
                }
            }
            catch(Exception e)
            {
                throw new EventCollectionAddException(e.Message);
            }
            
            return eventCollection1;
        }
        public IEvent GetEventById(string eventId)
        {
            IEvent event1=null;
            try
            {
                if (eventCollection.ContainsKey(eventId))
                {
                    event1= eventCollection[eventId];
                }
            } 
            catch(Exception e)
            {
                throw new EventNotFoundException(e.Message);
            }            
            return event1;
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
                new EventNotFoundException(e.Message);
            }
            return eventCollection1;
        }
    }
}

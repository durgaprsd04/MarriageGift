using System;
using System.Linq;
using MarriageGift.Model.Interfaces;
using System.Collections.Generic;
using log4net;

namespace MarriageGift.Model.EventModel
{
    public class EventCollection : IEventCollection
    {
        private readonly Dictionary<string, IEvent> eventCollection;
        private readonly ILog logger;

        public EventCollection(ILog logger)
        {
            this.logger = logger;
            eventCollection = new Dictionary<string, IEvent>();
        }
        private EventCollection()
        {
            eventCollection = new Dictionary<string, IEvent>();
        }
        public bool AddEvent(IEvent eventItem)
        {
            var successFlag =false;
            try
            {
                var eventGen = eventItem as Event;
                if(eventGen==null)
                throw new ArgumentException("eventItem");
            
                eventCollection.Add(eventGen.EventId, eventGen);
                successFlag=true;
            }
            catch(Exception e)
            {
                logger.Error("Event addition failed " + e.Message);
                logger.Error(e.StackTrace);
            }
            return successFlag;
        }

        public bool RemoveEvent(IEvent eventItem)
        {
            var successFlag =false;
            try
            {
                var eventGen = eventItem as Event;
                if(eventGen==null)
                throw new ArgumentException("eventItem");
            
                eventCollection.Remove(eventGen.EventId);
                successFlag=true;
            }
            catch(Exception e)
            {
                logger.Error("Event addition failed " + e.Message);
                logger.Error(e.StackTrace);
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
                logger.Error("Exception occured while generation colleciton " + e.Message);
                logger.Error(e.StackTrace);
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
                logger.Error("Exception occured while fetching event based on eventId: " + e.Message);
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
                logger.Error("Exception occured while getting event based on customer Id" + e.Message);
                logger.Error(e.Message);
            }
            return eventCollection1;
        }
    }
}

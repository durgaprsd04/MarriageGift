using System.Collections.Generic;
namespace MarriageGift.Model.Interfaces
{
    public interface IEventCollection
    {
        bool AddEvent(IEvent event1);
        bool RemoveEvent(IEvent event1);
        IEvent GetEventById(string eventId);
        IEventCollection AddEventsToCollection(IEnumerable<IEvent> eventCollection);
        
        IEventCollection GetEventsByCustId(string custId);
    }
}

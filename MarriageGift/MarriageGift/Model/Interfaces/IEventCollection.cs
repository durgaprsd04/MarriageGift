using System.Collections.Generic;
namespace MarriageGift.Model.Interfaces
{
    public interface IEventCollection :IGenericCollection<IBaseObject>
    {
        bool AddEvent(IEvent event1);
        bool RemoveEvent(IEvent event1);
        IEvent GetEventById(string eventId);
        IEventCollection AddEventsToCollection(IEnumerable<IBaseObject> eventCollection);
        int Count();
        IEventCollection GetEventsByCustId(string custId);
    }
}

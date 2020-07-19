using System.Collections.Generic;
namespace MarriageGift.Model.Interfaces
{
    public interface IEventCollection :IGenericCollection<IBaseObject>
    {
        bool AddEvent(IEvent eventItem);
        bool RemoveEvent(IEvent eventItem);
        IEvent GetEvent(string eventId);
        IEventCollection AddEventsToCollection(IEnumerable<IBaseObject> eventCollection);      
        IEventCollection GetEventsByCustId(string custId);
    }
}

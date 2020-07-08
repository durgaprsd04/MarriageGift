namespace MarriageGift.Exceptions
{
    public class EventCollectionRemoveException : CollectionException
    {
        public EventCollectionRemoveException(string message) : base(string.Format("Exception while removing Gift from collection, with base exception {0}.", message))
        {

        }
    }
}

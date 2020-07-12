namespace MarriageGift.Exceptions
{
    public class EventCollectionAddException : CollectionException
    {
        public EventCollectionAddException(string message) : base(string.Format("Exception while adding Gift to collection, with base exception {0}.", message))
        {

        }

    }
}

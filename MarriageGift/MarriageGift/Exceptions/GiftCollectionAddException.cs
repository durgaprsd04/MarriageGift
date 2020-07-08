namespace MarriageGift.Exceptions
{
    public class GiftCollectionAddException : CollectionException
    {
        public GiftCollectionAddException(string message) : base(string.Format("Exception while adding Gift to collection, with base exception {0}.", message))
        {

        }

    }
}

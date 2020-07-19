namespace MarriageGift.Exceptions.GiftExceptions
{
    public class GiftCollectionRemoveException:CollectionException
    {
        public GiftCollectionRemoveException(string message) : base(string.Format("Exception while removing Gift from collection, with base exception {0}.", message))
        {

        }
    }
}

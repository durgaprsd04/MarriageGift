namespace MarriageGift.Exceptions.CustomerExceptions
{
    public class CustomerCollectionRemoveException : CollectionException
    {
        public CustomerCollectionRemoveException(string message) : base(string.Format("Exception while removing Gift from collection, with base exception {0}.", message))
        {

        }
    }
}

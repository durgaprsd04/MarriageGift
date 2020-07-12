namespace MarriageGift.Exceptions.CustomerExceptions
{
    public class CustomerCollectionAddException : CollectionException
    {
        public CustomerCollectionAddException(string message) : base(string.Format("Exception while adding Gift to collection, with base exception {0}.", message))
        {

        }

    }
}

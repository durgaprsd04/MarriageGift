namespace MarriageGift.Exceptions.InvitationExceptions
{
    public class InvitationCollectionAddException : CollectionException
    {
        public InvitationCollectionAddException(string message) : base(string.Format("Exception while adding Gift to collection, with base exception {0}.", message))
        {

        }

    }
}

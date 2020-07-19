namespace MarriageGift.Exceptions.InvitationExceptions
{
    public class InvitationCollectionRemoveException:CollectionException
    {
        public InvitationCollectionRemoveException(string message) : base(string.Format("Exception while removing Gift from collection, with base exception {0}.", message))
        {

        }
    }
}

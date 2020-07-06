using System;
namespace MarriageGift.Exceptions
{
    public class GiftCollectionAddException : Exception
    {
        public GiftCollectionAddException(string message) : base(string.Format("Exception while adding Gift to collection, with base exception {0}.", message))
        {

        }

    }
}

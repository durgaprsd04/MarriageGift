using System;

namespace MarriageGift.Exceptions
{
   public class CollectionException:Exception
    {
        public CollectionException(string message) : base(string.Format("Exception while adding to collection, with base exception {0}.", message))
        {

        }
    }
}
